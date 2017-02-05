namespace WindowsFormsApplication2.Client
{
    partial class FTP
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
            this.Hide();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Directories = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Files = new System.Windows.Forms.TreeView();
            this.Current_Directory = new System.Windows.Forms.Label();
            this.User_To_Send_To = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Directories
            // 
            this.Directories.Location = new System.Drawing.Point(12, 30);
            this.Directories.Name = "Directories";
            this.Directories.Size = new System.Drawing.Size(200, 306);
            this.Directories.TabIndex = 0;
            this.Directories.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Directories_NodeMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Directories :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(215, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Files :";
            // 
            // Files
            // 
            this.Files.Location = new System.Drawing.Point(218, 30);
            this.Files.Name = "Files";
            this.Files.Size = new System.Drawing.Size(200, 306);
            this.Files.TabIndex = 8;
            this.Files.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Files_NodeMouseClick);
            // 
            // Current_Directory
            // 
            this.Current_Directory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Current_Directory.Location = new System.Drawing.Point(12, 335);
            this.Current_Directory.Name = "Current_Directory";
            this.Current_Directory.Size = new System.Drawing.Size(406, 83);
            this.Current_Directory.TabIndex = 10;
            this.Current_Directory.Text = "Current directory : ";
            // 
            // User_To_Send_To
            // 
            this.User_To_Send_To.FormattingEnabled = true;
            this.User_To_Send_To.Location = new System.Drawing.Point(200, 426);
            this.User_To_Send_To.Name = "User_To_Send_To";
            this.User_To_Send_To.Size = new System.Drawing.Size(129, 21);
            this.User_To_Send_To.TabIndex = 11;
            this.User_To_Send_To.TextChanged += new System.EventHandler(this.User_To_Send_To_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(76, 425);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "User to send to :";
            // 
            // Send
            // 
            this.Send.Enabled = false;
            this.Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Send.Location = new System.Drawing.Point(335, 421);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(81, 27);
            this.Send.TabIndex = 14;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 452);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.User_To_Send_To);
            this.Controls.Add(this.Current_Directory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Files);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Directories);
            this.Name = "FTP";
            this.Text = "Naate\'s Chat FTP";
            this.Load += new System.EventHandler(this.FTP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView Directories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView Files;
        private System.Windows.Forms.Label Current_Directory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Send;
        public System.Windows.Forms.ComboBox User_To_Send_To;
    }
}