using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace AuctionServer.Model
{
    class AuctionItem
    {
        private bool m_Closed;
        private int m_ItemID;
        private string m_ItemDescription;
        private string m_ItemPath;
        private Bid m_CurrentHighestBid;
        private DateTime m_ClosingTime;

        public AuctionItem(int ID, string itemDescription, DateTime closeAt, float startAmount, string imagePath)
        {
            //creates an AuctionItem object with id, description, closing time, starting amount and path to items image
            try
            {
                this.m_ItemID = ID;
                this.m_ItemDescription = itemDescription;
                this.m_ClosingTime = closeAt;
                this.m_ItemPath = imagePath;
                this.m_CurrentHighestBid = new Bid(this, startAmount); // creates a starting bid to beat (with no client)
                this.m_Closed = false;
            }
            catch (ArgumentNullException ane)
            {
                System.Windows.Forms.MessageBox.Show("ArgumentNullException in AuctionItem constructor by: " + ane.TargetSite + "\rwith: " + ane.Message);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Exception in AuctionItem constructor by: " + e.TargetSite + "\rwith: " + e.Message);
            }
        }

        public int getItemID()
        {
            //gets the auction items ID
            return this.m_ItemID;
        }

        public string getItemDescription()
        {
            //gets the auction items description
            return this.m_ItemDescription;
        }

        public DateTime getClosingTime()
        {
            //gets the auction items closing time
            return this.m_ClosingTime;
        }

        public Bid getCurrentHighBid()
        {
            //gets the current high bid
            return this.m_CurrentHighestBid;
        }

        public bool setCurrentHighBid(Bid newHighBid)
        {
            //sets the current high bid (checks if bid is actually higher than current bid and that auction is not closed)
            //and returns un/success of operation
            if (newHighBid.getAmount() <= this.m_CurrentHighestBid.getAmount() || this.m_ClosingTime < DateTime.Now)
                return false;
            else
            {
                this.m_CurrentHighestBid = newHighBid;
                return true;
            }
        }

        public override string ToString()
        {
            //writes all relevant information to easy to read format.
            return ("Item: " + m_ItemID.ToString()
                + "\rDescription: " + m_ItemDescription
                + "\rCurrent High Bid: " + m_CurrentHighestBid.ToString()
                + "\rCloses At: " + m_ClosingTime.ToString()
                + "\rImage Path: " + m_ItemPath);
        }

        public string makeXMLNode()
        {
            //makes an XML node for the item containg id, description, imagepath, closetime and current highbid AMMOUNT
            return ("<id>"+this.m_ItemID.ToString()+"</id>"+
                "<description>"+this.m_ItemDescription+"</description>"+
                "<imagepath>"+this.m_ItemPath+"</imagepath>"+
                "<closetime>"+this.m_ClosingTime.ToString()+"</closetime>"+
                "<bid>"+this.m_CurrentHighestBid.getAmount().ToString()+"</bid>");

        }

        public bool closed()
        {
            //checks if the auction on this item is closed
            return this.m_Closed;
        }
        public void closeAuction()
        {
            //closes the auction on the item
            this.m_Closed = true;
        }
    }
}
