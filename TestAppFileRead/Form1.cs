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
        private int PM_Offset = 12;
        private int PM_Detail = 13;
        private int PM_Length = 14;

        //DataGridView data
        private int DG_TimeOfDay = 0;
        private int DG_Name = 1;
        private int DG_PID = 2;
        private int DG_Path = 3;
        private int DG_Offset = 4;
        private int DG_Length = 5;


        //To parse the Length value from the column => details
        public static Regex regexLength = new Regex(@"Length:\s*(?<getLengthNum>\d+(,\d+)*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        public static Regex regexOffset = new Regex(@"Offset:\s*(?<getOffsetNum>\d+(,\d+)*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

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


        //Only setup to Read in the data from a Procmon CSV file and display in the table
        private void displayButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Time");
            dataTable.Columns.Add("Process Name");
            dataTable.Columns.Add("PID");
            dataTable.Columns.Add("Path");
            dataTable.Columns.Add("Offset");
            dataTable.Columns.Add("Length");

            string filePath = textBoxFilePath.Text;
            StreamReader streamReader = new StreamReader(filePath);
            string[] totalData = new string[File.ReadAllLines(filePath).Length];

            string megabyteOrKilobyte;
            long fileSize;

            FindFileSize(filePath, out megabyteOrKilobyte, out fileSize);

            //Confirm if the file size is exceptable to load
            if (MessageBox.Show("The file size is " + megabyteOrKilobyte + "" + ". Do you want to load it?", " Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //progress bar
                loadingForm load = new loadingForm(megabyteOrKilobyte, dataGridView1, this, fileSize);
                load.Show();

                if (fileSize != 0)
                {
                    totalData = streamReader.ReadLine().Split('"');
                    while (!streamReader.EndOfStream)
                    {
                        totalData = ParseProcmonData(streamReader);
                        //get the offset value

                        System.Threading.Thread.Sleep(100);
                        //load.setprogress;
                        Application.DoEvents();

                        if (totalData[1] != null)
                        {
                            //display relevant values
                            dataTable.Rows.Add(
                                                totalData[PM_TimeOfDay],
                                                totalData[PM_ProcessName],
                                                totalData[PM_PID],
                                                totalData[PM_Path],
                                                totalData[PM_Offset],
                                                totalData[PM_Length]
                                              );
                        }

                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dataTable;

                    //Generate a new CSV file with just the data we need
                    SaveToCSV(dataGridView1);
                    
                    FindLengthForEachProcess();
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }

        }

        private string[] ParseProcmonData(StreamReader streamReader)
        {
            string[] totalData = streamReader.ReadLine().Split('"');
            
            totalData[PM_TimeOfDay] = totalData[PM_TimeOfDay].Substring(0, 8);
            try
            {
                
                //get value of length and offset and remove any comma's
                var match = regexLength.Match(totalData[PM_Detail]);
                if (match.Success)
                {
                    totalData[PM_Length] = match.Groups["getLengthNum"].Value;
                    totalData[PM_Length] = totalData[PM_Length].Replace(",", "");
                }
                var match2 = regexOffset.Match(totalData[PM_Detail]);
                if (match2.Success)
                {
                    totalData[PM_Offset] = match2.Groups["getOffsetNum"].Value;
                    totalData[PM_Offset] = totalData[PM_Offset].Replace(",", "");
                }
            }
            catch (IncorrectFormatException exc)
            {
                MessageBox.Show(exc.Message);
            }

            return totalData;
        }

        private void SaveToCSV(DataGridView DGV)
        {
            //rewrite using streamerWriter ===> look up past work.
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Output.csv";
            int counter = 0;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Data will be exported and you will be notified when it is ready.");
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
                int columnCount = DGV.ColumnCount;
                string columnNames = "";
                string[] output = new string[DGV.RowCount + 1];
                for (int i = 0; i < columnCount; i++)
                {
                    columnNames += DGV.Columns[i].Name.ToString() + ",";
                }
                output[0] += columnNames;
                for (int i = 1; (i - 1) < DGV.RowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        
                        output[i] += DGV.Rows[i - 1].Cells[j].Value.ToString() + ",";
                        counter++;
                    }
                    //break out of loop to avoid null object reference
                    if (i == DGV.RowCount - 1)
                        break;
                }
                File.WriteAllLines(sfd.FileName, output, Encoding.UTF8);
                MessageBox.Show("Your file was generated and its ready for use.");
            }
        }
        

        //Convert file size for user information
        private static void FindFileSize(string filePath, out string megabyteOrKilobyte, out long fileSize)
        {
            //Get the size of the file and append Gigabyte, magabyte or kilobyte
            megabyteOrKilobyte = "";
            FileInfo f = new FileInfo(filePath);
            fileSize = f.Length;
            if (fileSize > 1073741824)
            {
                fileSize = fileSize / 1073741824;
                fileSize.ToString();
                megabyteOrKilobyte = fileSize + " Gigabytes";
            }
            else if (fileSize > 1048576)
            {
                fileSize = fileSize / 1048576;
                fileSize.ToString();
                megabyteOrKilobyte = fileSize + " Megabytes";
            }
            else if (fileSize > 1024)
            {
                fileSize = fileSize / 1024;
                fileSize.ToString();
                megabyteOrKilobyte = fileSize + " Kilobytes";
            }
            else
            {
                fileSize.ToString();
                megabyteOrKilobyte = fileSize + "bytes";
            }
        }

        //Calculate the size/length of each process
        private void FindLengthForEachProcess()
        {
            var ProcessList = new List<ProcessData>();
            
            int[] topLengths = new int[10];
            string[] topProcessNames = new string[10];
            
            int processLength = 0;
            int processOffset = 0;
            string processName = "";
            string processPath = "";
            string processPID = "";
            bool processFound = false;
            int loopCounter = 0;
            string processKeyString = "";

            //find the rows with the matching process name and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    processName = row.Cells[DG_Name].Value.ToString();
                    processPID = row.Cells[DG_PID].Value.ToString();
                    processPath = row.Cells[DG_Path].Value.ToString();
                    processOffset = Convert.ToInt32(row.Cells[DG_Offset].Value);
                    processLength = Convert.ToInt32(row.Cells[DG_Name].Value);
                    
                   // processKeyString = processName+"|"+
                    //If the process name is found in the list
                    //append the length value
                    foreach (var item in ProcessList)
                    {
                        if (item.ProcessName == processName )
                        {
                            item.ProcessLength += processLength;
                            processFound = true;
                        }
                        else
                            processFound = false;
                    }

                    //else add a new process to the list
                    if (processFound == false)
                    {
                        ProcessList.Add(new ProcessData
                        {
                            ProcessLength = processLength,
                            ProcessName = processName,
                            ProcessOffset = processOffset
                        });
                    }
                    
                    //To ensure it doesn't spill over and cause 
                    //an null object reference
                    loopCounter++;
                    if (loopCounter == dataGridView1.RowCount -1)
                        break;
                }
                totalProcessesLabel.Text = ProcessList.Count().ToString();

                GetDataForCharts(ProcessList, topLengths, topProcessNames);           
                PopulateChart(topLengths, topProcessNames);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

           
        }



        private void GetDataForCharts(List<ProcessData> ProcessList, int[] topLengths, string[] topProcessNames)
        {
            var topTenList = (ProcessList.OrderByDescending(i => i.ProcessLength).Take(10));

            int topListCounter = 0;
            foreach (var item in topTenList)
            {
                for (int i = 0; i < topLengths.Length; i++)
                {
                    if (topListCounter == i)
                    {
                        //convert ProcessLength to kilobytes
                        topLengths[i] = Convert.ToInt32(item.ProcessLength / 1024);
                        topProcessNames[i] = item.ProcessName;
                    }

                }
                if (topListCounter == 0)
                {
                    processNameLabel.Text = item.ProcessName.ToString();
                    lengthLabel.Text = item.ProcessLength.ToString();
                }
                topListCounter++;

            }
        }

        private void PopulateChart(int[] topLengths, string[] topProcessNames)
        {
            //BarChart1.Series.Add(series);
            BarChart1.Series[0].ChartType = SeriesChartType.RangeColumn;
            BarChart1.Series[0].Points.DataBindXY(new[] {topProcessNames[0], topProcessNames[1],topProcessNames[2],topProcessNames[3],topProcessNames[4],
                                                         topProcessNames[5], topProcessNames[6],topProcessNames[7],topProcessNames[8],topProcessNames[9]},
                                                  new[] {topLengths[0], topLengths[1], topLengths[2], topLengths[3], topLengths[4],
                                                         topLengths[5], topLengths[6], topLengths[7], topLengths[8], topLengths[9], });
            BarChart1.Legends[0].Enabled = true;

            //3D Pie chart
            pieChart1.Series[0].ChartType = SeriesChartType.Pie;
            pieChart1.Series[0].Points.DataBindXY(topProcessNames, topLengths);
            pieChart1.Legends[0].Enabled = true;
            pieChart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }

        
    }
}
