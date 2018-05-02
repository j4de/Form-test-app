using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestAppFileRead
{
    public partial class Form1 : Form
    {
        //Procmon data
        private int PM_TimeOfDay = 1;
        private int PM_ProcessName = 3;
        private int PM_PID = 5;
        private int PM_Path = 9;
        private int PM_FileName = 10;
        //private int PM_Offset = 12;
        private int PM_Detail = 13;
        private int PM_Length = 14;

        //DataGridView data
        private int DG_TimeOfDay = 0;
        private int DG_Name = 1;
        private int DG_PID = 2;
        private int DG_FileName = 3;
        //private int DG_Offset = 4;
        private int DG_Length = 4;
        private int DG_Path = 5;


        //To parse the Length value from the column => details
        public static Regex regexLength = new Regex(@"Length:\s*(?<getLengthNum>\d+(,\d+)*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //public static Regex regexOffset = new Regex(@"Offset:\s*(?<getOffsetNum>\d+(,\d+)*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public Form1()
        {
            InitializeComponent();
        }

        //Get the file path selected.
        //Filter setup for CSV files in Form1.designer.cs
        private void getFileButton_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = (openFileDialog1.FileName);
            }

        }


        //Only setup to Read in the data from a Procmon CSV file
        private void displayButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Time");
            dataTable.Columns.Add("Process Name");
            dataTable.Columns.Add("PID");
            dataTable.Columns.Add("File Name");
            //dataTable.Columns.Add("Offset");
            dataTable.Columns.Add("Length");
            dataTable.Columns.Add("Path");

            string filePath = textBoxFilePath.Text;
            StreamReader streamReader = new StreamReader(filePath);
            string[] totalData = new string[File.ReadAllLines(filePath).Length];

            string megabyteOrKilobyte;
            long fileSize;
            long stringSize;
            long progress = 0;
            

            FindFileSize(filePath, out megabyteOrKilobyte, out fileSize);

            //Confirm if the file size is exceptable to load
            //if (MessageBox.Show("The file size is " + megabyteOrKilobyte + "" + ". Do you want to load it?", " Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
                //progress bar
                loadingForm load = new loadingForm(megabyteOrKilobyte, dataGridView1, this, fileSize, progress);
                load.Show();
                //Reset the data grid
                dataGridView1.DataSource = null;
                if (fileSize != 0)
                {
                    string line = streamReader.ReadLine();
                    totalData = line.Split('"');
                    while (!streamReader.EndOfStream)
                    {

                        totalData = ParseProcmonData(streamReader);
                        //get the offset value

                        stringSize = line.Length;
                        progress += stringSize;
                        Update();
                        load.setprogress(progress);
                        if (load.iscanceled())
                        {
                            load.Close();
                            break;
                        }
                        Application.DoEvents();

                    
                    dataTable.Rows.Add(
                                    totalData[PM_TimeOfDay],
                                    totalData[PM_ProcessName],
                                    totalData[PM_PID],
                                    totalData[PM_FileName],
                                    //totalData[PM_Offset],
                                    totalData[PM_Length],
                                    totalData[PM_Path]
                                  );

                }
                
                //If file load is not cancelled
                ProcessFileData(dataTable, load);
                
            }
            //}
            //else
            //{
            //   
            //}
           

        }

      
        private void ProcessFileData(DataTable dataTable, loadingForm load)
        {
            if (!load.iscanceled())
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dataTable;
                FindLengthForEachProcess();
                load.Close();
            }
        }

        private string[] ParseProcmonData(StreamReader streamReader)
        {
            string[] totalData = streamReader.ReadLine().Split('"');
            foreach (var item in totalData)
            {
                

                totalData[PM_TimeOfDay] = totalData[PM_TimeOfDay].Substring(0, 8);
                
                //To  deal with an illigal symbol
                if (totalData[PM_Path].ToString() == @"\Device\HarddiskVolume3촧"|| totalData[PM_Path].ToString() == @"\Device\HarddiskVolume3엡\u0003")
                {
                    totalData[PM_FileName] = "Device.HarddiskVolume3";
                }
                else
                totalData[PM_FileName] = Path.GetFileName(totalData[PM_Path]).ToString();

                try
                {

                    //get value of length and offset and remove any comma's
                    var match = regexLength.Match(totalData[PM_Detail]);
                    if (match.Success)
                    {
                        totalData[PM_Length] = match.Groups["getLengthNum"].Value;
                        totalData[PM_Length] = totalData[PM_Length].Replace(",", "");
                    }
                    //var match2 = regexOffset.Match(totalData[PM_Detail]);
                    //if (match2.Success)
                    //{
                    //    totalData[PM_Offset] = match2.Groups["getOffsetNum"].Value;
                    //    totalData[PM_Offset] = totalData[PM_Offset].Replace(",", "");
                    //}
                }
                catch (IncorrectFormatException exc)
                {
                    MessageBox.Show(exc.Message);
                }
                //Add length together for the same process
                //if (totalData[PM_PID] == item. )
                //{

                //}
            }
            return totalData;
        }

        private void SaveToCSV(DataGridView DGV)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Output.csv";
            int counter = 0;


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Data will be exported and you will be notified when it is ready.");
                filename = sfd.FileName;
                Deleteifexists(filename);
                int columnCount = DGV.ColumnCount;
                string columnNames = "";

                SaveOutput(DGV, filename, ref counter, columnCount, ref columnNames);
                MessageBox.Show("Your file was generated and its ready for use.");
            }
        }

        private static void SaveOutput(DataGridView DGV, string filename, ref int counter, int columnCount, ref string columnNames)
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
                    if (i == DGV.RowCount - 1)
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
        private static void FindFileSize(string filePath, out string megabyteOrKilobyte, out long fileSize)
        {
            //Get the size of the file and append Gigabyte, magabyte or kilobyte
            megabyteOrKilobyte = "";
            FileInfo f = new FileInfo(filePath);
            fileSize = f.Length;
            //if (fileSize > 1073741824)
            //{
            //    fileSize = fileSize / 1073741824;
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + " Gigabytes";
            //}
            //else if (fileSize > 1048576)
            //{
            //    fileSize = fileSize / 1048576;
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + " Megabytes";
            //}
            //else if (fileSize > 1024)
            //{
            //    fileSize = fileSize / 1024;
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + " Kilobytes";
            //}
            //else
            //{
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + "bytes";
            //}
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
            //int processOffset = 0;
            string processName = "";
            string processFileName = "";
            string processPath = "";
            string processPID = "";
            bool processFound = false;
            int loopCounter = 0;
            string processKeyString = "";
            //Var for bar chart
            string processKeyID = "";
            bool processIDFound = false;

            //find the rows with the matching process key and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    processFileName = row.Cells[DG_FileName].Value.ToString();
                    processName = row.Cells[DG_Name].Value.ToString();
                    processPID = row.Cells[DG_PID].Value.ToString();
                    processPath = row.Cells[DG_Path].Value.ToString();

                    //Some offset values have a value of -1 which throws an exception
                    //if (row.Cells[DG_Offset].Value.ToString() == ",")
                    //{
                    //    processOffset = -1;
                    //}
                    //else
                    //{
                    //    processOffset = Convert.ToInt32(row.Cells[DG_Offset].Value);
                    //}
                    processLength = Convert.ToInt32(row.Cells[DG_Length].Value);

                    processKeyString = processName + "|" + processPID + "|" + processPath;
                    //If the process processKey is found in the list
                    //append the length value
                    processFound = false;
                    foreach (var item in ProcessFileList)
                    {
                        if (item.ProcessKey == processKeyString)
                        {

                            item.ProcessLength += processLength;
                            processFound = true;
                            break;
                        }


                    }

                    //else add a new process to the list
                    if (processFound == false)
                    {
                        ProcessFileList.Add(new ProcessData
                        {
                            ProcessLength = processLength,
                            ProcessName = processName,
                            //ProcessOffset = processOffset,
                            ProcessPID = processPID,
                            ProcessPath = processPath,
                            ProcessKey = processKeyString,
                            ProcessFileName = processFileName
                        });
                    }
                    //########################################################

                    processKeyID = processName + "|" + processPID;
                    //If the process processKey is found in the list
                    //append the length value
                    processIDFound = false;
                    foreach (var item in ProcessIDList)
                    {
                        if (item.ProcessKey == processKeyID)
                        {
                            item.ProcessLength += processLength;
                            processIDFound = true;
                            break;
                        }
                    }
                    if (processIDFound == false)
                    {
                        ProcessIDList.Add(new ProcessData
                        {
                            ProcessLength = processLength,
                            ProcessName = processName,
                            //ProcessOffset = processOffset,
                            ProcessPID = processPID,
                            ProcessPath = processPath,
                            ProcessKey = processKeyID,
                            ProcessFileName = processFileName
                        });
                    }
                    //#############################################################

                    //To ensure it doesn't spill over and cause 
                    //an null object reference
                    loopCounter++;
                    if (loopCounter == dataGridView1.RowCount - 1)
                        break;
                }
                SortedFileList(ProcessFileList);

                totalProcessesLabel.Text = ProcessFileList.Count().ToString();

                GetDataForCharts(ProcessFileList, ProcessIDList, topLengths, topIDLengths, topFileNames, topProcessID);

                PopulateChart(topLengths, topIDLengths, topFileNames, topProcessID);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }


        }

        private void SortedFileList(List<ProcessData> ProcessFileList)
        {
            List<ProcessData> SortedFileList = ProcessFileList.OrderByDescending(o => o.ProcessLength).ToList();

            DataTable filteredTable = new DataTable();
            filteredTable.Columns.Add("Process Name");
            filteredTable.Columns.Add("PID");
            filteredTable.Columns.Add("File Name");
            //filteredTable.Columns.Add("Offset");
            filteredTable.Columns.Add("Length");
            filteredTable.Columns.Add("Path");

            for (int i = 0; i < SortedFileList.Count; i++)
            {
                filteredTable.Rows.Add(
                                SortedFileList[i].ProcessName,
                                SortedFileList[i].ProcessPID,
                                SortedFileList[i].ProcessFileName,
                                //SortedFileList[i].ProcessOffset,
                                SortedFileList[i].ProcessLength,
                                SortedFileList[i].ProcessPath
                              );
            }
            dataGridView1.DataSource = filteredTable;
        }

        //Add the top ten files and lengths to the 
        private void GetDataForCharts(List<ProcessData> ProcessFileList, List<ProcessData> ProcessIDList, int[] topLengths, int[] topIDLengths, string[] topFileNames, string[] topProcessID)
        {
            var topTenList = (ProcessFileList.OrderByDescending(i => i.ProcessLength).Take(10));
            var topTenIDList = (ProcessIDList.OrderByDescending(i => i.ProcessLength).Take(10));
            int topListCounter = 0;

            foreach (var item in topTenList)
            {
                for (int i = 0; i < topLengths.Length; i++)
                {

                    if (topListCounter == i)
                    {
                        //convert ProcessLength to kilobytes
                        topLengths[i] = Convert.ToInt32(item.ProcessLength / 1024);
                        topFileNames[i] = item.ProcessFileName;

                    }

                }
                topListCounter++;

            }

            int topProcessCounter = 0;
            foreach (var item in topTenIDList)
            {
                for (int i = 0; i < topIDLengths.Length; i++)
                {

                    if (topProcessCounter == i)
                    {
                        topIDLengths[i] = Convert.ToInt32(item.ProcessLength);
                        topProcessID[i] = item.ProcessName + " " + item.ProcessPID;

                    }

                }
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

        private void SaveButton(object sender, EventArgs e)
        {
            SaveToCSV(dataGridView1);
        }
    }
}
