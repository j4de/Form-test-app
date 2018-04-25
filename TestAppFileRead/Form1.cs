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
        public static Regex regex = new Regex(@"Length:\s*(\d+\,+[0-9]+[0-9]|\d+\,+[0-9]|[0-9]+[0-9]|\d)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

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

                        //get value of length
                        var match = regex.Match(totalData[13]);
                        if (match.Success)
                        {
                            totalData[14] = match.Groups[1].Value;
                        }
                        if (totalData[1] != null)
                        {
                            //display relevant values
                            dataTable.Rows.Add(
                                                totalData[1], //time 
                                                totalData[3], //process name 
                                                totalData[5], //PID
                                                totalData[9],//path
                                                totalData[14] //length
                                              );
                        }
                        
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dataTable;

                    FindLengthForEachProcess();
                }
            }
            else
            {
                dataGridView1.DataSource = null;
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
            string stringLength = "";
            int length = 0;
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

                    processName = row.Cells[1].Value.ToString().ToLower();
                    processName = processName.Remove(processName.IndexOf("."));

                    //have to put the length value into a string and take out any
                    //commas before converting it to an int.
                    stringLength = row.Cells[4].Value.ToString();
                    stringLength = stringLength.Replace(",", "");
                    length = Convert.ToInt32(stringLength);

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
                        if (row != null)
                        {
                            ProcessList.Add(new ProcessData
                            {
                                ProcessLength = length,
                                ProcessName = processName
                            });
                        }

                    }

                    //for testing only
                    loopCounter++;
                    if (loopCounter == dataGridView1.RowCount -1)
                        break;
                }
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
            //Bar chart with array values
            var series = new Series("Process List");
            series.Points.DataBindXY(new[] {topProcessNames[0], topProcessNames[1],topProcessNames[2],topProcessNames[3],topProcessNames[4],
                                            topProcessNames[5], topProcessNames[6],topProcessNames[7],topProcessNames[8],topProcessNames[9]},
                                     new[] {topLengths[0], topLengths[1], topLengths[2], topLengths[3], topLengths[4],
                                            topLengths[5], topLengths[6], topLengths[7], topLengths[8], topLengths[9], });
            BarChart1.Series.Add(series);

            pieChart1.Series[0].ChartType = SeriesChartType.Pie;
            pieChart1.Series[0].Points.DataBindXY(topProcessNames, topLengths);
            pieChart1.Legends[0].Enabled = true;
            pieChart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }

        
    }
}
