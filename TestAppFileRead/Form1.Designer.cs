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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BarChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.getFileButton = new System.Windows.Forms.Button();
            this.ResultsLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.processNameLabel = new System.Windows.Forms.Label();
            this.pieChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.displayButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.totalProcessesLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.50365F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.233577F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.014598F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.17518F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.77778F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.555555F));
            this.tableLayoutPanel1.Controls.Add(this.totalProcessesLabel, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lengthLabel, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilePath, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.BarChart1, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.getFileButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ResultsLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.processNameLabel, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.pieChart1, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.displayButton, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.340454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.871829F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.71028F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.542056F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.675568F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.476635F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.343124F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.544726F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.68625F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.675568F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1354, 749);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(75, 524);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 64);
            this.label1.TabIndex = 14;
            this.label1.Text = "Total Processes Recorded";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 469);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(45, 10, 0, 0);
            this.label2.Size = new System.Drawing.Size(150, 55);
            this.label2.TabIndex = 13;
            this.label2.Text = "Length:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lengthLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lengthLabel.Location = new System.Drawing.Point(225, 469);
            this.lengthLabel.Margin = new System.Windows.Forms.Padding(0);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(318, 55);
            this.lengthLabel.TabIndex = 12;
            this.lengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxFilePath.Location = new System.Drawing.Point(228, 46);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(312, 20);
            this.textBoxFilePath.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 3);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(78, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(519, 239);
            this.dataGridView1.TabIndex = 4;
            // 
            // BarChart1
            // 
            chartArea3.Name = "ChartArea1";
            this.BarChart1.ChartAreas.Add(chartArea3);
            this.tableLayoutPanel1.SetColumnSpan(this.BarChart1, 2);
            legend3.Name = "Legend1";
            this.BarChart1.Legends.Add(legend3);
            this.BarChart1.Location = new System.Drawing.Point(657, 72);
            this.BarChart1.Name = "BarChart1";
            this.tableLayoutPanel1.SetRowSpan(this.BarChart1, 2);
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.BarChart1.Series.Add(series3);
            this.BarChart1.Size = new System.Drawing.Size(537, 239);
            this.BarChart1.TabIndex = 7;
            this.BarChart1.Text = "chart1";
            // 
            // getFileButton
            // 
            this.getFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getFileButton.Location = new System.Drawing.Point(78, 43);
            this.getFileButton.Name = "getFileButton";
            this.getFileButton.Size = new System.Drawing.Size(75, 23);
            this.getFileButton.TabIndex = 0;
            this.getFileButton.Text = "Get File";
            this.getFileButton.UseVisualStyleBackColor = true;
            this.getFileButton.Click += new System.EventHandler(this.getFileButton_Click);
            // 
            // ResultsLabel
            // 
            this.ResultsLabel.AutoSize = true;
            this.ResultsLabel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ResultsLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.ResultsLabel, 2);
            this.ResultsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsLabel.ForeColor = System.Drawing.Color.Red;
            this.ResultsLabel.Location = new System.Drawing.Point(75, 363);
            this.ResultsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsLabel.Name = "ResultsLabel";
            this.ResultsLabel.Padding = new System.Windows.Forms.Padding(90, 5, 3, 3);
            this.ResultsLabel.Size = new System.Drawing.Size(468, 50);
            this.ResultsLabel.TabIndex = 8;
            this.ResultsLabel.Text = "Most Process Used";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(75, 413);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.nameLabel.Size = new System.Drawing.Size(150, 56);
            this.nameLabel.TabIndex = 9;
            this.nameLabel.Text = "Name:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // processNameLabel
            // 
            this.processNameLabel.AutoSize = true;
            this.processNameLabel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.processNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processNameLabel.Location = new System.Drawing.Point(225, 413);
            this.processNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.processNameLabel.Name = "processNameLabel";
            this.processNameLabel.Size = new System.Drawing.Size(318, 56);
            this.processNameLabel.TabIndex = 10;
            this.processNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pieChart1
            // 
            chartArea4.Name = "ChartArea1";
            this.pieChart1.ChartAreas.Add(chartArea4);
            this.tableLayoutPanel1.SetColumnSpan(this.pieChart1, 2);
            legend4.Name = "Legend1";
            this.pieChart1.Legends.Add(legend4);
            this.pieChart1.Location = new System.Drawing.Point(657, 366);
            this.pieChart1.Name = "pieChart1";
            this.tableLayoutPanel1.SetRowSpan(this.pieChart1, 5);
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.pieChart1.Series.Add(series4);
            this.pieChart1.Size = new System.Drawing.Size(537, 329);
            this.pieChart1.TabIndex = 11;
            this.pieChart1.Text = "chart1";
            // 
            // displayButton
            // 
            this.displayButton.AutoSize = true;
            this.displayButton.Location = new System.Drawing.Point(657, 43);
            this.displayButton.Name = "displayButton";
            this.displayButton.Size = new System.Drawing.Size(87, 23);
            this.displayButton.TabIndex = 3;
            this.displayButton.Text = "Display file info";
            this.displayButton.UseVisualStyleBackColor = true;
            this.displayButton.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All files (*.csv*)|*.csv*";
            // 
            // totalProcessesLabel
            // 
            this.totalProcessesLabel.AutoSize = true;
            this.totalProcessesLabel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel1.SetColumnSpan(this.totalProcessesLabel, 2);
            this.totalProcessesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalProcessesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalProcessesLabel.ForeColor = System.Drawing.Color.Black;
            this.totalProcessesLabel.Location = new System.Drawing.Point(75, 588);
            this.totalProcessesLabel.Margin = new System.Windows.Forms.Padding(0);
            this.totalProcessesLabel.Name = "totalProcessesLabel";
            this.totalProcessesLabel.Size = new System.Drawing.Size(468, 110);
            this.totalProcessesLabel.TabIndex = 15;
            this.totalProcessesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1354, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieChart1)).EndInit();
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
        private System.Windows.Forms.Label ResultsLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label processNameLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalProcessesLabel;
    }
}

