namespace TestAppFileRead
{
    partial class loadingForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Cancel = new System.Windows.Forms.Button();
            this.bytesLbl = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 64);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(110, 93);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // bytesLbl
            // 
            this.bytesLbl.AutoSize = true;
            this.bytesLbl.Location = new System.Drawing.Point(72, 35);
            this.bytesLbl.Name = "bytesLbl";
            this.bytesLbl.Size = new System.Drawing.Size(161, 13);
            this.bytesLbl.TabIndex = 6;
            this.bytesLbl.Text = "Uploading 10 bytes. Please Wait";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(107, 9);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(91, 13);
            this.Status.TabIndex = 5;
            this.Status.Text = "                            ";
            // 
            // loadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 120);
            this.Controls.Add(this.bytesLbl);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.progressBar1);
            this.Name = "loadingForm";
            this.Text = "loadingForm";
            this.Load += new System.EventHandler(this.loadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label bytesLbl;
        private System.Windows.Forms.Label Status;
    }
}