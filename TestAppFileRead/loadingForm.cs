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
        int progress = 0;
        long sizefile = 0;

        public loadingForm(string bytesize, DataGridView dgv, Form1 frm, long filesize)
        {
            InitializeComponent();
            bytesLbl.Text = "Uploading " + bytesize + ". Please wait.";
            _dgv = dgv;
            check = frm;
            sizefile = filesize;
        }

        private void loadingForm_Load(object sender, EventArgs e)
        {

        }

        private async void Cancel_Click(object sender, EventArgs e)
        {
            Status.Text = "Loading Cancelled";
            _dgv = null;
            Form1 frm1 = new Form1();

            await Task.Delay(2000);
            this.Hide();
        }
    }
}
