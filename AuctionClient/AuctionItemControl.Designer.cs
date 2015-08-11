namespace AuctionClient
{
    partial class AuctionItemControl
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
            this.ItemPicture = new System.Windows.Forms.PictureBox();
            this.ItemDescriptionLabel = new System.Windows.Forms.Label();
            this.BidButton = new System.Windows.Forms.Button();
            this.TimeLeftLabel = new System.Windows.Forms.Label();
            this.HighestBidLabel = new System.Windows.Forms.Label();
            this.UsersCurrentBidLabel = new System.Windows.Forms.Label();
            this.CurrentBidTextBox = new System.Windows.Forms.TextBox();
            this.ItemStatusTextBox = new UnEditableRichTextBox.UERichTextBox();
            this.HighestBidTextBox = new UnEditableRichTextBox.UERichTextBox();
            this.TimeLeftTextBox = new UnEditableRichTextBox.UERichTextBox();
            this.ItemDescriptionTextBox = new UnEditableRichTextBox.UERichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemPicture
            // 
            this.ItemPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ItemPicture.Location = new System.Drawing.Point(3, 3);
            this.ItemPicture.Name = "ItemPicture";
            this.ItemPicture.Size = new System.Drawing.Size(120, 120);
            this.ItemPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ItemPicture.TabIndex = 0;
            this.ItemPicture.TabStop = false;
            // 
            // ItemDescriptionLabel
            // 
            this.ItemDescriptionLabel.AutoSize = true;
            this.ItemDescriptionLabel.Location = new System.Drawing.Point(129, 4);
            this.ItemDescriptionLabel.Name = "ItemDescriptionLabel";
            this.ItemDescriptionLabel.Size = new System.Drawing.Size(83, 13);
            this.ItemDescriptionLabel.TabIndex = 1;
            this.ItemDescriptionLabel.Text = "ItemDescription:";
            // 
            // BidButton
            // 
            this.BidButton.Location = new System.Drawing.Point(447, 99);
            this.BidButton.Name = "BidButton";
            this.BidButton.Size = new System.Drawing.Size(53, 23);
            this.BidButton.TabIndex = 4;
            this.BidButton.Text = "Bid";
            this.BidButton.UseVisualStyleBackColor = true;
            // 
            // TimeLeftLabel
            // 
            this.TimeLeftLabel.AutoSize = true;
            this.TimeLeftLabel.Location = new System.Drawing.Point(129, 74);
            this.TimeLeftLabel.Name = "TimeLeftLabel";
            this.TimeLeftLabel.Size = new System.Drawing.Size(51, 13);
            this.TimeLeftLabel.TabIndex = 6;
            this.TimeLeftLabel.Text = "TimeLeft:";
            // 
            // HighestBidLabel
            // 
            this.HighestBidLabel.AutoSize = true;
            this.HighestBidLabel.Location = new System.Drawing.Point(166, 104);
            this.HighestBidLabel.Name = "HighestBidLabel";
            this.HighestBidLabel.Size = new System.Drawing.Size(64, 13);
            this.HighestBidLabel.TabIndex = 8;
            this.HighestBidLabel.Text = "Highest Bid:";
            // 
            // UsersCurrentBidLabel
            // 
            this.UsersCurrentBidLabel.AutoSize = true;
            this.UsersCurrentBidLabel.Location = new System.Drawing.Point(310, 104);
            this.UsersCurrentBidLabel.Name = "UsersCurrentBidLabel";
            this.UsersCurrentBidLabel.Size = new System.Drawing.Size(62, 13);
            this.UsersCurrentBidLabel.TabIndex = 11;
            this.UsersCurrentBidLabel.Text = "Current Bid:";
            // 
            // CurrentBidTextBox
            // 
            this.CurrentBidTextBox.Location = new System.Drawing.Point(378, 101);
            this.CurrentBidTextBox.Name = "CurrentBidTextBox";
            this.CurrentBidTextBox.Size = new System.Drawing.Size(63, 20);
            this.CurrentBidTextBox.TabIndex = 12;
            // 
            // ItemStatusTextBox
            // 
            this.ItemStatusTextBox.BackColor = System.Drawing.Color.White;
            this.ItemStatusTextBox.Location = new System.Drawing.Point(3, 129);
            this.ItemStatusTextBox.Name = "ItemStatusTextBox";
            this.ItemStatusTextBox.ReadOnly = true;
            this.ItemStatusTextBox.Size = new System.Drawing.Size(497, 48);
            this.ItemStatusTextBox.TabIndex = 10;
            this.ItemStatusTextBox.Text = "";
            // 
            // HighestBidTextBox
            // 
            this.HighestBidTextBox.BackColor = System.Drawing.Color.White;
            this.HighestBidTextBox.Location = new System.Drawing.Point(236, 101);
            this.HighestBidTextBox.Name = "HighestBidTextBox";
            this.HighestBidTextBox.ReadOnly = true;
            this.HighestBidTextBox.Size = new System.Drawing.Size(63, 20);
            this.HighestBidTextBox.TabIndex = 9;
            this.HighestBidTextBox.Text = "";
            // 
            // TimeLeftTextBox
            // 
            this.TimeLeftTextBox.BackColor = System.Drawing.Color.White;
            this.TimeLeftTextBox.Location = new System.Drawing.Point(186, 71);
            this.TimeLeftTextBox.Name = "TimeLeftTextBox";
            this.TimeLeftTextBox.ReadOnly = true;
            this.TimeLeftTextBox.Size = new System.Drawing.Size(314, 23);
            this.TimeLeftTextBox.TabIndex = 7;
            this.TimeLeftTextBox.Text = "";
            // 
            // ItemDescriptionTextBox
            // 
            this.ItemDescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.ItemDescriptionTextBox.Location = new System.Drawing.Point(129, 20);
            this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
            this.ItemDescriptionTextBox.ReadOnly = true;
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(371, 47);
            this.ItemDescriptionTextBox.TabIndex = 2;
            this.ItemDescriptionTextBox.Text = "";
            // 
            // AuctionItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CurrentBidTextBox);
            this.Controls.Add(this.UsersCurrentBidLabel);
            this.Controls.Add(this.ItemStatusTextBox);
            this.Controls.Add(this.HighestBidTextBox);
            this.Controls.Add(this.HighestBidLabel);
            this.Controls.Add(this.TimeLeftTextBox);
            this.Controls.Add(this.TimeLeftLabel);
            this.Controls.Add(this.BidButton);
            this.Controls.Add(this.ItemDescriptionTextBox);
            this.Controls.Add(this.ItemDescriptionLabel);
            this.Controls.Add(this.ItemPicture);
            this.Name = "AuctionItemControl";
            this.Size = new System.Drawing.Size(504, 181);
            ((System.ComponentModel.ISupportInitialize)(this.ItemPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ItemPicture;
        private System.Windows.Forms.Label ItemDescriptionLabel;
        private UnEditableRichTextBox.UERichTextBox ItemDescriptionTextBox;
        private System.Windows.Forms.Button BidButton;
        private System.Windows.Forms.Label TimeLeftLabel;
        private UnEditableRichTextBox.UERichTextBox TimeLeftTextBox;
        private System.Windows.Forms.Label HighestBidLabel;
        private UnEditableRichTextBox.UERichTextBox HighestBidTextBox;
        private UnEditableRichTextBox.UERichTextBox ItemStatusTextBox;
        private System.Windows.Forms.Label UsersCurrentBidLabel;
        private System.Windows.Forms.TextBox CurrentBidTextBox;
    }
}
