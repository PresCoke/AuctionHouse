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
    public partial class ServerView : Form
    {
        //server
        Controller.ServerControl server;
        //delegate for writing to server status textbox
        public delegate void changeServerStatus(String msg);
        public changeServerStatus writeToStatusTextBox;

        public ServerView()
        {
            writeToStatusTextBox = new changeServerStatus(writeToServerBox);

            //create new server
            server = new AuctionServer.Controller.ServerControl();
            InitializeComponent();

            //add controller methods to handle certain events
            this.ListenButton.Click += new System.EventHandler(server.listenOnPortButtonClick);
            this.AddAuctionItemButton.Click += new System.EventHandler(server.addAuctionItem);
            this.timer1.Tick += new EventHandler(this.server.checkItemTimes);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(server.closeServer);
        }
        public void writeToServerBox(string message)
        {
            this.ServerStatusTextBox.Text += message+"\n";
        }

        public string getPortNumber()
        {
            return this.PortTextBox.Text;
        }

        public void changeIPAddress(string address)
        {
            this.serverIPAddressTextBox.Text = address;
        }
    }
}
