namespace AuctionServer
{
    partial class AddItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemForm));
            this.ItemIDLabel = new System.Windows.Forms.Label();
            this.NewitemIDTextBox = new System.Windows.Forms.TextBox();
            this.newItemStartBidTextBox = new System.Windows.Forms.TextBox();
            this.newItemImagePathTextBox = new System.Windows.Forms.TextBox();
            this.StartBidLabel = new System.Windows.Forms.Label();
            this.ImagePathLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.newItemDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.AddItemButton = new System.Windows.Forms.Button();
            this.FileBrowserButton = new System.Windows.Forms.Button();
            this.chooseImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // ItemIDLabel
            // 
            this.ItemIDLabel.AutoSize = true;
            this.ItemIDLabel.Location = new System.Drawing.Point(13, 9);
            this.ItemIDLabel.Name = "ItemIDLabel";
            this.ItemIDLabel.Size = new System.Drawing.Size(44, 13);
            this.ItemIDLabel.TabIndex = 0;
            this.ItemIDLabel.Text = "Item ID:";
            // 
            // NewitemIDTextBox
            // 
            this.NewitemIDTextBox.Location = new System.Drawing.Point(83, 6);
            this.NewitemIDTextBox.Name = "NewitemIDTextBox";
            this.NewitemIDTextBox.Size = new System.Drawing.Size(100, 20);
            this.NewitemIDTextBox.TabIndex = 1;
            // 
            // newItemStartBidTextBox
            // 
            this.newItemStartBidTextBox.Location = new System.Drawing.Point(83, 32);
            this.newItemStartBidTextBox.Name = "newItemStartBidTextBox";
            this.newItemStartBidTextBox.Size = new System.Drawing.Size(100, 20);
            this.newItemStartBidTextBox.TabIndex = 3;
            // 
            // newItemImagePathTextBox
            // 
            this.newItemImagePathTextBox.Location = new System.Drawing.Point(83, 58);
            this.newItemImagePathTextBox.Name = "newItemImagePathTextBox";
            this.newItemImagePathTextBox.Size = new System.Drawing.Size(100, 20);
            this.newItemImagePathTextBox.TabIndex = 4;
            // 
            // StartBidLabel
            // 
            this.StartBidLabel.AutoSize = true;
            this.StartBidLabel.Location = new System.Drawing.Point(13, 35);
            this.StartBidLabel.Name = "StartBidLabel";
            this.StartBidLabel.Size = new System.Drawing.Size(64, 13);
            this.StartBidLabel.TabIndex = 5;
            this.StartBidLabel.Text = "Starting Bid:";
            // 
            // ImagePathLabel
            // 
            this.ImagePathLabel.AutoSize = true;
            this.ImagePathLabel.Location = new System.Drawing.Point(13, 60);
            this.ImagePathLabel.Name = "ImagePathLabel";
            this.ImagePathLabel.Size = new System.Drawing.Size(64, 13);
            this.ImagePathLabel.TabIndex = 6;
            this.ImagePathLabel.Text = "Image Path:";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(13, 87);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(86, 13);
            this.descriptionLabel.TabIndex = 7;
            this.descriptionLabel.Text = "Item Description:";
            // 
            // newItemDescriptionTextBox
            // 
            this.newItemDescriptionTextBox.Location = new System.Drawing.Point(16, 103);
            this.newItemDescriptionTextBox.Name = "newItemDescriptionTextBox";
            this.newItemDescriptionTextBox.Size = new System.Drawing.Size(167, 158);
            this.newItemDescriptionTextBox.TabIndex = 8;
            this.newItemDescriptionTextBox.Text = "";
            // 
            // AddItemButton
            // 
            this.AddItemButton.Location = new System.Drawing.Point(205, 237);
            this.AddItemButton.Name = "AddItemButton";
            this.AddItemButton.Size = new System.Drawing.Size(75, 23);
            this.AddItemButton.TabIndex = 9;
            this.AddItemButton.Text = "Add Item";
            this.AddItemButton.UseVisualStyleBackColor = true;
            this.AddItemButton.Click += new System.EventHandler(this.AddItemButton_Click);
            // 
            // FileBrowserButton
            // 
            this.FileBrowserButton.Location = new System.Drawing.Point(205, 54);
            this.FileBrowserButton.Name = "FileBrowserButton";
            this.FileBrowserButton.Size = new System.Drawing.Size(75, 23);
            this.FileBrowserButton.TabIndex = 10;
            this.FileBrowserButton.Text = "Browse";
            this.FileBrowserButton.UseVisualStyleBackColor = true;
            this.FileBrowserButton.Click += new System.EventHandler(this.FileBrowserButton_Click);
            // 
            // chooseImageDialog
            // 
            this.chooseImageDialog.FileName = "openFileDialog1";
            this.chooseImageDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.chooseImageDialog_FileOk);
            // 
            // AddItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.FileBrowserButton);
            this.Controls.Add(this.AddItemButton);
            this.Controls.Add(this.newItemDescriptionTextBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.ImagePathLabel);
            this.Controls.Add(this.StartBidLabel);
            this.Controls.Add(this.newItemImagePathTextBox);
            this.Controls.Add(this.newItemStartBidTextBox);
            this.Controls.Add(this.NewitemIDTextBox);
            this.Controls.Add(this.ItemIDLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddItemForm";
            this.Text = "Add Item Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ItemIDLabel;
        private System.Windows.Forms.TextBox NewitemIDTextBox;
        private System.Windows.Forms.TextBox newItemStartBidTextBox;
        private System.Windows.Forms.TextBox newItemImagePathTextBox;
        private System.Windows.Forms.Label StartBidLabel;
        private System.Windows.Forms.Label ImagePathLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.RichTextBox newItemDescriptionTextBox;
        private System.Windows.Forms.Button AddItemButton;
        private System.Windows.Forms.Button FileBrowserButton;
        private System.Windows.Forms.OpenFileDialog chooseImageDialog;
    }
}