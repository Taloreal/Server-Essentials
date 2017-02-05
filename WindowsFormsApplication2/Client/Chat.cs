using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Media;
using System.Linq;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace WindowsFormsApplication2.Client
{
    public partial class Chat : Form
    {
        /// <summary>
        /// Initiates the chat interface.
        /// </summary>
        public Chat()
        {
            InitializeComponent();
        }

        public bool trying = false;
        public bool adding_data = false;
        public bool request_chat_send = false;
        public bool sending_chat = false;
        public bool Date_Messages = false;
        public bool log = false;
        public bool ftping = false;
        public bool busy = false;
        public string Recipients_Message = "";
        public string File_Name = "";
        public string FTP_Type = "";
        public List<string> kilos = new List<string>();
        public List<string> To_Send = new List<string>();
        public List<string> Lan_Addresses = new List<string>();
        public List<string> Heart_Beats = new List<string>();
        bool scrolltobottom = false;
        bool Enabled = false;
        bool Connected = false;
        bool Updating = false;
        bool blocked = false;
        public bool seen = false;
        public bool typing = false;
        public bool messaging = false;
        public bool speaking = false;
        public string Log_Path = "C:\\Program Data\\Chat.txt";
        public string Nickname = "Guest";
        string Chat_Text = "";
        string Version = "v1.7.1";
        List<string> colors = new List<string>();
        string Theme_Path = "C:\\Program Data\\Theme.txt";
        string Nickname_Path = "C:\\Program Data\\Name.txt";
        string Program_Data_Path = "C:\\Program Data\\";
        public string FTP_Sender = "";
        List<Color> Supported_Theme_Colors = new List<Color>();
        List<string> Connected_Users = new List<string>();
        List<string> Past_Messages = new List<string>();
        string Last_Known_State = "";
        public List<Settings> settings = new List<Settings>();
        List<bool> Typing_Users = new List<bool>();
        public string[] Computer_Info = new string[5];
        public List<int> lengths = new List<int>();
        public List<FTP> FTP_UI = new List<FTP>();
        public StreamWriter sw;
        public StreamReader sr;
        public string Error = "";
        public List<string> compile = new List<string>();
        public List<FTP_Progressbar> Progress = new List<FTP_Progressbar>();
        public List<string> speak = new List<string>();
        string Default = "localhost";
        
        /// <summary>
        /// Speaks Messages
        /// </summary>
        public void speak_messages()
        {
            while (speaking)
            {
                if (speak.Count() != 0)
                {
                    if (!speak[0].StartsWith(Username + ": "))
                    {
                        new SpeechSynthesizer().Speak(speak[0]);
                        speak.RemoveAt(0);
                    }
                }
            }
        }

        /// <summary>
        /// initiates the chatbox.
        /// </summary>
        /// <param name="sender">The object that initiates the load command.</param>
        /// <param name="e">The triggering event.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Log_Path.Replace("C:\\", settings[0].driver);
            Theme_Path.Replace("C:\\", settings[0].driver);
            Nickname_Path.Replace("C:\\", settings[0].driver);
            Program_Data_Path.Replace("C:\\", settings[0].driver);
            Load_Theme();
            Time.Location = new Point(this.Size.Width - 260, this.Size.Height - 70);
        }

        /// <summary>
        /// Loads the currently saved theme for the chat.
        /// </summary>
        public void Load_Theme()
        {
            try
            {
                foreach (KnownColor K in Enum.GetValues(typeof(KnownColor)))
                    if (K != KnownColor.Transparent)
                        Supported_Theme_Colors.Add(Color.FromKnownColor(K));
                if (!Directory.Exists(Program_Data_Path))
                    Directory.CreateDirectory(Program_Data_Path);
                if (!File.Exists(Theme_Path))
                {
                    TextWriter TW = new StreamWriter(Theme_Path);
                    TW.WriteLine(Color.White.Name);
                    TW.WriteLine(SystemColors.Control.Name);
                    TW.WriteLine(Color.Black.Name);
                    TW.Close();
                }
                else
                {
                    TextReader TR = new StreamReader(Theme_Path);
                    string theme1 = TR.ReadLine();
                    string theme2 = TR.ReadLine();
                    string theme3 = TR.ReadLine();
                    foreach (Color color in Supported_Theme_Colors) 
                    {
                        if (theme1 == color.Name) 
                            foreach (Control C in this.Controls) 
                                if (!C.Name.Contains("label"))
                                    C.BackColor = color;
                        if (theme2 == color.Name) {
                            foreach (Control C in this.Controls)
                                if (C.Name.Contains("label"))
                                    C.BackColor = color;
                            this.BackColor = color;
                        }
                        if (theme3 == color.Name) 
                            foreach (Control C in this.Controls) 
                                C.ForeColor = color; 
                    }
                    TR.Close();
                }
            }
            catch
            {
                Error = "Error Code 000x5 Could not load themes...F";
                return;
            }
        }

        /// <summary>
        /// Resets chatbox to allowed text.
        /// </summary>
        /// <param name="sender">The object that initiates the textchanged command.</param>
        /// <param name="e">The triggering event.</param>
        private void Chat_TextChanged(object sender, EventArgs e)
        {
            if (MessageBoard.Text != Chat_Text)
                MessageBoard.Text = Chat_Text;
        }

        /// <summary>
        /// Scans the program to make changes to GUI.
        /// </summary>
        /// <param name="sender">The object that initiates the tick command.</param>
        /// <param name="e">The triggering event.</param>
        private void Thread_Scanner_Tick(object sender, EventArgs e)
        {
            Time.Text = "Date / Time : " + DateTime.Now.ToString();
            if (Error != "")
            {
                speak.Add((Error.Replace("...F", "")).Replace("...", ""));
                Thread_Scanner.Enabled = false;
                if (!Error.EndsWith("F"))
                {
                    MessageBox.Show(Error);
                    Process.GetCurrentProcess().Kill();
                }
                MessageBox.Show(Error.Replace("...F", "..."));
                Thread_Scanner.Enabled = true;
                Error = "";
                return;
            }
            if (Connected && !Enabled)
            {
                settings[0].Connect.Enabled = false;
                Send.Enabled = true;
                Username.Enabled = true;
                Message.Enabled = true;
                Open_FTP.Enabled = true;
                Chat_Text = "Connecting... Connected\r\n";
                Enabled = true;
            }
            if (!Connected && !trying)
                settings[0].Connect.Enabled = true;
            if (Updating && !Enabled)
            {
                settings[0].Connect.Enabled = false;
                Chat_Text = "Connecting... Updating...\r\n";
                Enabled = true;
            } 
            if (Connected)
            {
                if (blocked)
                    Send.Enabled = false;
                if (!blocked)
                    Send.Enabled = true;
                if (!ftping)
                    To_Send.Add("<heartbeat>");
                if (ftping && !seen)
                {
                    seen = true;
                    Progress.Add(new FTP_Progressbar(FTP_Type));
                }
                if (Recipients_Message != "")
                {
                    string msg = Recipients_Message;
                    Recipients_Message = "";
                    MessageBox.Show(msg);
                }
                MessageBoard.Text = Chat_Text;
                FTP_UI[0].User_To_Send_To.Items.Clear();
                foreach (string user in Connected_Users)
                {
                    if (user != Nickname)
                    {
                        FTP_UI[0].User_To_Send_To.Items.Add(user);
                    }
                }
                if (!Connected_Users.Contains(FTP_UI[0].User_To_Send_To.Text))
                    FTP_UI[0].User_To_Send_To.Text = "";
                if (Chat_Text != Last_Known_State && Last_Known_State != "")
                {
                    string Message_To_Add = Chat_Text.Replace(Last_Known_State, "");
                    Past_Messages.Add(Message_To_Add);
                    if (Past_Messages.Count == 50)
                    {
                        Past_Messages.RemoveAt(0);
                        Chat_Text = "";
                        foreach (string message in Past_Messages)
                            Chat_Text = Chat_Text + message;
                        MessageBoard.Text = Chat_Text;
                    }
                }
                Last_Known_State = Chat_Text;
                if (scrolltobottom)
                {
                    scrolltobottom = false;
                    MessageBoard.Select(this.MessageBoard.Text.Length, 0);
                    MessageBoard.ScrollToCaret();
                }
                Userlist.Nodes.Clear();
                int index = 0;
                foreach (string user in Connected_Users)
                {
                    TreeNode TN = new TreeNode();
                    if (Typing_Users[index])
                        TN.Text = "   -typing- ";
                    TN.Text = user + TN.Text;
                    index++;
                    Userlist.Nodes.Add(TN);
                }
            }
        }

        /// <summary>
        /// Attempts to connect to server.
        /// </summary>
        public void Attempt_Connection()
        {
            try
            {
                TcpClient client = new TcpClient();
                if (settings[0].TargetIP == "default")
                    client.Connect(Default, 5534);
                else if (settings[0].TargetIP == "lh")
                    client.Connect("localhost", 5534);
                else
                    client.Connect(settings[0].TargetIP, 5534);
                NetworkStream NS = client.GetStream();
                sw = new StreamWriter(NS);
                sr = new StreamReader(NS);
                trying = false;
            }
            catch
            {
                trying = false;
                Error = "Error Code 000x6 Failure to connect to server...F";
                return;
            }
            Check_For_Updates();
        }

        /// <summary>
        /// Sends data to the server.
        /// </summary>
        public void Send_Data()
        {
            while (true)
            {
                while (To_Send.Count == 0) { }
                while (adding_data) { }
                List<int> sent = new List<int>();
                for (int I = 0; I != To_Send.Count; I++)
                {
                    if (To_Send[I] != null)
                    {
                        sw.WriteLine(To_Send[I]);
                        sw.Flush();
                    }
                    int temp = I;
                    sent.Add(temp);
                }
                sending_chat = false;
                sent.Reverse();
                foreach (int i in sent)
                    To_Send.RemoveAt(i);
            }
        }

        /// <summary>
        /// Checks for updates to the client.
        /// </summary>
        void Check_For_Updates()
        {
            try
            {
                string Incoming_Version = sr.ReadLine();
                if (Incoming_Version != Version)
                    Auto_Update();
                sw.WriteLine("no update needed...");
                sw.Flush();
                foreach (string info in Computer_Info)
                {
                    sw.WriteLine(info);
                    sw.Flush();
                }
                sw.WriteLine(Nickname);
                sw.Flush();
                new Thread(Send_Data).Start();
                Command_Listener();
            }
            catch
            {
                Error = "Error Code 000x7 Failure to check for updates or send Computer info...";
                return;
            }
        }

        /// <summary>
        /// Listens for incoming transmitions to the client.
        /// </summary>
        void Command_Listener()
        {
            Connected = true;
            while (true)
            {
                try
                {
                    string Comunicae = sr.ReadLine();
                    if (Comunicae[0] == '1')
                        User_List_Update(Comunicae.Replace("1: ", ""));
                    if (Comunicae[0] == '2')
                        New_Message(Comunicae.Replace("2: ", ""));
                    if (Comunicae[0] == '4')
                        New_Server_Command(Comunicae.Replace("4: ", ""));
                    if (Comunicae[0] == '5')
                    {
                        string filenam = sr.ReadLine();
                        File_Name = filenam;
                        FTP_Sender = Comunicae.Replace("5: ", "");
                        Recipients_Message = Comunicae.Replace("5: ", "") + " is sending you " + filenam + " file via ftp...\r\nYou will recieve this file in " + settings[0].driver + "program data\\";
                    }
                    if (Comunicae[0] == '6')
                    {
                        int length = Comunicae.Length;
                        lengths.Add(length);
                        string kilo = Comunicae.Replace("6: ", "");
                        compile.Add(Comunicae);
                        kilos.Add(kilo);
                    }
                    if (Comunicae[0] == '7')
                    {
                        int last = 0;
                        foreach (string kilo in kilos)
                        {
                            last = kilo.Length;
                        }
                        new Thread(Compile_File).Start();
                    }
                }
                catch (Exception E)
                {
                    Error = "Error Code 000x8 Exception searching for communications...\r\n\r\n" + E.Message;
                    return;
                }
            }
        }

        /// <summary>
        /// Compiles an ftp'ed file.
        /// </summary>
        void Compile_File()
        {
            int length = kilos[kilos.Count - 1].Length;
            List<string> bytes = new List<string>();
            int index = 0;
            foreach (string kilo in kilos)
            {
                while (index != kilo.Count())
                {
                    int tem = kilo.Count();
                    string temp = kilo[index].ToString();
                    temp = temp + kilo[index + 1];
                    temp = temp + kilo[index + 2];
                    bytes.Add(temp);
                    index = index + 3;
                }
                index = 0;
            }
            List<byte> file = new List<byte>();
            foreach (string kilo in bytes)
            {
                file.Add(Convert.ToByte(Convert.ToInt32(kilo)));
            }
            byte[] final = new byte[file.Count];
            for (int i = 0; i != file.Count; i++)
            {
                final[i] = file[i];
            }
            Directory.CreateDirectory(settings[0].driver + "Program data\\");
            File.WriteAllBytes(settings[0].driver + "Program Data\\" + File_Name, final);
            ftping = false;
            seen = false;
            Error = "FTP Complete...F";
        }

        /// <summary>
        /// Reads a new administrator command to either stop allowing messages or kill the program.
        /// </summary>
        /// <param name="Command">The command to perform.</param>
        void New_Server_Command(string Command)
        {
            if (Command == "Kill")
            {
                this.Dispose(true);
                return;
            }
            if (Command == "blocked")
            {
                blocked = !blocked;
            }
        }

        /// <summary>
        /// Updates the allowed chat in the chatbox by reading incoming chat data.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void New_Message(string message)
        {
            string temp = message;
            if (Date_Messages)
                temp = temp + " " + DateTime.Now.ToString().Replace(" ", "-");
            if (log) 
            {
                if (!File.Exists(Log_Path))
                {
                    TextWriter tw = new StreamWriter(Log_Path);
                    tw.Close();
                }
                TextReader TR = new StreamReader(Log_Path);
                string Log = TR.ReadToEnd();
                TR.Close();
                TextWriter TW = new StreamWriter(Log_Path);
                TW.WriteLine(Log + temp);
                TW.Close();
            }
            List<string> words = new List<string>();
            string word = "";
            foreach (char character in temp)
            {
                if (character.ToString() == " ")
                {
                    string str7 = word;
                    words.Add(str7);
                    word = "";
                }
                else
                {
                    word = word + character.ToString();
                }
            }
            words.Add(word);
            //int length = 0;
            temp = "";
            List<string> Lines = new List<string>();
            string current_Line = "";
            foreach (string Word in words)
            {
                if (current_Line.Length < 300)
                {
                    current_Line += Word + " ";
                }
                else
                {
                    string temp_line = current_Line;
                    current_Line = "";
                    Lines.Add(temp_line);
                }
            }
            Lines.Add(current_Line);
            temp = "";
            foreach (string line in Lines)
                temp = temp + line + "\r\n";
            scrolltobottom = true;
            Chat_Text = Chat_Text + temp;
            string str9 =  temp.Replace(" ", "");
            int num3 = temp.Length - str9.Length;
            speak.Add("You have a new message " + message);
            if (this.WindowState == FormWindowState.Minimized)
            {
                new SoundPlayer(settings[0].driver + "Windows\\Media\\ding.wav").Play();
            }
        }

        /// <summary>
        /// Updates the userlist.
        /// </summary>
        /// <param name="list">New userlist to update to.</param>
        void User_List_Update(string list)
        {
            try
            {
                List<string> templist = new List<string>();
                string tempuser = "";
                foreach (char c in list)
                {
                    if (c == '-')
                    {
                        string newuser = tempuser;
                        templist.Add(newuser);
                        tempuser = "";
                    }
                    else
                    {
                        tempuser = tempuser + c;
                    }
                }
                List<bool> temptyping = new List<bool>();
                foreach (string user in templist)
                {
                    bool typing = false;
                    if (user.EndsWith("Y"))
                    {
                        typing = true;
                    }
                    else if (user.EndsWith("N"))
                    {
                        typing = false;
                    }
                    temptyping.Add(typing);
                }
                for (int index = 0; index < templist.Count; index++)
                    templist[index] = templist[index].TrimEnd('N');
                for (int index = 0; index < templist.Count; index++)
                    templist[index] = templist[index].TrimEnd('Y');
                Connected_Users = templist;
                Typing_Users = temptyping;
            }
            catch
            {
                Error = "Error Code 000x9 Could not retrieve user list...";
                return;
            }
        }

        /// <summary>
        /// Updates the chat client to newer version.
        /// </summary>
        void Auto_Update()
        {
            Updating = true;
            sw.WriteLine("ready for update...");
            sw.Flush();
            int num = Convert.ToInt32(this.sr.ReadLine());
            List<byte> source = new List<byte>();
            for (int i = 0; i != num; i++)
            {
                source.Add(Convert.ToByte(this.sr.ReadLine()));
            }
            byte[] buffer = new byte[source.Count];
            for (int j = 0; j != source.Count; j++)
            {
                buffer.SetValue(source.ElementAt<byte>(j), j);
            }
            BinaryWriter writer = new BinaryWriter(File.Open(Application.StartupPath + @"\naateschatUpdate.exe", FileMode.Create));
            writer.Write(buffer);
            writer.Close();
            Directory.CreateDirectory(settings[0].driver + "Program Data\\");
            TextWriter writer2 = new StreamWriter(settings[0].driver + "Program Data\\prevname.txt");
            writer2.WriteLine(Application.ExecutablePath);
            writer2.Close();
            Process.Start(Application.StartupPath + @"\naateschatUpdate.exe");
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Occurs when client attempts to send a message.
        /// </summary>
        /// <param name="sender">The object that initiates the send command.</param>
        /// <param name="e">The triggering event.</param>
        private void Send_Click(object sender, EventArgs e)
        {
            if (blocked)
                return;
            if (Message.Text == "")
                Message.Text = " ";
            if (Message.Text.StartsWith("@" + Username.Text))
            {
                Message.Text = "Talking to your self isn't considered sane...";
                speak.Add("Talking to your self isn't considered sane...");
                return;
            }
            try
            {
                if (Message.Text.StartsWith("@"))
                {
                    string construct = "";
                    string message = "";
                    bool building = true;
                    foreach (char C in Message.Text)
                    {
                        if (building)
                        {
                            if (C == ' ')
                                building = false;
                            if (C != ' ' && C != '@')
                                construct = construct + C;
                        }
                        else
                            message = message + C;
                    }
                    if (!Connected_Users.Contains(construct))
                    {
                        Message.Text = "User not connected...";
                        return;
                    }
                    messaging = true;
                    To_Send.Add("2: @" + Nickname + " whispers to you; " + construct + "; " + message);
                    Chat_Text = Chat_Text + "You whisper to " + construct + " " + message + "\r\n";
                    messaging = false;
                }
                else
                {
                    messaging = true;
                    To_Send.Add("2: " + Message.Text);
                    messaging = false;
                }
            }
            catch 
            {
                this.Dispose(true);
                return;
            }
            Message.Text = "";
        }

        /// <summary>
        /// Occurs when client changes their username.
        /// </summary>
        /// <param name="sender">The object that initiates the change user command.</param>
        /// <param name="e">The triggering event.</param>
        private void Username_TextChanged(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                Username.Text = Nickname;
            }
            for (int i = 0; i < Username.Text.Length; i++)
            {
                if (!char.IsLetterOrDigit(Username.Text[i]))
                {
                    Username.Text = Nickname;
                    return;
                }
            }
            Nickname = Username.Text;
            if (Connected)
            {
                try
                {
                    To_Send.Add("1: " + Nickname);
                }
                catch
                {
                    this.Dispose(true);
                    return;
                }
            }
            TextWriter TW = new StreamWriter(Nickname_Path);
            TW.WriteLine(Nickname);
            TW.Close();
        }

        /// <summary>
        /// Occurs when a user starts or stops typing.
        /// </summary>
        /// <param name="sender">The object that initiates the typing command.</param>
        /// <param name="e">The triggering event.</param>
        private void Message_TextChanged(object sender, EventArgs e)
        {
            if (Message.Text != "")
            {
                To_Send.Add("3: typing");
                typing = true;
            }
            if (Message.Text == "")
            {
                To_Send.Add("3: empty");
                typing = false;
            }
        }

        /// <summary>
        /// Selects a username and starts whispering to them.
        /// </summary>
        /// <param name="sender">The object that initiates the whisper command.</param>
        /// <param name="e">The triggering event.</param>
        private void Userlist_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Message.Text = "@" + e.Node.Text.Replace("   -typing- ", "");
        }

        /// <summary>
        /// Occurs when a user hits enter.
        /// </summary>
        /// <param name="sender">The object that initiates the send command.</param>
        /// <param name="e">The triggering event.</param>
        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send_Click(null, null);
            }
        }

        /// <summary>
        /// Opens the settings page.
        /// </summary>
        /// <param name="sender">The object that initiates the Open Settings command.</param>
        /// <param name="e">The triggering event.</param>
        private void Open_Settings_Click(object sender, EventArgs e)
        {
            this.Hide();
            settings[0].Show();
        }

        /// <summary>
        /// Occurs when user resizes chat.
        /// </summary>
        /// <param name="sender">The object that initiates the resize command.</param>
        /// <param name="e">The triggering event.</param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            MessageBoard.Size = new Size(this.Size.Width - 251, this.Size.Height - 133);
            Userlist.Location = new Point(this.Size.Width - 233, 30);
            Userlist.Size = new Size(207, this.Size.Height - 133);
            label1.Location = new Point(this.Size.Width - 236, 9);
            Send.Location = new Point(this.Size.Width - 233, this.Size.Height - 101);
            label2.Location = new Point(12, this.Size.Height - 67);
            Username.Location = new Point(103, this.Size.Height - 70);
            Open_Settings.Location = new Point(217, this.Size.Height - 71);
            Message.Location = new Point(87, this.Size.Height - 100);
            label3.Location = new Point(12, this.Size.Height - 97);
            Message.Size = new Size(this.Size.Width - 326, 24);
            Time.Location = new Point(this.Size.Width - 260, this.Size.Height - 70);
            Open_FTP.Location = new Point(338, this.Size.Height - 71);
        }

        /// <summary>
        /// Occurs when a user opens the FTP interface.
        /// </summary>
        /// <param name="sender">The object that initiates the FTP open command.</param>
        /// <param name="e">The triggering event.</param>
        private void Open_FTP_Click(object sender, EventArgs e)
        {
            if (ftping)
            {
                MessageBox.Show("You already have a file transfer in progress...");
                return;
            }
            if (Connected_Users.Count == 1)
            {
                MessageBox.Show("No other users in chat");
                return;
            }
            FTP_UI[0].Show();
        }

        /// <summary>
        /// Updates FTP status.
        /// </summary>
        /// <param name="send_to">Username to send to.</param>
        /// <param name="filename">The name of the file to send.</param>
        public void Update_FTP_Status(string send_to, string filename)
        {
            To_Send.Add("5: " + send_to + ":" + filename);
            FTP_Type = "outgoing...";
            seen = false;
            ftping = true;
        }
    }
}
