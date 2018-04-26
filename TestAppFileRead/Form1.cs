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
        //To parse the Length value from the column => details
        public static Regex regexLength = new Regex(@"Length:\s*(\d+\,+[0-9]+[0-9]|\d+\,+[0-9]|[0-9]+[0-9]|\d)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //public static Regex regexOffset = new Regex(@"
        //                    Offset:\s*(\d{2,3}|\d{2}|\d\D\d{2,3})|
        //                    Offset:\s*(\d+\D\d{2,3})|
        //                    Offset:\s*(\d\D\d{2,3}\D\d{2,3})|
        //                    Offset:\s*(\d{1,3})", RegexOptions.CultureInvariant | RegexOptions.Compiled);

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
                if (fileSize != 0)
                {
                    totalData = streamReader.ReadLine().Split('"');
                    while (!streamReader.EndOfStream)
                    {
                        
                        totalData = streamReader.ReadLine().Split('"');
                        //format time value
                        totalData[1] = totalData[1].Substring(0,8);
                        try
                        {
                            //get value of length and remove the comma
                            var match = regexLength.Match(totalData[13]);
                            if (match.Success)
                            {
                                totalData[14] = match.Groups[1].Value;
                                totalData[14] = totalData[14].Replace(",", "");
                            }
                            //get the offset value
                            var match2 = regexOffset.Match(totalData[13]);
                            if (match2.Success)
                            {
                                totalData[12] = match.Groups[1].Value;
                                totalData[12] = totalData[12].Replace(",", "");
                            }

                        }
                        catch (IncorrectFormatException exc)
                        {
                            MessageBox.Show(exc.Message, caption: "Must be a Procmon file saved in the CSV format. ");
                        }
                        if (totalData[1] != null)
                        {
                            //display relevant values
                            dataTable.Rows.Add(
                                                totalData[1], //time 
                                                totalData[3], //process name 
                                                totalData[5], //PID
                                                totalData[9],//path
                                                 totalData[12],//offset
                                                totalData[14] //length
                                               
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
            
            int length = 0;
            int offset = 0;
            string processName = "";
            bool processFound = false;
            int loopCounter = 0;

            //find the rows with the matching process name and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    processName = row.Cells[1].Value.ToString();
                    //Remove entension, just for readability
                    processName = processName.Remove(processName.IndexOf("."));
                    length = Convert.ToInt32(row.Cells[4].Value);
                    offset = Convert.ToInt32(row.Cells[5].Value);

                    //If the process name is found in the list
                    //append the length value
                    foreach (var item in ProcessList)
                    {
                        if (item.ProcessName == processName)
                        {
                            item.ProcessLength += length;
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
                            ProcessLength = length,
                            ProcessName = processName,
                            Offset = offset
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
            var topTenList = (ProcessList.OrderByDescending(i => i.ProcessLength).Take(10)).Distinct();

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
