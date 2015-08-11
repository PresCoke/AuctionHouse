using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuctionClient
{
    public partial class AuctionItemControl : UserControl
    {
        //item id and high bid
        private int m_ItemID;
        private float m_HighBid;
        //delegates for getting neccessary information
        public delegate int getItemIDDelegate();
        public getItemIDDelegate getItemID;
        public delegate float getUserBidDelegate();
        public getUserBidDelegate getUserBid;
        public delegate float getHighBidDelegate();
        public getHighBidDelegate getHighBid;
        public delegate DateTime getCloseTimeDelegate();
        public getCloseTimeDelegate getCloseTime;

        public AuctionItemControl(string ID, string ItemDescription, string HighBid, string closeTime, string imageURL, string status, Controller.ClientController control)
        {
            try
            {
                InitializeComponent();

                getItemID = new getItemIDDelegate(this.getID);
                getUserBid = new getUserBidDelegate(this.getUBid);
                getHighBid = new getHighBidDelegate(this.getHBid);
                getCloseTime = new getCloseTimeDelegate(this.getCT);

                //explicitly add form as parent of button (for easy way of getting auction item)
                this.BidButton.Parent = this;

                this.m_ItemID = Int32.Parse(ID);
                this.m_HighBid = float.Parse(HighBid);
                this.ItemDescriptionTextBox.Text = ItemDescription;
                this.ItemStatusTextBox.Text = status;
                this.TimeLeftTextBox.Text = closeTime;
                //display like money value
                this.HighestBidTextBox.Text = m_HighBid.ToString("C");
                //add image to picture box
                this.makeImage(imageURL);

                //on bid button click let creating controller handle the event
                this.BidButton.Click += new EventHandler(control.makeBid);

                this.Visible = true;
            }
            catch (FormatException fe)
            {
                MessageBox.Show("FormatException in AuctionItemControl constructor by: " + fe.TargetSite + " with: " + fe.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception in AuctionItemControl constructor by: " + e.TargetSite + " with: " + e.Message);
            }
        }

        private void makeImage(string path)
        {
            //create image from URL and set it to background of picture box
            try
            {
                this.ItemPicture.BackgroundImage = Image.FromFile(path);
            }
            catch (Exception e)
            {
                //if any exception occurs set picture box image to error image
                this.ItemPicture.Image = this.ItemPicture.ErrorImage;
                System.Windows.Forms.MessageBox.Show("Exception in ClientController.makeImage(string) by: " + e.TargetSite + "\rwith: " + e.Message);
            }
        }

        public int getID()
        {
            //get items id
            return this.m_ItemID;
        }

        public float getUBid()
        {
            //get users bid
            try
            {
                string temp = this.CurrentBidTextBox.Text;
                if (temp != "")
                {
                    //if text starts with $ remove it
                    if (temp[0] == '$')
                    {
                        char[] work = new char[temp.Length - 1];
                        temp.CopyTo(1, work, 0, work.Length);
                        temp = "";
                        for (int i = 0; i < work.Length; i++)
                        {
                            temp += work[i];
                        }
                    }
                    //return value
                    return (float.Parse(temp));
                }
                else
                    return -1;
            }
            catch (FormatException fe)
            {
                //if parse unsuccessful
                return -1;
            }
        }

        public float getHBid()
        {
            //get current high bid
            return this.m_HighBid;
        }
        public DateTime getCT()
        {
            //return closing time
            try
            {
                return (DateTime.Parse(this.TimeLeftTextBox.Text));
            }
            catch (FormatException fe)
            {
                //if exception return current time (should force client to be unable to bid)
                return DateTime.Now;
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }
    }
}
