using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AuctionServer.Model
{
    class AuctionItemList
    {
        //list of items currently in auction
        private LinkedList<AuctionItem> listOItems;
        //relative path to auction file
        private static string URLString = "../../../Data/Auction.xml";

        public AuctionItemList()
        {
            //creates and initializes auction items
            //read xml file and get auction item information
            XmlDocument doc = new XmlDocument();
            doc.Load(URLString);
            XmlElement root = doc.DocumentElement;
            XmlNodeList setOnodes = root.SelectNodes("item");

            int id = 0;
            string description = "";
            string imagePath = "";
            float startBid = 0;
            DateTime closeTime;
            Random r = new Random();

            this.listOItems = new LinkedList<AuctionItem>();

            foreach (XmlNode node in setOnodes)
            {
                try
                {
                    id = Int32.Parse(node["id"].InnerText);

                    description = node["description"].InnerText;

                    imagePath = node["imagePath"].InnerText;

                    startBid = float.Parse(node["startBid"].InnerText);

                    //generates random closing time
                    double minuteAddition = (double)(r.NextDouble() + r.Next(2, 6));
                    minuteAddition = .5;
                    closeTime = DateTime.Now.AddMinutes(minuteAddition);

                    AuctionItem newItem = new AuctionItem(id, description, closeTime, startBid, imagePath);
                    this.listOItems.AddLast(newItem);
                }
                catch (FormatException fe)
                {
                    //if either id or startBid to not parse tell server
                    System.Windows.Forms.MessageBox.Show("FormatException in AuctionItemList constructor by: " + fe.TargetSite + "\rwith: " + fe.Message);
                }
                catch (ArgumentNullException ane)
                {
                    System.Windows.Forms.MessageBox.Show("ArgumentNullException in AuctionItemList constructor by: " + ane.TargetSite + "\rwith: " + ane.Message);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Exception in AuctionItemList constructor by: " + e.TargetSite + "\rwith: " + e.Message);
                }
            }
        }

        public override string ToString()
        {
            //concatenates a string of all auction items
            string outString = "";
            int i = listOItems.Count;
            LinkedListNode<AuctionItem> x = listOItems.First;
            while (i>0)
            {
                outString += x.Value.ToString() + "\n\n";
                x = x.Next;
                i--;
            }
            return outString;
        }

        public int getNumberOfItems()
        {
            //returns number of items currently in auction
            return (this.listOItems.Count);
        }

        public LinkedListNode<AuctionItem> getItemFirstNode()
        {
            //returns first node of auction item list
            return this.listOItems.First;
        }

        public AuctionItem getItemByID(int id)
        {
            //gets an item by it's ID
            LinkedListNode<AuctionItem> node = listOItems.First;
            int i = listOItems.Count;
            while (i > 0)
            {
                if (node.Value.getItemID() == id)
                {
                    return (node.Value);
                }
                else
                {
                    node = node.Next;
                    i--;
                }
            }
            return (null);
        }

        public AuctionItem getItemByPosition(int position) //not used
        {
            //gets an Item by it's position
            if (position < listOItems.Count)
            {
                LinkedListNode<AuctionItem> node = listOItems.First;
                int i = position;
                while (i > 0)
                {
                    node = node.Next;
                    i--;
                }
                return (node.Value);
            }
            else
            {
                throw (new IndexOutOfRangeException("Position supplied greater then number of auction items."));
            }
        }
        public bool checkCloseTimes()
        {
            //checks closing times of all auction items to see if they have passed
            //only gets current time once to ensure that all auction items compared against same standard
            DateTime temp = DateTime.Now;
            LinkedListNode<AuctionItem> node = listOItems.First;
            int i = listOItems.Count;
            bool time;
            while (i > 0)
            {
                time = node.Value.getClosingTime().CompareTo(temp) < 0;
                if (time && !node.Value.closed())
                {
                    node.Value.closeAuction();
                    return true;
                }
                node = node.Next;
                i--;
            }
            return false;
        }

        public bool addAuctionItemToList(int id, string description, float startingBid, string imagePath)
        {
            try
            {
                //attempts to add auction item to list
                //NOTE DOES NOT PERSIST ITEM
                //first checks if item id is already used
                LinkedListNode<AuctionItem> node = listOItems.First;
                int i = listOItems.Count;
                while (i > 0)
                {
                    if (node.Value.getItemID() == id)
                    {
                        //if item id found return false
                        return false;
                    }
                    else
                    {
                        node = node.Next;
                        i--;
                    }
                }

                Random r = new Random();
                //generate random closing time
                double minuteAddition = (double)(r.NextDouble() + r.Next(2, 6));
                DateTime closeTime = DateTime.Now.AddMinutes(minuteAddition);
                //create item add it to list and return true
                AuctionItem newItem = new AuctionItem(id, description, closeTime, startingBid, imagePath);
                this.listOItems.AddFirst(newItem);
                return true;
            }
            catch (Exception e)
            {
                //if any exception occcurs return false
                return false;
            }
        }
    }
}
