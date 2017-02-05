using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Xml.Serialization;

namespace WindowsFormsApplication2.Server
{
    public partial class Host : Form
    {
        /// <summary>
        /// Initiates a new server graphical interface.
        /// </summary>
        public Host()
        {
            InitializeComponent();
        }

        TcpListener Server = new TcpListener(5534);
        public string Version = "v1.7.1";
        public List<Client> clients = new List<Client>();
        public string Log_Path = "C:\\Program Data\\Log.txt";
        public string Save_Update_Loc = "Program Data\\save.txt";
        public bool busy = false;
        public int Selected_Index = 0;
        public bool Reset_Manager = false;
        public string driver = Path.GetPathRoot(Environment.SystemDirectory);
        public string Error = "";

        /// <summary>
        /// Command to stop the server program all together.
        /// </summary>
        /// <param name="sender">The button or object that initiates the stop command.</param>
        /// <param name="e">The triggering event.</param>
        private void Stop_Server_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
            return;
        }

        /// <summary>
        /// Starts the server so clients can start connecting.
        /// </summary>
        /// <param name="sender">The button or object that initiates the start command.</param>
        /// <param name="e">The triggering event.</param>
        private void Start_Server_Click(object sender, EventArgs e)
        {
            Log_Path = Log_Path.Replace("C:\\", driver);
            new Thread(Catch_Clients).Start();
            Start_Server.Enabled = false;
        }

        /// <summary>
        /// Thread safe logging.
        /// </summary>
        /// <param name="To_Log">The information to log.</param>
        public void Log_it(string To_Log)
        {
            while (busy)
            { }
            busy = true;
            Log_Event(To_Log);
            busy = false;
        }

