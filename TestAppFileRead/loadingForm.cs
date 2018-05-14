using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAppFileRead
{
    public partial class loadingForm : Form
    {
        private DataGridView _dgv;
        Form1 check;
        
        //public constant to check the progress of the %
        long totprogress = 0;
        long sizefile = 0;
        Boolean canceled = false;
        Boolean complete = false;
        public loadingForm(DataGridView dgv, Form1 frm, long filesize, long progress)
        {
            InitializeComponent();
            bytesLbl.Text = "Uploading " + filesize + " bytes. Please wait.";
            _dgv = dgv;
            check = frm;
            sizefile = filesize;
            totprogress = progress;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
        }

        private void loadingForm_Load(object sender, EventArgs e)
        {
            
        }

        private  void Cancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            Status.Text = "Loading Cancelled";
            Cancel.Enabled = true;
        }

        internal void setprogress(long progress)
        {

                int per = (int)(((double)progress / (double)sizefile) * 100);
                if (progressBar1.Value != per)
                {
                    progressBar1.Value = per;
                }
        }

        internal bool iscanceled()
        {
            return canceled;
        }

        internal bool completed()
        {
            return complete;
        }

        
    }
}
