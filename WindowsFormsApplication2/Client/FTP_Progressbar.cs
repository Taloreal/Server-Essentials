using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2.Client
{
    public partial class FTP_Progressbar : Form
    {
        public FTP_Progressbar(string type)
        {
            FTP_Type = type;
            InitializeComponent();
        }
        public bool Update_Title = true;
        public string FTP_Type = "";
        public int Selected_Pieces = 0;
        public int Completed_Pieces = 0;
        int Last_Known = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Update_Title)
            {
                this.Text = "FTP Progress - " + FTP_Type;
                Update_Title = false;
            }
            label2.Text = ((int)Math.Round(Convert.ToDouble(Completed_Pieces / Selected_Pieces * 100))).ToString() + "%";
            progressBar1.Value = (int)Math.Round(Convert.ToDouble(Completed_Pieces / Selected_Pieces * 100));
            label3.Text = (Completed_Pieces - Last_Known).ToString() + " kb/s";
            Last_Known = Completed_Pieces;
            label1.Text = "Pieces : " + Completed_Pieces.ToString() + "/" + Selected_Pieces.ToString();
        }
    }
}
