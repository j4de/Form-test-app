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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        //Only setup to Read in the data from a CSV file and display in the table
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

            //Get the size of the file and append magabyte or kilobyte
            string megabyteOrKilobyte = "";
            FileInfo f = new FileInfo(filePath);
            long fileSize = f.Length;
            if (fileSize > 1024)
            {
                fileSize = fileSize / 1024;
                fileSize.ToString();
                megabyteOrKilobyte = fileSize + "Megabytes";
            }
            else
            {
                fileSize.ToString();
                megabyteOrKilobyte = fileSize + "Kilobyte";
            }
            //Confirm if the file size is exceptable to load
            if (MessageBox.Show("The file size is " + megabyteOrKilobyte + ""+". Do you want to load it?", " Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

                        //display relevant values
                        dataTable.Rows.Add(
                                            totalData[1], //time 
                                            totalData[3], //process name 
                                            totalData[5], //PID
                                            totalData[9],//path
                                            totalData[14] //length
                                          );
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dataTable;

                    //var results = from DataRow myRow in dataTable.Rows
                    //              where (int)myRow[14] == 1
                    //              select myRow;

                    FindLengthForEachPID();

                    var series = new Series("Process List");
                    series.Points.DataBindXY(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new[] { 100, 200, 90, 150, 20, 330, 500, 120, 50, 210 });
                    BarChart1.Series.Add(series);
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        

           
        }

        private void FindLengthForEachPID()
        {
            //find the rows with the matching PID's and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dictionary<string, List<int>> data = new Dictionary<string, List<int>>();
            try
            {
                string stringLength = "";
                int length = 0;
                int totalLength = 0;
                string pid = "";

                //have to put the length value into a string and take out any
                //commas before converting it to an int.
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    pid = row.Cells[2].Value.ToString();
                    stringLength = row.Cells[4].Value.ToString();
                    stringLength = stringLength.Replace(",", "");
                    length = Convert.ToInt32(stringLength);

                    if (data.ContainsKey(pid))
                    {
                        data[pid].Add(length);
                        //totalLength += length;
                    }
                    else data.Add(pid, new List<int>(new int[] { length }));

                    totalLength += length;


                }
                GetMaxValue();

                //foreach (var pidKey in data.Keys)
                //{
                //    if(pid == pidKey)
                //    totalLength += length;


                //}

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //Get the highest length value
        private void GetMaxValue()
        {
            int max = 0;
            string str = "";
            int strValue = 0;
            string maxMessage = "The largest usage is ";
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                str = dataGridView1.Rows[i].Cells[4].Value.ToString();
                str = str.Replace(",", "");
                strValue = Convert.ToInt32(str);

                if (max < strValue)
                {
                    max = strValue;
                }
            }
            maxMessage = maxMessage + max.ToString();
            label1.Text = maxMessage;
        }



        //Working Chart sample
        private void barChartButton_Click(object sender, EventArgs e)
        {

            var series = new Series("Process List");

            // First parameter is X-Axis and Second is Collection of Y- Axis
            series.Points.DataBindXY(new[] {1,2,3,4,5,6,7,8,9,10 }, new[] { 10, 20, 9, 15,2,33,50,12,5,21 });
            BarChart1.Series.Add(series);

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
