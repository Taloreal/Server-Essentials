using System.Diagnostics;
using System.IO;

namespace WindowsFormsApplication2.Client
{
    partial class Chat
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
            Process.GetCurrentProcess().Kill();
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
            this.components = new System.ComponentModel.Container();
            this.MessageBoard = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Userlist = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Thread_Scanner = new System.Windows.Forms.Timer(this.components);
            this.Username = new System.Windows.Forms.TextBox();
            this.Open_Settings = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.Open_FTP = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // Chat
            // 
            this.MessageBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageBoard.Location = new System.Drawing.Point(12, 30);
            this.MessageBoard.Multiline = true;
            this.MessageBoard.Name = "Chat";
            this.MessageBoard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessageBoard.Size = new System.Drawing.Size(559, 315);
            this.MessageBoard.TabIndex = 0;
            this.MessageBoard.TextChanged += new System.EventHandler(this.Chat_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(574, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Connected :";
            // 
            // Userlist
            // 
            this.Userlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Userlist.Location = new System.Drawing.Point(577, 30);
            this.Userlist.Name = "Userlist";
            this.Userlist.Size = new System.Drawing.Size(207, 315);
            this.Userlist.TabIndex = 7;
            this.Userlist.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Userlist_AfterSelect);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Message :";
            // 
            // Message
            // 
            this.Message.Enabled = false;
            this.Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.Location = new System.Drawing.Point(87, 348);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(484, 24);
            this.Message.TabIndex = 12;
            this.Message.TextChanged += new System.EventHandler(this.Message_TextChanged);
            this.Message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Message_KeyDown);
            // 
            // Send
            // 
            this.Send.Enabled = false;
            this.Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Send.Location = new System.Drawing.Point(577, 347);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(207, 27);
            this.Send.TabIndex = 13;
            this.Send.Text = "Send message";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Username :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "Message Board ";
            // 
            // Thread_Scanner
            // 
            this.Thread_Scanner.Enabled = true;
            this.Thread_Scanner.Interval = 1000;
            this.Thread_Scanner.Tick += new System.EventHandler(this.Thread_Scanner_Tick);
            // 
            // Username
            // 
            this.Username.Enabled = false;
            this.Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(103, 378);
            this.Username.MaxLength = 9;
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(108, 24);
            this.Username.TabIndex = 26;
            this.Username.Text = "guest";
            this.Username.TextChanged += new System.EventHandler(this.Username_TextChanged);
            // 
            // Open_Settings
            // 
            this.Open_Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Open_Settings.Location = new System.Drawing.Point(217, 377);
            this.Open_Settings.Name = "Open_Settings";
            this.Open_Settings.Size = new System.Drawing.Size(115, 27);
            this.Open_Settings.TabIndex = 29;
            this.Open_Settings.Text = "Open Settings";
            this.Open_Settings.UseVisualStyleBackColor = true;
            this.Open_Settings.Click += new System.EventHandler(this.Open_Settings_Click);
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.Location = new System.Drawing.Point(459, 381);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(53, 18);
            this.Time.TabIndex = 30;
            this.Time.Text = "Time : ";
            // 
            // Open_FTP
            // 
            this.Open_FTP.Enabled = false;
            this.Open_FTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Open_FTP.Location = new System.Drawing.Point(338, 377);
            this.Open_FTP.Name = "Open_FTP";
            this.Open_FTP.Size = new System.Drawing.Size(115, 27);
            this.Open_FTP.TabIndex = 31;
            this.Open_FTP.Text = "Open FTP";
            this.Open_FTP.UseVisualStyleBackColor = true;
            this.Open_FTP.Visible = false;
            this.Open_FTP.Click += new System.EventHandler(this.Open_FTP_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 410);
            this.Controls.Add(this.Open_FTP);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.Open_Settings);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Userlist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MessageBoard);
            this.MinimumSize = new System.Drawing.Size(718, 296);
            this.Name = "Form1";
            this.Text = "Naates Chat Program v1.7";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessageBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView Userlist;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer Thread_Scanner;
        private System.Windows.Forms.Button Open_Settings;
        public System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Button Open_FTP;
        private EventLog eventLog1;
    }
}

