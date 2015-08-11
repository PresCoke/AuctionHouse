namespace AuctionClient
{
    partial class ClientView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientView));
            this.JoinExitButton = new System.Windows.Forms.Button();
            this.ServerIPAddressTextBox = new System.Windows.Forms.TextBox();
            this.ServerIPAddressLabel = new System.Windows.Forms.Label();
            this.PortNumberTextBox = new System.Windows.Forms.TextBox();
            this.PortNumberLabel = new System.Windows.Forms.Label();
            this.auctionItemPanel = new System.Windows.Forms.Panel();
            this.analogClock1 = new AuctionClient.AnalogClock();
            this.SuspendLayout();
            // 
            // JoinExitButton
            // 
            this.JoinExitButton.Location = new System.Drawing.Point(347, 10);
            this.JoinExitButton.Name = "JoinExitButton";
            this.JoinExitButton.Size = new System.Drawing.Size(75, 23);
            this.JoinExitButton.TabIndex = 3;
            this.JoinExitButton.Text = "Join Auction";
            this.JoinExitButton.UseVisualStyleBackColor = true;
            // 
            // ServerIPAddressTextBox
            // 
            this.ServerIPAddressTextBox.Location = new System.Drawing.Point(241, 12);
            this.ServerIPAddressTextBox.Name = "ServerIPAddressTextBox";
            this.ServerIPAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.ServerIPAddressTextBox.TabIndex = 2;
            // 
            // ServerIPAddressLabel
            // 
            this.ServerIPAddressLabel.AutoSize = true;
            this.ServerIPAddressLabel.Location = new System.Drawing.Point(140, 15);
            this.ServerIPAddressLabel.Name = "ServerIPAddressLabel";
            this.ServerIPAddressLabel.Size = new System.Drawing.Size(95, 13);
            this.ServerIPAddressLabel.TabIndex = 2;
            this.ServerIPAddressLabel.Text = "Server IP Address:";
            // 
            // PortNumberTextBox
            // 
            this.PortNumberTextBox.Location = new System.Drawing.Point(81, 12);
            this.PortNumberTextBox.Name = "PortNumberTextBox";
            this.PortNumberTextBox.Size = new System.Drawing.Size(53, 20);
            this.PortNumberTextBox.TabIndex = 1;
            // 
            // PortNumberLabel
            // 
            this.PortNumberLabel.AutoSize = true;
            this.PortNumberLabel.Location = new System.Drawing.Point(6, 15);
            this.PortNumberLabel.Name = "PortNumberLabel";
            this.PortNumberLabel.Size = new System.Drawing.Size(69, 13);
            this.PortNumberLabel.TabIndex = 4;
            this.PortNumberLabel.Text = "Port Number:";
            // 
            // auctionItemPanel
            // 
            this.auctionItemPanel.AutoScroll = true;
            this.auctionItemPanel.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.auctionItemPanel.AutoScrollMinSize = new System.Drawing.Size(50, 50);
            this.auctionItemPanel.BackColor = System.Drawing.Color.Transparent;
            this.auctionItemPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.auctionItemPanel.Location = new System.Drawing.Point(9, 47);
            this.auctionItemPanel.Name = "auctionItemPanel";
            this.auctionItemPanel.Size = new System.Drawing.Size(568, 281);
            this.auctionItemPanel.TabIndex = 6;
            // 
            // analogClock1
            // 
            this.analogClock1.Location = new System.Drawing.Point(589, 104);
            this.analogClock1.Name = "analogClock1";
            this.analogClock1.Size = new System.Drawing.Size(149, 150);
            this.analogClock1.TabIndex = 7;
            //
            // ClientView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 340);
            this.Controls.Add(this.analogClock1);
            this.Controls.Add(this.auctionItemPanel);
            this.Controls.Add(this.PortNumberLabel);
            this.Controls.Add(this.PortNumberTextBox);
            this.Controls.Add(this.ServerIPAddressLabel);
            this.Controls.Add(this.ServerIPAddressTextBox);
            this.Controls.Add(this.JoinExitButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(601, 367);
            this.Name = "ClientView";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button JoinExitButton;
        private System.Windows.Forms.TextBox ServerIPAddressTextBox;
        private System.Windows.Forms.Label ServerIPAddressLabel;
        private System.Windows.Forms.TextBox PortNumberTextBox;
        private System.Windows.Forms.Label PortNumberLabel;
        private System.Windows.Forms.Panel auctionItemPanel;
        private AnalogClock analogClock1;
    }
}

