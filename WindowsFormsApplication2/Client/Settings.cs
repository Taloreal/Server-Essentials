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
using System.Speech.Synthesis;

namespace WindowsFormsApplication2.Client
{
    public partial class Settings : Form
    {
        /// <summary>
        /// Initiates the Settings interface.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
        }
        List<Color> Supported_Theme_Colors = new List<Color>();
        string Theme_Path = "C:\\Program Data\\Theme.txt";
        string Nickname_Path = "C:\\Program Data\\Name.txt";
        string Program_Data_Path = "C:\\Program Data\\";
        List<Chat> UI = new List<Chat>();
        List<Thread> Threads = new List<Thread>();
        List<bool> Done = new List<bool>();
        string speaking = "Now not speaking messages";
        public string driver = Path.GetPathRoot(Environment.SystemDirectory);
        public string TargetIP = "Default";

        /// <summary>
        /// Saves the current theme and opens the chat interface.
        /// </summary>
        /// <param name="sender">The button or object that initiates the save and open command.</param>
        /// <param name="e">The triggering event.</param>
        private void SaveTheme_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(Program_Data_Path))
                    Directory.CreateDirectory(Program_Data_Path);
                TextWriter TW = new StreamWriter(Theme_Path);
                TW.WriteLine(Theme1.SelectedItem);
                TW.WriteLine(Theme2.SelectedItem);
                TW.WriteLine(Theme3.SelectedItem);
                if (checkBox1.Checked)
                    TW.WriteLine("Yes dating messages");
                else
                    TW.WriteLine("No dating messages");
                if (checkBox2.Checked)
                    TW.WriteLine("Yes loging");
                else
                    TW.WriteLine("Not loging");
                TW.Close();
                UI[0].Load_Theme();
                UI[0].Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Error Code 000x1 Could not load chat or save theme....");
                Process.GetCurrentProcess().Kill();
                return;
            }
        }

        /// <summary>
        /// Loads the hard-drive information of the computer and network information.
        /// </summary>
        /// <param name="sender">The object that initiates the load command.</param>
        /// <param name="e">The triggering event.</param>
        private void Settings_Load(object sender, EventArgs e)
        {
            new Thread(() => MessageBox.Show("Loading local network information")).Start();
            this.Visible = false;
            checkBox2.Text.Replace("C:\\", driver);
            Theme_Path.Replace("C:\\", driver);
            Nickname_Path.Replace("C:\\", driver);
            Program_Data_Path.Replace("C:\\", driver);
            foreach (KnownColor K in Enum.GetValues(typeof(KnownColor)))
                if (K != KnownColor.Transparent) 
                    Supported_Theme_Colors.Add(Color.FromKnownColor(K)); 
            foreach (Color color in Supported_Theme_Colors)
            {
                Theme1.Items.Add(color.Name);
                Theme2.Items.Add(color.Name);
                Theme3.Items.Add(color.Name);
            }
            Chat f1 = new Chat();
            f1.settings.Add(this);
            UI.Add(f1);
            if (!Directory.Exists(Program_Data_Path))
                Directory.CreateDirectory(Program_Data_Path);
            if (!File.Exists(Theme_Path))
            {
                TextWriter TW = new StreamWriter(Theme_Path);
                TW.WriteLine(Color.White.Name);
                TW.WriteLine(SystemColors.Control.Name);
                TW.WriteLine(Color.Black.Name);
                TW.WriteLine("No dating messages");
                TW.WriteLine("Not loging");
                TW.Close();
            }
            else
            {
                TextReader TR = new StreamReader(Theme_Path);
                string theme1 = TR.ReadLine();
                Theme1.SelectedItem = theme1;
                string theme2 = TR.ReadLine();
                Theme2.SelectedItem = theme2;
                string theme3 = TR.ReadLine();
                Theme3.SelectedItem = theme3;
                string date_messages = TR.ReadLine();
                if (date_messages.StartsWith("Y"))
                    checkBox1.Checked = true;
                else
                    checkBox1.Checked = false;
                string loging = TR.ReadLine();
                if (loging == "Yes loging")
                    checkBox2.Checked = true;
                else
                    checkBox2.Checked = false;
                TR.Close();
            }
            Load_Computer_Info();
            Check_For_Duplicate_Processes();
            Check_If_Updated();
            Load_Nickname();
            UI[0].FTP_UI.Add(new FTP(UI[0]));
            Activate_Threads();
        }

        /// <summary>
        /// Starts scanning the local area network.
        /// </summary>
        void Activate_Threads()
        {
            for (int i = 0; i != 256; i++)
            {
                int I = i;
                Thread T = new Thread(() => Load_LAN_NETWORK(I));
                bool B = false;
                Threads.Add(T);
                Done.Add(B);
            }
            foreach (Thread T in Threads)
                T.Start();
        }

        /// <summary>
        /// Loads the current nickname for the chat.
        /// </summary>
        void Load_Nickname()
        {
            try
            {
                if (!Directory.Exists(Program_Data_Path))
                    Directory.CreateDirectory(Program_Data_Path);
                if (!File.Exists(Nickname_Path))
                {
                    TextWriter TW = new StreamWriter(Nickname_Path);
                    TW.WriteLine(Environment.UserName.ToString());
                    TW.Close();
                    UI[0].Nickname = Environment.UserName.ToString();
                    UI[0].Username.Text = Environment.UserName.ToString();
                }
                else
                {
                    TextReader TR = new StreamReader(Nickname_Path);
                    UI[0].Nickname = TR.ReadLine();
                    TR.Close();
                    UI[0].Username.Text = UI[0].Nickname;
                }
            }
            catch
            {
                MessageBox.Show("Error Code 000x3 Could not load nickname...");
            }
        }

        /// <summary>
        /// Splits a string into parts according to it's marker.
        /// </summary>
        /// <param name="whole">The original string.</param>
        /// <param name="marker">Where to split at.</param>
        /// <returns>A list of strings that ar part of the whole.</returns>
        public List<string> Split(string whole, char marker)
        {
            List<string> bits = new List<string>();
            string temp = "";
            foreach (char c in whole)
            {
                if (c == marker)
                {
                    string place_holder = temp;
                    bits.Add(place_holder);
                    temp = "";
                }
                else
                    temp = temp + c.ToString();
            }
            bits.Add(temp);
            return bits;
        }

        /// <summary>
        /// Loads the current addresses in the LAN network.
        /// </summary>
        /// <param name="index">The address to check.</param>
        void Load_LAN_NETWORK(int index)
        {
            string LAN = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            List<string> Parts = Split(LAN, '.');
            LAN = "";
            foreach (string s in Parts)
                LAN = LAN + s + ".";
            try
            {
                string name = LAN + index.ToString();
                IPHostEntry D = Dns.GetHostByAddress(name);
                IPAddress IP = D.AddressList[0];
                UI[0].Lan_Addresses.Add(name + " - " + D.HostName);
            }
            catch
            {}
            Done[index] = true;
        }

        /// <summary>
        /// Loads the local computer information for connection to the server.
        /// </summary>
        void Load_Computer_Info()
        {
            for (int i = 0; i != UI[0].Computer_Info.Count(); i++)
                UI[0].Computer_Info[i] = "";
            UI[0].Computer_Info[0] = Environment.UserName.ToString();
            UI[0].Computer_Info[1] = Environment.MachineName.ToString();
            UI[0].Computer_Info[2] = "Windows " + new BorrowedCode().getOSInfo();
            try
            {
                UI[0].Computer_Info[3] = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                string address = "http://taloreal.com/ipaddress.shtml";
                WebClient client = new WebClient();
                UTF8Encoding encoding = new UTF8Encoding();
                string str3 = "";
                str3 = encoding.GetString(client.DownloadData(address));
                bool flag = false;
                string str4 = "";
                foreach (char ch in str3)
                {
                    if (ch.ToString() == "'")
                    {
                        if (!flag)
                        {
                            flag = true;
                        }
                        else if (flag)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        str4 = str4 + ch.ToString();
                    }
                }
                str4 = str4.Replace("'", "");
                UI[0].Computer_Info[4] = str4;
            }
            catch
            {
                MessageBox.Show("Error Code 000x4 Could not load computer data...");
                return;
            }
        }

        /// <summary>
        /// Checks to see if the exe was recently updated.
        /// </summary>
        void Check_If_Updated()
        {
            if (Application.ExecutablePath == Application.StartupPath + @"\naateschatUpdate.exe")
            {
                TextReader TR = new StreamReader(@"C:\Program Data\prevname.txt");
                string oldname = TR.ReadLine();
                TR.Close();
                File.Delete(oldname);
                File.Delete(@"C:\Program Data\prevname.txt");
                File.Move(Application.ExecutablePath, oldname);
            }
        }

        /// <summary>
        /// Checks to see if a process with the same name is already runing and ends current one.
        /// </summary>
        void Check_For_Duplicate_Processes()
        {
            Process P1 = Process.GetCurrentProcess();
            int Processes = 0;
            foreach (Process P2 in Process.GetProcesses())
                if (P1.ProcessName == P2.ProcessName)
                    Processes++;
            if (Processes == 2)
                P1.Kill();
        }

        /// <summary>
        /// Connects to the server.
        /// </summary>
        /// <param name="sender">The object that initiates the connect command.</param>
        /// <param name="e">The triggering event.</param>
        private void Connect_Click(object sender, EventArgs e)
        {
            UI[0].trying = true;
            Connect.Enabled = false;
            UI[0].speak.Add("Attempting to connect to server");
            new Thread(UI[0].Attempt_Connection).Start();
        }

        /// <summary>
        /// Starts or stops posting date and time with messages.
        /// </summary>
        /// <param name="sender">The object that initiates the DateTime command.</param>
        /// <param name="e">The triggering event.</param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UI[0].Date_Messages = checkBox1.Checked;
        }

        /// <summary>
        /// Stars or stops loging incoming messages.
        /// </summary>
        /// <param name="sender">The object that initiates the log command.</param>
        /// <param name="e">The triggering event.</param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UI[0].log = checkBox2.Checked;
        }

        /// <summary>
        /// Starts or stops speaking messages.
        /// </summary>
        /// <param name="sender">The object that initiates the speak command.</param>
        /// <param name="e">The triggering event.</param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            UI[0].speak.Clear();
            UI[0].speaking = !UI[0].speaking;
            if (speaking == "Now not speaking messages")
            {
                speaking = "Now speaking messages";
                new SpeechSynthesizer().Speak(speaking);
            }
            else
            {
                speaking = "Now not speaking messages";
                return;
            }
            new Thread(UI[0].speak_messages).Start();
        }

        /// <summary>
        /// Scans for to display LAN info.
        /// </summary>
        /// <param name="sender">The object that initiates the tick command.</param>
        /// <param name="e">The triggering event.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool truely = true;
            string temp = "";
            foreach (bool B in Done)
            {
                if (!B)
                    truely = false;
            }
            if (truely)
            {
                this.Visible = true;
                Threads.Clear();
                Done.Clear();
                foreach (string s in UI[0].Lan_Addresses)
                    temp = temp + s + "\r\n";
                timer1.Enabled = false;
                MessageBox.Show("local IP addresses are : \r\n" + temp);
            }
        }

        private bool IsIP(string IP)
        {
            IP = IP.ToLower();
            if (IP == "default" || IP == "lh" || IP == "localhost")
                return true;
            List<string> Items = Split(IP, '.');
            foreach (string Item in Items)
            {
                try { Convert.ToInt32(Item); }
                catch { return false;  }
            }
            return true;
        }

        private void IP_Address_TextChanged(object sender, EventArgs e)
        {
            if (IsIP(IP_Address.Text))
                TargetIP = IP_Address.Text.ToLower();
        }
    }
}
