using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2.Server
{
    public partial class Client_Info : UserControl
    {
        public Client_Info()
        {
            InitializeComponent();
        }
        public List<string> Defaults = new List<string>();
        private void Client_Info_Load(object sender, EventArgs e)
        {
            Defaults.Add("[username]");
            Defaults.Add("[computer name]");
            Defaults.Add("[operating system]");
            Defaults.Add("[lan IP]");
            Defaults.Add("[wan IP]");
        }

        public void Update_Info(List<string> info)
        {
            Username.Text = info[0];
            Computer_Name.Text = info[1];
            Operating_System.Text = info[2];
            Lan_IP.Text = info[3];
            Wan_IP.Text = info[4];
        }
    }
}
