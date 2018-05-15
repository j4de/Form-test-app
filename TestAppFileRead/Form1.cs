using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace TestAppFileRead
{
    public partial class Form1 : Form
    {
        //Procmon data
        private int PM_TimeOfDay = 1;
        private int PM_ProcessName = 3;
        private int PM_lengthFromSmallArray = 4;
        private int PM_PID = 5;
        private int PM_Operation = 7;
        private int PM_Path = 9;
        private int PM_FileName = 10;
        private int PM_Detail = 13;
        private int PM_Length = 14;

        //DataGridView data
        private int DG_Time = 0;
        private int DG_Name = 1;
        private int DG_PID = 2;
        private int DG_FileName = 3;
        private int DG_Operation = 4;
        private int DG_Length = 5;
        private int DG_Path = 6;

        private List<string> operationList = new List<string>();
        
        //To parse the Length value from the column => details
        public static Regex regexLength = new Regex(@"Length:\s*(?<getLengthNum>\d+(,\d+)*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            

        }
        
        //Get the file path selected.
        //Filter setup for CSV files in Form1.designer.cs
        private void GetFileButton(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = (openFileDialog1.FileName);
            }

        }


        //Only setup to Read in the data from a Procmon CSV file
        private void DisplayButton_Click(object sender, EventArgs e)
        {


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Time");
            dataTable.Columns.Add("Process Name");
            dataTable.Columns.Add("PID");
            dataTable.Columns.Add("File Name");
            dataTable.Columns.Add("Operation");
            dataTable.Columns.Add("Length");
            dataTable.Columns.Add("Path");
            
            string filePath = textBoxFilePath.Text;

            try
            {
                StreamReader streamReader = new StreamReader(filePath);
                string[] totalData = new string[File.ReadAllLines(filePath).Length];

                long fileSize;
                long stringSize;
                long progress = 0;


                FindFileSize(filePath, out fileSize);

                //progress bar
                loadingForm load = new loadingForm(dataGridView1, this, fileSize, progress);
                load.Show();
                
                //Reset the data grid
                dataGridView1.DataSource = null;

                if (fileSize != 0)
                {
                    string defaultLengthValue = "0";
                    string defaultPath = "noFilePathFound";
                    
                    string line = streamReader.ReadLine();

                    int lineCount = 1;
                    totalData = streamReader.ReadLine().Split('"');
                    stringSize = line.Length + 2;
                    progress += stringSize;
                    while (!streamReader.EndOfStream)
                    {
                        lineCount++;
                        line = streamReader.ReadLine();
                        totalData = line.Split('"');

                        stringSize = line.Length + 2;
                        progress += stringSize;

                        Update();
                       
                        load.setprogress(progress);
                        if (load.iscanceled())
                        {
                            load.Close();
                            break;
                        }
                        Application.DoEvents();

                        ParseProcmonData(totalData, defaultLengthValue, defaultPath);



                        //to deal with arrays of different sizes
                        if (totalData.Length == 15)
                        {
                            dataTable.Rows.Add(
                                        totalData[PM_TimeOfDay],
                                        totalData[PM_ProcessName],
                                        totalData[PM_PID],
                                        totalData[PM_FileName],
                                        totalData[PM_Operation],
                                        totalData[PM_Length],
                                        totalData[PM_Path]
                                        );
                        }
                        if (totalData.Length == 14)
                        {
                            dataTable.Rows.Add(
                                        totalData[PM_TimeOfDay],
                                        totalData[PM_ProcessName],
                                        totalData[PM_PID],
                                        totalData[PM_FileName],
                                        totalData[PM_Operation],
                                        totalData[PM_lengthFromSmallArray],
                                        totalData[PM_Path]
                                      );
                        }

                    }
                    load.Close();
                    //If file load is not cancelled
                    ProcessFileData(dataTable, load);

                }

            }//end try
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void ParseProcmonData(string[] totalData, string defaultLengthValue, string defaultPath)
        {
            if (totalData.Length == 14)
            {
                
                foreach (var item in totalData)
                {

                    try
                    {
                        totalData[PM_Operation] = totalData[PM_Operation].Replace(" ", "");
                        totalData[PM_TimeOfDay] = totalData[PM_TimeOfDay].Substring(0, 8);
                        if (totalData[PM_FileName] == "")
                        {
                            totalData[PM_FileName] = defaultPath;
                        }
                        else
                            totalData[PM_FileName] = Path.GetFileName(totalData[PM_Path]).ToString();

                        //get value of length and remove any comma's
                        var match = regexLength.Match(totalData[PM_Detail]);
                        if (match.Success)
                        {
                            totalData[PM_lengthFromSmallArray] = match.Groups["getLengthNum"].Value;
                            totalData[PM_lengthFromSmallArray] = totalData[PM_Length].Replace(",", "");
                        }
                        else
                        {
                            totalData[PM_lengthFromSmallArray] = defaultLengthValue;
                        }
                    }

                    catch (IncorrectFormatException exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }

            }
            else
                foreach (var item in totalData)
                {

                    try
                    {


                        if (totalData.Length > 12)
                        {
                            totalData[PM_Operation] = totalData[PM_Operation].Replace(" ", "");
                            totalData[PM_TimeOfDay] = totalData[PM_TimeOfDay].Substring(0, 8);
                            if (totalData[PM_FileName] == "")
                            {
                                totalData[PM_FileName] = defaultPath;
                            }
                            else
                                totalData[PM_FileName] = Path.GetFileName(totalData[PM_Path]).ToString();

                            //get value of length and remove any comma's
                            var match = regexLength.Match(totalData[PM_Detail]);
                            if (match.Success)
                            {
                                totalData[PM_Length] = match.Groups["getLengthNum"].Value;
                                totalData[PM_Length] = totalData[PM_Length].Replace(",", "");
                            }
                            else
                            {
                                totalData[PM_Length] = defaultLengthValue;
                            }
                        }


                    }

                    catch (IncorrectFormatException exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            //get the offset value
        }

        private void ProcessFileData(DataTable dataTable, loadingForm load)
        {
            if (!load.iscanceled())
            {
                dataGridViewAllData.DataSource = dataTable;
                dataGridView1.DataSource = null;              
                dataGridView1.DataSource = dataTable;
                FindLengthForEachProcess();
                load.Close();
            }
        }


        private void SaveToCSV(DataGridView DGVfiles)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = ""
            };

            int counter = 0;

            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                Deleteifexists(filename);
                int columnCount = DGVfiles.ColumnCount;
                string columnNames = "";

                SaveOutputTotalFiles(DGVfiles, filename, ref counter, columnCount, ref columnNames);
            }
        }

        private static void SaveOutputTotalFiles(DataGridView DGV, string filename, ref int counter, int columnCount, ref string columnNames)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {

                for (int i = 0; i < columnCount; i++)
                {
                    columnNames += DGV.Columns[i].Name.ToString() + ",";
                }
                sw.WriteLine(columnNames);
                for (int i = 1; (i - 1) < DGV.RowCount; i++)
                {
                    string rowdata = "";
                    for (int j = 0; j < columnCount; j++)
                    {

                        rowdata += DGV.Rows[i - 1].Cells[j].Value.ToString() + ",";
                        counter++;
                    }
                    sw.WriteLine(rowdata);
                    //break out of loop to avoid null object reference
                    if (i == DGV.RowCount)
                        break;
                }
            }
            
        }

        private static void Deleteifexists(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                }
            }
        }


        //Convert file size for user information
        private static void FindFileSize(string filePath, out long fileSize)
        {
            FileInfo f = new FileInfo(filePath);
            fileSize = f.Length;
        }

        //Calculate the size/length of each process
        private void FindLengthForEachProcess()
        {
            var ProcessFileList = new List<ProcessData>();
            var ProcessIDList = new List<ProcessData>();
            
            int[] topLengths = new int[10];
            int[] topIDLengths = new int[10];
            string[] topFileNames = new string[10];
            string[] topProcessID = new string[10];
            
            int processLength = 0;
            string processTime;
            string processName = "";
            string processFileName = "";
            string processPath = "";
            string processPID = "";
            string processOperation = "";
            bool processFound = false;
            int loopCounter = 0;
            string processKeyString = "";
            //Var for bar chart
            string processKeyID = "";
            bool processIDFound = false;

            //find the rows with the matching process key and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            long rowCount =0;
            rowCount = dataGridView1.Rows.Count;
            long progress = 1;

            //progress bar
            loadingForm load = new loadingForm(dataGridView1, this, rowCount, progress);
            load.Show();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    processTime = row.Cells[DG_Time].Value.ToString();
                    processFileName = row.Cells[DG_FileName].Value.ToString();
                    processName = row.Cells[DG_Name].Value.ToString();
                    processOperation = row.Cells[DG_Operation].Value.ToString();
                    processPID = row.Cells[DG_PID].Value.ToString();
                    processPath = row.Cells[DG_Path].Value.ToString();
                    int iPL = 0;
                    int.TryParse(row.Cells[DG_Length].Value.ToString(), out iPL);
                    processLength = iPL;

                    

                    processKeyString = processName + "|" + processPID + "|" + processPath+ "|"+ processOperation;
                    processKeyID = processName + "|" + processPID + "|" + processOperation;

                    //If the processKey is found in the list, append the length value and set processFound to true
                    processFound = AppendLength(ProcessFileList, processLength, processKeyString);
                    if (processFound == false)
                    {
                        AddNewItemToList(ProcessFileList,  processLength, processName, processTime, processFileName, processPath, processPID, processOperation, processFound, processKeyString);
                    }
                    
                    processIDFound = AppendLength(ProcessIDList, processLength, processKeyID);
                    AddNewItemToList(ProcessIDList, processLength, processName, processTime, processFileName,  processPath, processPID, processOperation, processIDFound, processKeyID);



                    ComboBoxListItems(operationList, processOperation);


                    loopCounter++;

                    //Increment the progress bar
                    progress++;
                    load.setFilterprogress(progress);

                    if (load.iscanceled())
                    {
                        load.Close();
                        break;
                    }
                    Application.DoEvents();
                    if (loopCounter == dataGridView1.Rows.Count - 1)
                    {
                        break;
                    }
                       

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }

           

            SortedFileList(ProcessFileList);

            SortedProcessList(ProcessIDList);

            FilterOnCombobox(ProcessFileList, ProcessIDList, topLengths, topIDLengths, topFileNames, topProcessID);

            PopulateChart(topLengths, topIDLengths, topFileNames, topProcessID);

            totalProcessesLabel.Text = ProcessFileList.Count().ToString();


        }

        private void ComboBoxListItems(List<string> operationList, string processOperation)
        {
            
            bool alreadyExists = operationList.Any(x => x.Contains(processOperation));
            if (alreadyExists == false)
            {
               
                operationList.Add(processOperation);
                OperationComboBox.Items.Add(processOperation);
            }
 
        }

        //add a new process to the list
        private static void AddNewItemToList(List<ProcessData> ProcessFileList, int processLength, string processName, string processTime, string processFileName, string processPath, string processPID, string processOperation, bool processFound, string keyString)
        {
            if (processFound == false)
            {
                ProcessFileList.Add(new ProcessData
                {
                    Time = processTime,
                    Length = processLength,
                    Name = processName,
                    Operation = processOperation,
                    PID = processPID,
                    Path = processPath,
                    ProcessKey = keyString,
                    FileName = processFileName
                });
            }
            
        }

        //Append the length value
        private static bool AppendLength(List<ProcessData> fileList, int length, string keyString)
        {
            bool processFound = false;
            foreach (var item in fileList)
            {
                if (item.ProcessKey == keyString)
                {

                    item.Length += length;
                    processFound = true;
                    break;
                }

            }

            return processFound;
        }

        private void FilterOnCombobox(List<ProcessData> ProcessFileList, List<ProcessData> ProcessIDList, int[] topLengths, int[] topIDLengths, string[] topFileNames, string[] topProcessID)
        {


            if (OperationComboBox.SelectedItem == null)
            {
                int i = operationList.FindIndex(x => x.StartsWith("WriteFile"));
                if (operationList.Contains("WriteFile"))
                {
                    OperationComboBox.SelectedIndex = i;
                }
                else
                {
                    OperationComboBox.SelectedIndex = 0;
                }
            }
            
            string operationName = OperationComboBox.SelectedItem.ToString();
            OperationComboBox_SelectedIndexChanged(ProcessFileList, ProcessIDList);
            GetDataForCharts(ProcessFileList, ProcessIDList, topLengths, topIDLengths, topFileNames, topProcessID);

        }

        private void SortedFileList(List<ProcessData> ProcessFileList)
        {
            List<ProcessData> SortedFileList = ProcessFileList.OrderByDescending(o => o.Length).ToList();

            DataTable filteredTable = new DataTable();
            
            filteredTable.Columns.Add("Process Name");
            filteredTable.Columns.Add("PID");
            filteredTable.Columns.Add("Time");
            filteredTable.Columns.Add("File Name");
            
            filteredTable.Columns.Add("Operation");
            filteredTable.Columns.Add("Length");
            filteredTable.Columns.Add("Path");

            for (int i = 0; i < SortedFileList.Count; i++)
            {
                filteredTable.Rows.Add(
                                
                                SortedFileList[i].Name,
                                SortedFileList[i].PID,
                                SortedFileList[i].FileName,
                                SortedFileList[i].Time,
                                SortedFileList[i].Operation,
                                SortedFileList[i].Length,
                                SortedFileList[i].Path
                              );
            }
            dataGridView1.DataSource = filteredTable;

        }
        private void SortedProcessList(List<ProcessData> ProcessIDList)
        {
           

            List<ProcessData> SortedProcessList = ProcessIDList.OrderByDescending(o => o.Length).ToList();

            DataTable filteredTableProcesses = new DataTable();
            
            filteredTableProcesses.Columns.Add("Process Name");
            filteredTableProcesses.Columns.Add("PID");
            filteredTableProcesses.Columns.Add("Time");
            filteredTableProcesses.Columns.Add("File Name");
            
            filteredTableProcesses.Columns.Add("Operation");
            filteredTableProcesses.Columns.Add("Length");
            filteredTableProcesses.Columns.Add("Path");

            for (int i = 0; i < SortedProcessList.Count; i++)
            {
                filteredTableProcesses.Rows.Add(
                               
                                SortedProcessList[i].Name,
                                SortedProcessList[i].PID,
                                SortedProcessList[i].FileName,
                                SortedProcessList[i].Time,
                                SortedProcessList[i].Operation,
                                SortedProcessList[i].Length,
                                SortedProcessList[i].Path
                              );
            }
            dataGridViewProcesses.DataSource = filteredTableProcesses;
        }

        //Add the top ten files and lengths
        private void GetDataForCharts(List<ProcessData> ProcessFileList, List<ProcessData> ProcessIDList, int[] topLengths, int[] topIDLengths, string[] topFileNames, string[] topProcessID)
        {

            string operationValue = OperationComboBox.SelectedItem.ToString();

            var query = from ProcessData data in ProcessFileList
                        where data.Operation == operationValue
                        orderby data.Length descending
                        select data;
            var topTenIDList = (query.OrderByDescending(i => i.Length).Take(10));

            var query2 = from ProcessData data in ProcessIDList
                         where data.Operation == operationValue
                         orderby data.Length descending
                         select data;
            var topTenList = (query2.OrderByDescending(i => i.Length).Take(10));

           

            int topListCounter = 0;
            foreach (var item in topTenIDList)
            {
                        topLengths[topListCounter] = Convert.ToInt32(item.Length);
                        topFileNames[topListCounter] = item.FileName;

                topListCounter++;
            }

            int topProcessCounter = 0;
            foreach (var item in topTenList)
            {
                        //Convert byte to kilobyte
                        topIDLengths[topProcessCounter] = Convert.ToInt32(item.Length);
                        topProcessID[topProcessCounter] = item.Name + " " + item.PID;

              
                topProcessCounter++;

            }
            
        }

        private void PopulateChart(int[] topLengths, int[] topIDLengths, string[] topProcessNames, string[] topProcessID)
        {
            //BarChart1.Series.Add(series);
            BarChart1.Series[0].ChartType = SeriesChartType.RangeColumn;
            BarChart1.Series[0].Points.DataBindXY(new[] {topProcessNames[0], topProcessNames[1],topProcessNames[2],topProcessNames[3],topProcessNames[4],
                                                         topProcessNames[5], topProcessNames[6],topProcessNames[7],topProcessNames[8],topProcessNames[9]},
                                                  new[] {topLengths[0], topLengths[1], topLengths[2], topLengths[3], topLengths[4],
                                                         topLengths[5], topLengths[6], topLengths[7], topLengths[8], topLengths[9], });
            BarChart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            BarChart1.ChartAreas[0].AxisX.Interval = 1;
            BarChart1.Legends[0].Enabled = true;
            BarChart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            BarChart1.ChartAreas[0].AxisY.Title = "Bytes Used";
            //BarChart1.ChartAreas[0].AxisY.TitleFont="Arial", 16, FontStyle.Bold;
            BarChart1.Legends[0].Enabled = true;

            //3D Pie chart
            pieChart1.Series[0].ChartType = SeriesChartType.Pie;
            pieChart1.Series[0].Points.DataBindXY(topProcessID, topIDLengths);
            pieChart1.Legends[0].Enabled = true;
            pieChart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }

        private void ExitButton(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void SaveFileOperationToXML()
        {
            MessageBox.Show("Save files accessed, based on operation, to XML format ?");
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "XML (*.xml)|*.xml",
                FileName = ""
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                Deleteifexists(filename);

                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);
                XmlElement element0 = doc.CreateElement("ProcmonCSV");
                doc.AppendChild(element0);

                int icols = dataGridView1.Columns.Count;
                foreach (DataGridViewRow drow in this.dataGridView1.Rows)
                {
                    XmlElement Msg = doc.CreateElement("Msg");
                    Msg.SetAttribute("Date", DateTime.Now.ToShortDateString());
                    Msg.SetAttribute("Time", "");
                    element0.AppendChild(Msg);

                    XmlElement Layer = doc.CreateElement("LayerName");
                    Layer.SetAttribute("Name", "Core");
                    Msg.AppendChild(Layer);

                    XmlElement SourceLayer = doc.CreateElement("SourceLayer");
                    SourceLayer.SetAttribute("Name", "PML");
                    Layer.AppendChild(SourceLayer);

                    XmlElement Message = doc.CreateElement("Message");
                    Layer.AppendChild(Message);

                    for (int i = 0; i <= icols - 2; i++)
                    {
                        Message.SetAttribute(dataGridView1.Columns[i].Name, drow.Cells[i].Value.ToString());
                        Msg.SetAttribute("Time", drow.Cells[3].Value.ToString());
                    }

                    
                }

                doc.Save(filename);


            }
        }
        private void SaveProcessesToXML()
        {
            MessageBox.Show("Save processes, based on operation, to XML format ?");
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "XML (*.xml)|*.xml",
                FileName = ""
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                Deleteifexists(filename);

                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);
                XmlElement element0 = doc.CreateElement("ProcmonCSVprocess");
                doc.AppendChild(element0);

                int icols = dataGridView1.Columns.Count;
                foreach (DataGridViewRow drow in this.dataGridViewProcesses.Rows)
                {
                    XmlElement Msg = doc.CreateElement("Msg");
                    Msg.SetAttribute("Date", DateTime.Now.ToShortDateString());
                    Msg.SetAttribute("Time", "");
                    element0.AppendChild(Msg);

                    XmlElement Layer = doc.CreateElement("LayerName");
                    Layer.SetAttribute("Name", "Core");
                    Msg.AppendChild(Layer);

                    XmlElement SourceLayer = doc.CreateElement("SourceLayer");
                    SourceLayer.SetAttribute("Name", "PML");
                    Layer.AppendChild(SourceLayer);

                    XmlElement Message = doc.CreateElement("Message");
                    Layer.AppendChild(Message);

                    for (int i = 0; i <= icols - 2; i++)
                    {
                        Message.SetAttribute(dataGridView1.Columns[i].Name, drow.Cells[i].Value.ToString());
                        Msg.SetAttribute("Time", drow.Cells[3].Value.ToString());
                    }


                }

                doc.Save(filename);


            }
        }
        private void SaveAllDataToXML()
        {
            MessageBox.Show("Save ALL file data to XML format ?");
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "XML (*.xml)|*.xml",
                FileName = ""
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                Deleteifexists(filename);

                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);
                XmlElement element0 = doc.CreateElement("ProcmonCsvAllData");
                doc.AppendChild(element0);
                int counter = 0;
                int icols = dataGridViewAllData.Columns.Count;
                foreach (DataGridViewRow drow in this.dataGridViewAllData.Rows)
                {
                    XmlElement msg = doc.CreateElement("Msg");
                    msg.SetAttribute("Date", DateTime.Now.ToShortDateString());
                    msg.SetAttribute("Time", drow.Cells[0].Value.ToString());

                    XmlElement layer = doc.CreateElement("Layer");
                    layer.SetAttribute("Name", "CORE");

                    XmlElement slayer = doc.CreateElement("SourceLayer");
                    slayer.SetAttribute("Name", "PML");
                                

                    XmlElement message = doc.CreateElement("Message");
                    message.SetAttribute("ProcessName", drow.Cells[1].Value.ToString());
                    message.SetAttribute("ProcessID", drow.Cells[2].Value.ToString());
                    message.SetAttribute("FileName", drow.Cells[3].Value.ToString());
                    message.SetAttribute("Operation", drow.Cells[4].Value.ToString());
                    message.SetAttribute("Length", drow.Cells[5].Value.ToString());
                    message.SetAttribute("Path", drow.Cells[6].Value.ToString());

                    msg.AppendChild(layer);
                    msg.AppendChild(slayer);
                    msg.AppendChild(message);

                    element0.AppendChild(msg);
                    counter++;
                    if (counter == dataGridViewAllData.RowCount -1)
                    {
                        break;
                    }
                }
               

                doc.Save(filename);


            }
        }
        private void SaveCSV(object sender, EventArgs e)
        {
            SaveToCSV(dataGridView1);
            SaveToCSV(dataGridViewProcesses);

        }

        private void OperationComboBox_SelectedIndexChanged(List<ProcessData> dataFileList, List<ProcessData> dataProcessesList)
        {
            string operationValue = OperationComboBox.SelectedItem.ToString();
                var query = from ProcessData data in dataFileList
                            where data.Operation == operationValue
                            orderby data.Length descending
                            select data;
                
                //files
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = query.ToList();

            var query2 = from ProcessData data in dataProcessesList
                         where data.Operation == operationValue
                         orderby data.Length descending
                         select data;
            var topTenList = (query2.OrderByDescending(i => i.Length));
            //processes 
            //dataProcessesList = query.GroupBy(x => x.Name).Select(x => x.First()).ToList();
                //dataGridViewProcesses.DataSource = null;
            dataGridViewProcesses.DataSource = topTenList.ToList();
            
        }

        private void SaveXML(object sender, EventArgs e)
        {
            SaveFileOperationToXML();
            SaveAllDataToXML();
            SaveProcessesToXML();
        }

       
    }
}
