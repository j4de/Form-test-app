namespace TestAppFileRead
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.totalProcessesLabel = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.getFileButton = new System.Windows.Forms.Button();
            this.displayButton = new System.Windows.Forms.Button();
            this.pieChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.exitButton = new System.Windows.Forms.Button();
            this.BarChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewProcesses = new System.Windows.Forms.DataGridView();
            this.OperationComboBox = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveXMLbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcesses)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Ivory;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.781971F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.51363F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.69826F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.63443F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.06064F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.69681F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.30524F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.241521F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.saveXMLbutton, 4, 14);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 6, 14);
            this.tableLayoutPanel1.Controls.Add(this.totalProcessesLabel, 2, 14);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilePath, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.getFileButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.displayButton, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.pieChart1, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.exitButton, 7, 14);
            this.tableLayoutPanel1.Controls.Add(this.BarChart1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewProcesses, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.OperationComboBox, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.337784F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.874499F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.66038F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.773585F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.981132F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.509434F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.35849F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.169867F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.9291F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.675568F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1034, 661);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.Green;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(761, 630);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(97, 28);
            this.saveButton.TabIndex = 18;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton);
            // 
            // totalProcessesLabel
            // 
            this.totalProcessesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProcessesLabel.AutoSize = true;
            this.totalProcessesLabel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.totalProcessesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalProcessesLabel.ForeColor = System.Drawing.Color.Black;
            this.totalProcessesLabel.Location = new System.Drawing.Point(157, 627);
            this.totalProcessesLabel.Margin = new System.Windows.Forms.Padding(0);
            this.totalProcessesLabel.Name = "totalProcessesLabel";
            this.totalProcessesLabel.Size = new System.Drawing.Size(161, 34);
            this.totalProcessesLabel.TabIndex = 15;
            this.totalProcessesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxFilePath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxFilePath, 3);
            this.textBoxFilePath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxFilePath.Location = new System.Drawing.Point(160, 25);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(461, 20);
            this.textBoxFilePath.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 8);
            this.dataGridView1.Location = new System.Drawing.Point(19, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.tableLayoutPanel1.SetRowSpan(this.dataGridView1, 3);
            this.dataGridView1.Size = new System.Drawing.Size(980, 240);
            this.dataGridView1.TabIndex = 4;
            // 
            // getFileButton
            // 
            this.getFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getFileButton.BackColor = System.Drawing.Color.SteelBlue;
            this.getFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getFileButton.Location = new System.Drawing.Point(19, 20);
            this.getFileButton.Name = "getFileButton";
            this.getFileButton.Size = new System.Drawing.Size(107, 25);
            this.getFileButton.TabIndex = 0;
            this.getFileButton.Text = "Load File";
            this.getFileButton.UseVisualStyleBackColor = false;
            this.getFileButton.Click += new System.EventHandler(this.GetFileButton);
            // 
            // displayButton
            // 
            this.displayButton.AutoSize = true;
            this.displayButton.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel1.SetColumnSpan(this.displayButton, 2);
            this.displayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayButton.Location = new System.Drawing.Point(864, 20);
            this.displayButton.Name = "displayButton";
            this.displayButton.Size = new System.Drawing.Size(135, 25);
            this.displayButton.TabIndex = 3;
            this.displayButton.Text = "Display Data";
            this.displayButton.UseVisualStyleBackColor = false;
            this.displayButton.Click += new System.EventHandler(this.DisplayButton_Click);
            // 
            // pieChart1
            // 
            this.pieChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.pieChart1.ChartAreas.Add(chartArea3);
            this.tableLayoutPanel1.SetColumnSpan(this.pieChart1, 6);
            legend3.Name = "Legend1";
            this.pieChart1.Legends.Add(legend3);
            this.pieChart1.Location = new System.Drawing.Point(472, 305);
            this.pieChart1.Name = "pieChart1";
            this.tableLayoutPanel1.SetRowSpan(this.pieChart1, 8);
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.pieChart1.Series.Add(series3);
            this.pieChart1.Size = new System.Drawing.Size(559, 319);
            this.pieChart1.TabIndex = 11;
            this.pieChart1.Text = "chart1";
            title3.BorderColor = System.Drawing.Color.Black;
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "Top Processes";
            this.pieChart1.Titles.Add(title3);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tableLayoutPanel1.SetColumnSpan(this.exitButton, 2);
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(864, 630);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(135, 28);
            this.exitButton.TabIndex = 17;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.ExitButton);
            // 
            // BarChart1
            // 
            this.BarChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ChartArea1";
            this.BarChart1.ChartAreas.Add(chartArea4);
            this.tableLayoutPanel1.SetColumnSpan(this.BarChart1, 4);
            legend4.DockedToChartArea = "ChartArea1";
            legend4.IsDockedInsideChartArea = false;
            legend4.Name = "Legend1";
            this.BarChart1.Legends.Add(legend4);
            this.BarChart1.Location = new System.Drawing.Point(3, 305);
            this.BarChart1.Name = "BarChart1";
            this.tableLayoutPanel1.SetRowSpan(this.BarChart1, 8);
            series4.ChartArea = "ChartArea1";
            series4.IsValueShownAsLabel = true;
            series4.LabelAngle = -90;
            series4.Legend = "Legend1";
            series4.Name = "Bytes";
            series4.ShadowColor = System.Drawing.Color.Gray;
            series4.ShadowOffset = 5;
            series4.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            series4.SmartLabelStyle.IsOverlappedHidden = false;
            this.BarChart1.Series.Add(series4);
            this.BarChart1.Size = new System.Drawing.Size(463, 319);
            this.BarChart1.TabIndex = 7;
            this.BarChart1.Text = "chart1";
            title4.BorderColor = System.Drawing.Color.Black;
            title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title4.Name = "Title1";
            title4.Text = "Files That Access The Most Memory";
            this.BarChart1.Titles.Add(title4);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 627);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 34);
            this.label1.TabIndex = 14;
            this.label1.Text = "Total Files:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewProcesses
            // 
            this.dataGridViewProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProcesses.Location = new System.Drawing.Point(3, 20);
            this.dataGridViewProcesses.Name = "dataGridViewProcesses";
            this.dataGridViewProcesses.Size = new System.Drawing.Size(10, 25);
            this.dataGridViewProcesses.TabIndex = 19;
            this.dataGridViewProcesses.Visible = false;
            // 
            // OperationComboBox
            // 
            this.OperationComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.OperationComboBox, 2);
            this.OperationComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperationComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OperationComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperationComboBox.FormattingEnabled = true;
            this.OperationComboBox.Location = new System.Drawing.Point(627, 20);
            this.OperationComboBox.Name = "OperationComboBox";
            this.OperationComboBox.Size = new System.Drawing.Size(231, 24);
            this.OperationComboBox.TabIndex = 20;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All files (*.csv*)|*.csv*";
            // 
            // saveXMLbutton
            // 
            this.saveXMLbutton.BackColor = System.Drawing.Color.Gold;
            this.saveXMLbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveXMLbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveXMLbutton.Location = new System.Drawing.Point(472, 630);
            this.saveXMLbutton.Name = "saveXMLbutton";
            this.saveXMLbutton.Size = new System.Drawing.Size(149, 28);
            this.saveXMLbutton.TabIndex = 21;
            this.saveXMLbutton.Text = "Save To XML";
            this.saveXMLbutton.UseVisualStyleBackColor = false;
            this.saveXMLbutton.Click += new System.EventHandler(this.SaveToXml);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1034, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcesses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button getFileButton;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button displayButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataVisualization.Charting.Chart BarChart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalProcessesLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridView dataGridViewProcesses;
        private System.Windows.Forms.ComboBox OperationComboBox;
        private System.Windows.Forms.Button saveXMLbutton;
    }
}

