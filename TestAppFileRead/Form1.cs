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

namespace TestAppFileRead
{
    public partial class Form1 : Form
    {
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
            dataTable.Columns.Add("Operation");
            dataTable.Columns.Add("Path");
            dataTable.Columns.Add("Result");
            dataTable.Columns.Add("Detail1");
            dataTable.Columns.Add("Detail2");
            


            string filePath = textBoxFilePath.Text;
            StreamReader streamReader = new StreamReader(filePath);
            string[] totalData = new string[File.ReadAllLines(filePath).Length];
            totalData = streamReader.ReadLine().Split('"');
            while (!streamReader.EndOfStream)
            {
                //Regex reg = new Regex("\"([^\"]*?)\"");
                totalData = streamReader.ReadLine().Split('"');
                //need to account for commas in offset and length values, not returning correct results


                if (totalData[13].Contains("Offset: "))
                {
                    totalData[14] = totalData[13].Substring(8);
                }


                dataTable.Rows.Add( //totalData[0], 
                                    totalData[1], 
                                    //totalData[2], 
                                    totalData[3], 
                                    //totalData[4], 
                                    totalData[5], 
                                    //totalData[6],
                                    totalData[7],
                                    //totalData[8],
                                    totalData[9],
                                    //totalData[10],
                                    totalData[11],
                                    //totalData[12],
                                    totalData[13],
                                    totalData[14]
                                    );
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dataTable;
        }
    }
}
