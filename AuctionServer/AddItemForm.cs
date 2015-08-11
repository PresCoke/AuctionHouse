using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuctionServer
{
    public partial class AddItemForm : Form
    {
        Controller.ServerControl m_Server;
        public AddItemForm(Controller.ServerControl server)
        {
            //creates a new form
            this.InitializeComponent();

            //adds reference to controller creating form
            this.m_Server = server;
        }

        private void FileBrowserButton_Click(object sender, EventArgs e)
        {
            //opens a file chooser when "Browse" button clicked
            this.chooseImageDialog.ShowDialog();
        }

        private void chooseImageDialog_FileOk(object sender, CancelEventArgs e)
        {
            //when file chosen checks if file is .png and make image path text box = to ABSOLUTE path of file
            if (this.chooseImageDialog.FileName.EndsWith(".png"))
            {
                this.newItemImagePathTextBox.Text = chooseImageDialog.FileName;
            }
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            //tells server to add item with given fields and closes window
            m_Server.addItem(this.NewitemIDTextBox.Text, this.newItemDescriptionTextBox.Text, this.newItemStartBidTextBox.Text, this.newItemImagePathTextBox.Text);
            this.Close();
        }
    }
}
