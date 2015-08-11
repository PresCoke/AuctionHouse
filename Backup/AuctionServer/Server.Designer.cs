namespace AuctionServer
{
    partial class ServerView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerView));
            this.ListenButton = new System.Windows.Forms.Button();
            this.ListenOnPortLabel = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.ServerIPAddressLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serverIPAddressTextBox = new UnEditableRichTextBox.UERichTextBox();
            this.ServerStatusTextBox = new UnEditableRichTextBox.UERichTextBox();
            this.AddAuctionItemButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(205, 12);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(75, 23);
            this.ListenButton.TabIndex = 0;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            // 
            // ListenOnPortLabel
            // 
            this.ListenOnPortLabel.AutoSize = true;
            this.ListenOnPortLabel.Location = new System.Drawing.Point(12, 17);
            this.ListenOnPortLabel.Name = "ListenOnPortLabel";
            this.ListenOnPortLabel.Size = new System.Drawing.Size(77, 13);
            this.ListenOnPortLabel.TabIndex = 1;
            this.ListenOnPortLabel.Text = "Listen On Port:";
            this.ListenOnPortLabel.UseMnemonic = false;
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(110, 14);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(78, 20);
            this.PortTextBox.TabIndex = 2;
            // 
            // ServerIPAddressLabel
            // 
            this.ServerIPAddressLabel.AutoSize = true;
            this.ServerIPAddressLabel.Location = new System.Drawing.Point(12, 44);
            this.ServerIPAddressLabel.Name = "ServerIPAddressLabel";
            this.ServerIPAddressLabel.Size = new System.Drawing.Size(92, 13);
            this.ServerIPAddressLabel.TabIndex = 4;
            this.ServerIPAddressLabel.Text = "Server IP Address";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            // 
            // serverIPAddressTextBox
            // 
            this.serverIPAddressTextBox.BackColor = System.Drawing.Color.White;
            this.serverIPAddressTextBox.Location = new System.Drawing.Point(110, 41);
            this.serverIPAddressTextBox.Name = "serverIPAddressTextBox";
            this.serverIPAddressTextBox.ReadOnly = true;
            this.serverIPAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.serverIPAddressTextBox.TabIndex = 6;
            this.serverIPAddressTextBox.Text = "";
            // 
            // ServerStatusTextBox
            // 
            this.ServerStatusTextBox.BackColor = System.Drawing.Color.White;
            this.ServerStatusTextBox.Location = new System.Drawing.Point(15, 90);
            this.ServerStatusTextBox.Name = "ServerStatusTextBox";
            this.ServerStatusTextBox.ReadOnly = true;
            this.ServerStatusTextBox.Size = new System.Drawing.Size(265, 171);
            this.ServerStatusTextBox.TabIndex = 5;
            this.ServerStatusTextBox.Text = "";
            // 
            // AddAuctionItemButton
            // 
            this.AddAuctionItemButton.Location = new System.Drawing.Point(257, 39);
            this.AddAuctionItemButton.Name = "AddAuctionItemButton";
            this.AddAuctionItemButton.Size = new System.Drawing.Size(23, 23);
            this.AddAuctionItemButton.TabIndex = 7;
            this.AddAuctionItemButton.Text = "+";
            this.AddAuctionItemButton.UseVisualStyleBackColor = true;
            // 
            // ServerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.AddAuctionItemButton);
            this.Controls.Add(this.serverIPAddressTextBox);
            this.Controls.Add(this.ServerStatusTextBox);
            this.Controls.Add(this.ServerIPAddressLabel);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.ListenOnPortLabel);
            this.Controls.Add(this.ListenButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "ServerView";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Label ListenOnPortLabel;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label ServerIPAddressLabel;
        private UnEditableRichTextBox.UERichTextBox ServerStatusTextBox;
        private UnEditableRichTextBox.UERichTextBox serverIPAddressTextBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button AddAuctionItemButton;
    }
}

