using System.Diagnostics;

namespace WindowsFormsApplication2.Server
{
    partial class Host
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
            this.Start_Server = new System.Windows.Forms.Button();
            this.Stop_Server = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Client_Number = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Back_One_Client = new System.Windows.Forms.Button();
            this.Forward_One_Client = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Number_Of_Clients = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Client_Manager_Helper = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.client_Info1 = new Client_Info();
            this.DUMP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_Server
            // 
            this.Start_Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_Server.Location = new System.Drawing.Point(12, 12);
            this.Start_Server.Name = "Start_Server";
            this.Start_Server.Size = new System.Drawing.Size(166, 57);
            this.Start_Server.TabIndex = 0;
            this.Start_Server.Text = "Start Server";
            this.Start_Server.UseVisualStyleBackColor = true;
            this.Start_Server.Click += new System.EventHandler(this.Start_Server_Click);
            // 
            // Stop_Server
            // 
            this.Stop_Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stop_Server.Location = new System.Drawing.Point(184, 12);
            this.Stop_Server.Name = "Stop_Server";
            this.Stop_Server.Size = new System.Drawing.Size(166, 57);
            this.Stop_Server.TabIndex = 1;
            this.Stop_Server.Text = "Stop Server";
            this.Stop_Server.UseVisualStyleBackColor = true;
            this.Stop_Server.Click += new System.EventHandler(this.Stop_Server_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Client Selected :";
            // 
            // Client_Number
            // 
            this.Client_Number.AutoSize = true;
            this.Client_Number.Location = new System.Drawing.Point(101, 161);
            this.Client_Number.Name = "Client_Number";
            this.Client_Number.Size = new System.Drawing.Size(13, 13);
            this.Client_Number.TabIndex = 4;
            this.Client_Number.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Client Manager :";
            // 
            // Back_One_Client
            // 
            this.Back_One_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_One_Client.Location = new System.Drawing.Point(12, 177);
            this.Back_One_Client.Name = "Back_One_Client";
            this.Back_One_Client.Size = new System.Drawing.Size(83, 57);
            this.Back_One_Client.TabIndex = 6;
            this.Back_One_Client.Text = "Previous Client";
            this.Back_One_Client.UseVisualStyleBackColor = true;
            this.Back_One_Client.Click += new System.EventHandler(this.Back_One_Client_Click);
            // 
            // Forward_One_Client
            // 
            this.Forward_One_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Forward_One_Client.Location = new System.Drawing.Point(95, 177);
            this.Forward_One_Client.Name = "Forward_One_Client";
            this.Forward_One_Client.Size = new System.Drawing.Size(65, 57);
            this.Forward_One_Client.TabIndex = 7;
            this.Forward_One_Client.Text = "Next Client";
            this.Forward_One_Client.UseVisualStyleBackColor = true;
            this.Forward_One_Client.Click += new System.EventHandler(this.Forward_One_Client_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Clients Connected :";
            // 
            // Number_Of_Clients
            // 
            this.Number_Of_Clients.AutoSize = true;
            this.Number_Of_Clients.Location = new System.Drawing.Point(260, 161);
            this.Number_Of_Clients.Name = "Number_Of_Clients";
            this.Number_Of_Clients.Size = new System.Drawing.Size(13, 13);
            this.Number_Of_Clients.TabIndex = 9;
            this.Number_Of_Clients.Text = "0";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(166, 177);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(59, 57);
            this.button3.TabIndex = 10;
            this.button3.Text = "Kill Client";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(225, 177);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(64, 57);
            this.button4.TabIndex = 11;
            this.button4.Text = "Block Client";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Client_Manager_Helper
            // 
            this.Client_Manager_Helper.Enabled = true;
            this.Client_Manager_Helper.Interval = 500;
            this.Client_Manager_Helper.Tick += new System.EventHandler(this.Client_Manager_Helper_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Updated exe location : ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 253);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(338, 46);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // client_Info1
            // 
            this.client_Info1.BackColor = System.Drawing.Color.Lime;
            this.client_Info1.Location = new System.Drawing.Point(12, 88);
            this.client_Info1.Name = "client_Info1";
            this.client_Info1.Size = new System.Drawing.Size(337, 70);
            this.client_Info1.TabIndex = 2;
            // 
            // DUMP
            // 
            this.DUMP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DUMP.Location = new System.Drawing.Point(295, 177);
            this.DUMP.Name = "DUMP";
            this.DUMP.Size = new System.Drawing.Size(64, 57);
            this.DUMP.TabIndex = 14;
            this.DUMP.Text = "Dump Client";
            this.DUMP.UseVisualStyleBackColor = true;
            this.DUMP.Click += new System.EventHandler(this.DUMP_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 238);
            this.Controls.Add(this.DUMP);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Number_Of_Clients);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Forward_One_Client);
            this.Controls.Add(this.Back_One_Client);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Client_Number);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.client_Info1);
            this.Controls.Add(this.Stop_Server);
            this.Controls.Add(this.Start_Server);
            this.Name = "Form1";
            this.Text = "Naates Chat Server v15";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Server;
        private System.Windows.Forms.Button Stop_Server;
        private Client_Info client_Info1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Client_Number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Back_One_Client;
        private System.Windows.Forms.Button Forward_One_Client;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Number_Of_Clients;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer Client_Manager_Helper;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button DUMP;
    }
}

