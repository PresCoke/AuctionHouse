using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuctionClient
{
    public partial class ClientView : Form
    {
        //client
        private AuctionClient.Controller.ClientController controller;

        //delegates
        public delegate void addAuctionItemDelegate(string ID, string ItemDescription, string HighBid, string closeTime, string imageURL, string status);
        public addAuctionItemDelegate addItemToAuction;

        public delegate void changeTextDelegate();
        public changeTextDelegate changeButtonText;

        public delegate void deleteAllAuctionItemsDelegate();
        public deleteAllAuctionItemsDelegate deleteAllItems;

        public ClientView()
        {
            addItemToAuction = new addAuctionItemDelegate(this.addAuctionItem);
            changeButtonText = new changeTextDelegate(this.changeTextOnButton);
            deleteAllItems = new deleteAllAuctionItemsDelegate(this.deleteAllAuctionItems);

            this.controller = new AuctionClient.Controller.ClientController();
            InitializeComponent();
            //join/exit button click event handled by client controller
            this.JoinExitButton.Click += new System.EventHandler(this.controller.JoinExitAuction);
        }

        public string getServerIP()
        {
            //returns contents of server Ip address text box
            return this.ServerIPAddressTextBox.Text;
        }
        public string getServerPortNumber()
        {
            //returns contents of port number text box
            return this.PortNumberTextBox.Text;
        }
        public void changeTextOnButton()
        {
            //depending on current value changes text to opposite
            if (this.JoinExitButton.Text == "Join Auction")
                this.JoinExitButton.Text = "Exit Auction";
            else if (this.JoinExitButton.Text == "Exit Auction")
                this.JoinExitButton.Text = "Join Auction";
        }

        public void addAuctionItem(string ID, string ItemDescription, string HighBid, string closeTime, string imageURL, string status)
        {
            try
            {
                //creates a new auction item control
                AuctionItemControl aic = new AuctionItemControl(ID, ItemDescription, HighBid, closeTime, imageURL, status, controller);
                //sets location to number of controls * the height of each individual control
                Point location = new Point(0, this.auctionItemPanel.Controls.Count * aic.Height);
                aic.Location = location;
                //adds control to panel
                this.auctionItemPanel.Controls.Add(aic);
                //re-paints form
                this.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception in ClientView.addAuctionItem by: " + e.TargetSite + " with: " + e.Message);
            }
        }

        public void deleteAllAuctionItems()
        {
            //deletes all controls in panel
            this.auctionItemPanel.Controls.Clear();
        }
    }
}
