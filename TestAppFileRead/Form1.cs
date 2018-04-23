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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Get the file path selected
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
   
            List<string> PIDlist = new List<string>();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Time");
            dataTable.Columns.Add("Process Name");
            dataTable.Columns.Add("PID");           
            dataTable.Columns.Add("Path");
            dataTable.Columns.Add("Length");
            
            string filePath = textBoxFilePath.Text;
            StreamReader streamReader = new StreamReader(filePath);
            string[] totalData = new string[File.ReadAllLines(filePath).Length];
            totalData = streamReader.ReadLine().Split('"');
            while (!streamReader.EndOfStream)
            {
                
                totalData = streamReader.ReadLine().Split('"');
               
                //get value of length
                var match = regex.Match(totalData[13]);
                if(match.Success)
                {
                    totalData[14] = match.Groups[1].Value;
                }

                //add PID value to list
                PIDlist.Add(totalData[5]);
                
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

            var series = new Series("Process List");


            //find the rows with the matching PID's and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dictionary<string, List<int>> data = new Dictionary<string, List<int>>();
            try
            {
                int length = 0;
                int totalLength = 0;
                string pid = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    pid = row.Cells[5].ToString();
                    length = Convert.ToInt32(row.Cells[14]);
                    
                    if (data.ContainsKey(pid))
                        data[pid].Add(length);
                    else data.Add(pid, new List<int>(new int[] { length }));
                }
               
                foreach (string pidKey in data.Keys)
                {                    
                        totalLength += length;
                        pid = pidKey;

                }
                
                //need to sort the pid's by largest lengths and display the top ten.
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            series.Points.DataBindXY(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new[] { 100, 200, 90, 150, 20, 330, 500, 120, 50, 210 });
            BarChart1.Series.Add(series);
        }

    



        private void barChartButton_Click(object sender, EventArgs e)
        {

            var series = new Series("Process List");

            // First parameter is X-Axis and Second is Collection of Y- Axis
            series.Points.DataBindXY(new[] {1,2,3,4,5,6,7,8,9,10 }, new[] { 100, 200, 90, 150,20,330,500,120,50,210 });
            BarChart1.Series.Add(series);

        }
       
    }
}
