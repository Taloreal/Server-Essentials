namespace WindowsFormsApplication2.Server
{
    partial class Client_Info
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.Computer_Name = new System.Windows.Forms.Label();
            this.Operating_System = new System.Windows.Forms.Label();
            this.Wan_IP = new System.Windows.Forms.Label();
            this.Lan_IP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client username : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Client computer name : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Client operating system : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Client lan IP address: : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Client wan IP address : ";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(117, 0);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(59, 13);
            this.Username.TabIndex = 5;
            this.Username.Text = "[username]";
            // 
            // Computer_Name
            // 
            this.Computer_Name.AutoSize = true;
            this.Computer_Name.Location = new System.Drawing.Point(117, 13);
            this.Computer_Name.Name = "Computer_Name";
            this.Computer_Name.Size = new System.Drawing.Size(86, 13);
            this.Computer_Name.TabIndex = 6;
            this.Computer_Name.Text = "[computer name]";
            // 
            // Operating_System
            // 
            this.Operating_System.AutoSize = true;
            this.Operating_System.Location = new System.Drawing.Point(117, 26);
            this.Operating_System.Name = "Operating_System";
            this.Operating_System.Size = new System.Drawing.Size(92, 13);
            this.Operating_System.TabIndex = 7;
            this.Operating_System.Text = "[operating system]";
            // 
            // Wan_IP
            // 
            this.Wan_IP.AutoSize = true;
            this.Wan_IP.Location = new System.Drawing.Point(117, 52);
            this.Wan_IP.Name = "Wan_IP";
            this.Wan_IP.Size = new System.Drawing.Size(46, 13);
            this.Wan_IP.TabIndex = 8;
            this.Wan_IP.Text = "[wan IP]";
            // 
            // Lan_IP
            // 
            this.Lan_IP.AutoSize = true;
            this.Lan_IP.Location = new System.Drawing.Point(117, 39);
            this.Lan_IP.Name = "Lan_IP";
            this.Lan_IP.Size = new System.Drawing.Size(40, 13);
            this.Lan_IP.TabIndex = 9;
            this.Lan_IP.Text = "[lan IP]";
            // 
            // Client_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.Controls.Add(this.Lan_IP);
            this.Controls.Add(this.Wan_IP);
            this.Controls.Add(this.Operating_System);
            this.Controls.Add(this.Computer_Name);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Client_Info";
            this.Size = new System.Drawing.Size(337, 70);
            this.Load += new System.EventHandler(this.Client_Info_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label Username;
        public System.Windows.Forms.Label Computer_Name;
        public System.Windows.Forms.Label Operating_System;
        public System.Windows.Forms.Label Wan_IP;
        public System.Windows.Forms.Label Lan_IP;
    }
}
