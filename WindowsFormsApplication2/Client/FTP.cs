using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Web;
using Microsoft.VisualBasic;
using System.Management;
using System.Media;
using System.Linq;
using System.Diagnostics;

namespace WindowsFormsApplication2.Client
{
    public partial class FTP : Form
    {
        public FTP(Chat controler)
        {
            Controler.Add(controler);
            InitializeComponent();
        }

        List<Chat> Controler = new List<Chat>();
        string driver = "";
        List<string> previous = new List<string>();
        List<string> ftping_users = new List<string>();
        string Selected_File = "";
        bool selected = false;
        string dir = "Current directory : ";
        string fil = "Selected File : ";

        /// <summary>
        /// Occurs when FTP interface is initiated.
        /// </summary>
        /// <param name="sender">The object that initiates the load FTP command.</param>
        /// <param name="e">The triggering event.</param>
        private void FTP_Load(object sender, EventArgs e)
        {
            driver = Controler[0].settings[0].driver;
            Current_Directory.Text = Current_Directory.Text + driver;
            foreach (string directory in Directory.GetDirectories(driver))
            {
                try
                {
                    foreach (string sub in Directory.GetDirectories(directory))
                    {
                    }
                    Directories.Nodes.Add(directory.Replace(driver, "") + "\\");
                }
                catch
                {}
            }
            foreach (string file in Directory.GetFiles(driver))
                Files.Nodes.Add(file.Replace(driver, ""));
            previous.Add("C:\\");
        }

        /// <summary>
        /// Updates the graphical interface for FTP.
        /// </summary>
        /// <param name="location">The Directory to load.</param>
        public void Update_UI(string location)
        {
            Directories.Nodes.Clear();
            Files.Nodes.Clear();
            if (location != driver)
                Directories.Nodes.Add("<-- back");
            foreach (string directory in Directory.GetDirectories(location))
            {
                try
                {
                    foreach (string sub in Directory.GetDirectories(directory))
                    {
                    }
                    Directories.Nodes.Add(directory.Replace(location, "") + "\\");
                }
                catch
                {}
            }
            foreach (string file in Directory.GetFiles(location))
                Files.Nodes.Add(file.Replace(location, ""));
            Current_Directory.Text = "Current directory : " + location;
        }

        /// <summary>
        /// Occurs when a user clicks a directory.
        /// </summary>
        /// <param name="sender">The object that initiates the click directory command.</param>
        /// <param name="e">The triggering event.</param>
        private void Directories_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Selected_File != "")
            {
                Current_Directory.Text = (Current_Directory.Text.Replace(Selected_File, "")).Replace(fil, dir);
                Selected_File = "";
                selected = false;
            }
            if (e.Node.Text == "<-- back")
            {
                Update_UI(previous[previous.Count - 2]);
                previous.RemoveAt(previous.Count - 1);
                return;
            }
            Update_UI(Current_Directory.Text.Replace("Current directory : ", "") + e.Node.Text);
            previous.Add(Current_Directory.Text.Replace("Current directory : ", ""));
            Send.Enabled = false;
        }

        /// <summary>
        /// Occurs when a user selects a file to send.
        /// </summary>
        /// <param name="sender">The object that initiates the select file command.</param>
        /// <param name="e">The triggering event.</param>
        private void Files_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (selected)
            {
                Current_Directory.Text = Current_Directory.Text.Replace(Selected_File, "");
            }
            Selected_File = e.Node.Text;
            Current_Directory.Text = (Current_Directory.Text + Selected_File).Replace(dir, fil);
            selected = true;
            if (User_To_Send_To.Text != "")
                Send.Enabled = true;
        }

        /// <summary>
        /// Occurs when a user changes the user to send to.
        /// </summary>
        /// <param name="sender">The object that initiates the Change user command.</param>
        /// <param name="e">The triggering event.</param>
        private void User_To_Send_To_TextChanged(object sender, EventArgs e)
        {
            if (User_To_Send_To.Text != "" && Selected_File != "")
                Send.Enabled = true;
            else
                Send.Enabled = false;
        }

        /// <summary>
        /// Occurs when a user attempts to start sending a file thru the ftp.
        /// </summary>
        /// <param name="sender">The object that initiates the start ftp command.</param>
        /// <param name="e">The triggering event.</param>
        private void Send_Click(object sender, EventArgs e)
        {
            if (Controler[0].ftping)
            {
                MessageBox.Show("You already have a file transfer in progress...");
                return;
            }
            if (ftping_users.Contains(User_To_Send_To.Text))
            {
                MessageBox.Show(User_To_Send_To.Text + " is has a inbound or outbound file already in progress...");
                return;
            }
            Controler[0].Update_FTP_Status(User_To_Send_To.Text, Selected_File);
            byte[] file = File.ReadAllBytes(Current_Directory.Text.Replace(fil, ""));
            Thread T = new Thread(() => send(file));
            T.Start();
            this.Hide(); 
        }

        /// <summary>
        /// Sends a file.
        /// </summary>
        /// <param name="file">Bytes of the file to send.</param>
        public void send(byte[] file)
        {
            int temp1 = 0;
            Controler[0].sw.WriteLine("A: " + file.Count().ToString());
            Controler[0].sw.Flush();
            List<string> kilos = new List<string>();
            string temp = "";
            int index = 0;
            foreach (byte B in file)
            {
                if (Convert.ToInt32(B).ToString().Length == 3)
                    temp = temp + Convert.ToInt32(B).ToString();
                if (Convert.ToInt32(B).ToString().Length == 2)
                    temp = temp + "0" + Convert.ToInt32(B).ToString();
                if (Convert.ToInt32(B).ToString().Length == 1)
                    temp = temp + "00" + Convert.ToInt32(B).ToString();
                if (index == 1024)
                {
                    kilos.Add("6: " + temp);
                    index = 0;
                    temp = "";
                }
                if (temp1 == file.Count() - 100)
                    Controler[0].adding_data = true;
                index++;
                temp1++;
            }
            kilos.Add("6: " + temp);
            foreach (string kilo in kilos)
                Controler[0].Heart_Beats.Add(kilo);
            Controler[0].ftping = false;
            Controler[0].adding_data = false;
            if (temp1 != file.Count()) { Controler[0].Error = "inproper bytes...F"; }
            else { Controler[0].Error = "File at server...F"; }
            Controler[0].To_Send.Add("7: Complete...");
        }
    }
}