        /// <summary>
        /// Logs information to the log.txt file.
        /// </summary>
        /// <param name="To_Log">The information to log.</param>
        public void Log_Event(string To_Log)
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
            TW.WriteLine(Log + To_Log);
            TW.Close();
        }

        /// <summary>
        /// Waits for a new client to connect (Use on a non-graphics thread only).
        /// </summary>
        void Catch_Clients()
        {
            Server.Start();
            while (true)
            {
                Socket S = Server.AcceptSocket();
                NetworkStream NS = new NetworkStream(S);
                StreamWriter SW = new StreamWriter(NS);
                StreamReader SR = new StreamReader(NS);
                Client C = new Client(this, SW, SR, clients.Count);
                clients.Add(C);
            }
        }

        /// <summary>
        /// Updates the indexes of the clients in the client array.
        /// </summary>
        public void Update_Index()
        {
            Reset_Manager = true;
            int index = 0;
            foreach (Client C in clients)
            {
                C.Client_Index = index;
                index++;
            }
        }

        /// <summary>
        /// Updates the graphical interface of information on clients.
        /// </summary>
        /// <param name="sender">The button or object that initiates the back command.</param>
        /// <param name="e">The triggering event.</param>
        private void Back_One_Client_Click(object sender, EventArgs e)
        {
            if (Selected_Index == 0)
                return;
            Selected_Index--;
            Client_Number.Text = Selected_Index.ToString();
            if (Selected_Index == 0)
            {
                client_Info1.Update_Info(client_Info1.Defaults);
                client_Info1.BackColor = Color.Lime;
                return;
            }
            client_Info1.Update_Info(clients[Selected_Index - 1].Client_Info);
            if (clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Red;
            if (!clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Lime;
        }

        /// <summary>
        /// Updates the graphical interface of information on clients.
        /// </summary>
        /// <param name="sender">The button or object that initiates the next command.</param>
        /// <param name="e">The triggering event.</param>
        private void Forward_One_Client_Click(object sender, EventArgs e)
        {
            if (Selected_Index == clients.Count)
                return;
            Selected_Index++;
            Client_Number.Text = Selected_Index.ToString();
            client_Info1.Update_Info(clients[Selected_Index - 1].Client_Info);
            if (clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Red;
            if (!clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Lime;
        }

        /// <summary>
        /// Helps update the client manager
        /// </summary>
        /// <param name="sender">The timer or object that initiates the update manager command.</param>
        /// <param name="e">The triggering event.</param>
        private void Client_Manager_Helper_Tick(object sender, EventArgs e)
        {
            if (Error != "")
            {
                string temp = Error;
                Error = "";
                MessageBox.Show(temp.TrimEnd('f'));
                if (!temp.EndsWith("f"))
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
            Number_Of_Clients.Text = clients.Count.ToString();
            if (Reset_Manager == true)
            {
                Reset_Manager = false;
                Selected_Index = 0;
                Client_Number.Text = Selected_Index.ToString();
                client_Info1.Update_Info(client_Info1.Defaults);
                client_Info1.BackColor = Color.Lime;
                return;
            }
        }

        /// <summary>
        /// Force disconnects a client from the server.
        /// </summary>
        /// <param name="sender">The button or object that initiates the kill command.</param>
        /// <param name="e">The triggering event.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (Selected_Index == 0)
                return;
            clients[Selected_Index - 1].Send("4: Kill");
            Selected_Index--;
            Client_Number.Text = Selected_Index.ToString();
            if (Selected_Index == 0)
            {
                client_Info1.Update_Info(client_Info1.Defaults);
                client_Info1.BackColor = Color.Lime;
                return;
            }
            client_Info1.Update_Info(clients[Selected_Index - 1].Client_Info);
            if (clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Red;
            if (!clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Lime;
        }

        /// <summary>
        /// Blocks a client from sending information.
        /// </summary>
        /// <param name="sender">The button or object that initiates the block command.</param>
        /// <param name="e">The triggering event.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (Selected_Index == 0)
                return;
            clients[Selected_Index - 1].blocked = !clients[Selected_Index - 1].blocked;
            if (clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Red;
            if (!clients[Selected_Index - 1].blocked)
                client_Info1.BackColor = Color.Lime;
            clients[Selected_Index - 1].Send("4: blocked");
        }

        /// <summary>
        /// Initiates the user interface and update's hard-drive info.
        /// </summary>
        /// <param name="sender">The object that initiates the load command.</param>
        /// <param name="e">The triggering event.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Save_Update_Loc = driver + Save_Update_Loc;
            if (File.Exists(Save_Update_Loc))
            {
                TextReader TR = new StreamReader(Save_Update_Loc);
                textBox1.Text = TR.ReadLine();
                TR.Close();
            }
        }

        /// <summary>
        /// Saves the location of the updated client .exe file.
        /// </summary>
        /// <param name="sender">The textbox or object that initiates the Location change command.</param>
        /// <param name="e">The triggering event.</param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Directory.CreateDirectory(driver + "Program Data\\");
            TextWriter TW = new StreamWriter(Save_Update_Loc);
            TW.WriteLine(textBox1.Text);
            TW.Close();
        }

        private void DUMP_Click(object sender, EventArgs e)
        {
            Client.Save_Info(clients[Selected_Index - 1]);
        }

        public List<string> Get_Users()
        {
            List<string> users = new List<string>();
            foreach (Client C in clients)
            {
                string user = C.username + Client.cTyping(C);
                users.Add(user);
            }
            return users;
        }
    }

    [Serializable]
    public class Client
    {
        public List<string> Client_Info = new List<string>();
        public StreamReader sr;
        public StreamWriter sw;
        [XmlIgnore] public List<Host> UI = new List<Host>();
        public int Client_Index;
        public bool updating = false;
        public bool typing = false;
        public bool request_chat_send = false;
        public bool sending_chat = false;
        public string username = "Guest";
        public bool blocked = false;
        public bool ftping = false;
        public string user_to_send_to = "";
        public List<Client> FTP_Connection = new List<Client>();
        public List<string> kilos = new List<string>();
        public bool busy = false;
        public List<string> To_Send = new List<string>();
        public int ftpbytes = 0;
        public bool adding_data = false;

        public Client() { }

        /// <summary>
        /// Creates a new instance of a client object.
        /// </summary>
        /// <param name="ui">The parent server control.</param>
        /// <param name="SW">The network data writer.</param>
        /// <param name="SR">The network data reader.</param>
        /// <param name="index">The clients index in the client array.</param>
        public Client(Host ui, StreamWriter SW, StreamReader SR, int index)
        {
            UI.Add(ui);
            sr = SR;
            sw = SW;
            Client_Index = index;
            new Thread(Loading).Start();
        }

        /// <summary>
        /// Iniiiates the client interface starting the command listener if no updates needed.
        /// </summary>
        public void Loading()
        {
            Check_For_Update();
            if (updating)
                return;
            Get_Client_Info();
            new Thread(Send_Data).Start();
            Command_Listener();
        }

        /// <summary>
        /// Checks for updates to client.
        /// </summary>
        public void Check_For_Update()
        {
            Send(UI[0].Version);
            string response = sr.ReadLine();
            if (response == "ready for update...")
            {
                Send_Updated_Exe();
                updating = true;
                return;
            }
        }

        /// <summary>
        /// Sends the updated client file to the current client.
        /// </summary>
        public void Send_Updated_Exe()
        {
            try {
                byte[] Chat_Update = File.ReadAllBytes(UI[0].textBox1.Text);
                List<string> Kilos = FileToStrings(Chat_Update);
                Send(Kilos.Count().ToString());
                foreach (string K in Kilos)
                    Send(K);
                KillConnection();
            }
            catch {
                KillConnection();
            }
            return;
        }

        private int Remainder(int Total)
        {
            if ((Total % 1024) == 0)
                return 0;
            return 1;
        }

        private List<string> FileToStrings(byte[] F)
        {
            List<string> Kilos = new List<string>();
            string Temp = "";
            for (int i = 0; i != F.Count(); i++)
            {
                if ((i % 1024) == 0 && i != 0)
                {
                    string N = Temp;
                    Kilos.Add(N);
                    Temp = "";
                }
                Temp += ByteToString(F[i]);
            }
            Kilos.Add(Temp);
            return Kilos;
        }

        private string ByteToString(byte B)
        {
            string S = Convert.ToString(B);
            while (S.Length != 3)
                S = "0" + S;
            return S;
        }

        /// <summary>
        /// The Client leaves the server and recycles resources.
        /// </summary>
        public void KillConnection()
        {
            sw.Dispose();
            sr.Dispose();
            UI[0].clients.RemoveAt(Client_Index);
            UI[0].Update_Index();
        }

        /// <summary>
        /// Sends something to the client.
        /// </summary>
        /// <param name="ToSend">The something to send.</param>
        public void Send(string ToSend)
        {
            sw.WriteLine(ToSend);
            sw.Flush();
        }

        /// <summary>
        /// Recieves the detailed information about the client.
        /// </summary>
        public void Get_Client_Info()
        {
            for (int i = 0; i != 5; i++)
            {
                string info = sr.ReadLine();
                Client_Info.Add(info);
            }
            username = sr.ReadLine();
            Update_Chat_List(username);
        }

        private void GetCommands() {
            DoCommand.Add("1: ", (s) => Update_Chat_List(s.Replace("1: ", "")));
            DoCommand.Add("2: ", (s) => Update_Message_Board(s.Replace("2: ", "")));
            DoCommand.Add("3: ", (s) => SetTyping(s));
            DoCommand.Add("4: ", (s) => kilos.Add(s.Replace("<heartbeat>", "")));
        }

        private void SetTyping(string command) {
            if (command == "3: typing" && !typing) {
                typing = true;
                Update_Chat_List(username);
            }
            if (command == "3: empty" && typing) {
                typing = false;
                Update_Chat_List(username);
            }
        } 

        private Dictionary<string, Action<string>> DoCommand = new Dictionary<string, Action<string>>();

        /// <summary>
        /// Listens to the network reader for new messages from the client.
        /// </summary>
        public void Command_Listener()
        {
            Login_Event();
            while (true) {
                try {
                    string command = sr.ReadLine();
                    if (command[0] == '1')
                        Update_Chat_List(command.Replace("1: ", ""));
                    if (command[0] == '2')
                        Update_Message_Board(command.Replace("2: ", ""));
                    if (command == "3: typing" && !typing) {
                        typing = true;
                        Update_Chat_List(username);
                    }
                    if (command == "3: empty" && typing) {
                        typing = false;
                        Update_Chat_List(username);
                    }
                    if (command[0] == '5')
                        Update_FTP_Status(command.Replace("5: ", ""));
                    if (command[0] == '6')
                        kilos.Add(command.Replace("<heartbeat>", ""));
                    if (command[0] == '7') {
                        if (kilos.Count != ftpbytes)
                            UI[0].Error = "ftp failure incorrect indexed amount of bytes...f";
                        kilos.Add(command);
                        new Thread(Send_File).Start();
                    }
                    if (command[0] == 'A')
                        ftpbytes = Convert.ToInt32(command.Replace("A: ", ""));
                }
                catch (Exception E) {
                    KillConnection();
                    UI[0].Log_it(username + " has disconnected...");
                    foreach (Client C in UI[0].clients)
                        C.Send("2: " + username + " has disconnected...");
                    Update_Chat_List("");
                    return;
                }
            }
        }

        public void Login_Event()
        {
            foreach (Client C in UI[0].clients)
                C.Send("2: User: " + username + " has connected...");
            UI[0].Log_it(username + " has connected..." + DateTime.Now.ToString());
            UI[0].Log_it("Client username : " + Client_Info[0]);
            UI[0].Log_it("Client computer name : " + Client_Info[1]);
            UI[0].Log_it("Client operating system : " + Client_Info[2]);
            UI[0].Log_it("Client lan IP address : " + Client_Info[3]);
            UI[0].Log_it("Client wan IP address : " + Client_Info[4]);
        }

        /// <summary>
        /// Sends an FTP'ed file.
        /// </summary>
        public void Send_File()
        {
            FTP_Connection[0].adding_data = true;
            foreach (string kilo in kilos)
            {
                FTP_Connection[0].To_Send.Add(kilo);
            }
            ftping = false;
            FTP_Connection[0].adding_data = false;
            FTP_Connection[0].ftping = false;
            FTP_Connection[0].FTP_Connection.Clear();
            FTP_Connection.Clear();
        }

        /// <summary>
        /// Sends data to the client.
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
                        Send(To_Send[I]);
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
        /// Updates the FTP status of the client object.
        /// </summary>
        /// <param name="user">The user to send the ftp to.</param>
        public void Update_FTP_Status(string user)
        {
            string filename = "";
            bool building = false;
            foreach (char c in user)
            {
                if (building)
                    filename = filename + c;
                else if (c == ':')
                    building = true;
            }
            user_to_send_to = user.Replace(":" + filename, "");
            foreach (Client User in UI[0].clients)
            {
                if (User.username == user_to_send_to)
                {
                    User.To_Send.Add("5: " + username);
                    User.To_Send.Add(filename);
                    FTP_Connection.Add(User);
                    FTP_Connection[0].FTP_Connection.Add(this);
                }
            }
        }

        /// <summary>
        /// Updates the client's username and makes the clients aware of who is in chat.
        /// </summary>
        /// <param name="New_User_Name">The new username for the client.</param>
        public void Update_Chat_List(string New_User_Name)
        {
            username = New_User_Name;
            List<string> users = UI[0].Get_Users();
            string list = "1: ";
            foreach (string user in users)
                list = list + user + "-";
            foreach (Client C in UI[0].clients)
                C.To_Send.Add(list);
        }


        /// <summary>
        /// A simple test and return to determine typing status.
        /// </summary>
        /// <param name="C">The client to test.</param>
        /// <returns>The Character indicating typing status.</returns>
        public static char cTyping(Client C)
        {
            if (C.typing)
                return 'Y';
            else
                return 'N';
        }

        /// <summary>
        /// Sends a chat message to the client array.
        /// </summary>
        /// <param name="Message">The message to send.</param>
        public void Update_Message_Board(string Message)
        {
            if (Message[0] == '@')
            {
                string User_To_Send_To = "";
                string construct = "";
                bool finished = false;
                bool build = true;
                foreach (char C in Message) {
                    if (construct == "@" + username + " whispers to you; ") {
                        build = false;
                        if (!finished && C == ';')
                            finished = true;
                        else if (!finished)
                            User_To_Send_To = User_To_Send_To + C;
                    }
                    else if (build)
                        construct = construct + C;
                }
                foreach (Client C in UI[0].clients) {
                    if (C.username == User_To_Send_To) {
                        C.To_Send.Add("2: " + username + " " + Message.Replace("@", "").Replace(username, "").Replace(";", "").Replace(User_To_Send_To, ""));
                        UI[0].Log_it(username + " whispers to " + User_To_Send_To + Message.Replace(";", "").Replace(" whispers to you ", "").Replace("@", "").Replace(username, "").Replace(User_To_Send_To, ""));
                    }
                }
            }
            else
            {
                UI[0].Log_it(username + ": " + Message + " " + DateTime.Now.ToString());
                foreach (Client C in UI[0].clients)
                    C.To_Send.Add("2: " + username + ": " + Message);
            }
        }

        /// <summary>
        /// Saves client info.
        /// </summary>
        /// <param name="C">The Client to save info of.</param>
        public static void Save_Info(Client C)
        {
            XmlSerializer XS = new XmlSerializer(typeof(Client));
            StreamWriter SW = new StreamWriter(@"ClientInfo.xml");
            XS.Serialize(SW, C);
            SW.Close();
        }
    }
}
