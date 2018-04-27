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
        long stringSize = 0;
        Boolean canceled = false;
        public loadingForm(string bytesize, DataGridView dgv, Form1 frm, long filesize, long progress)
        {
            InitializeComponent();
            bytesLbl.Text = "Uploading " + bytesize + ". Please wait.";
            _dgv = dgv;
            check = frm;
            sizefile = filesize;
            totprogress = progress;
            progressBar1.Maximum =Convert.ToInt32(filesize);
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
            progressBar1.Value =Convert.ToInt32(progress);
        }

        internal bool iscanceled()
        {
            return canceled;
        }
    }
}
