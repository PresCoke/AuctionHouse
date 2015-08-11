using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionServer.Model
{
    class Bid
    {
        private int m_ClientID;
        private AuctionItem m_Item;
        private float m_Amount;

        public Bid(AuctionItem onItem, float forAmount)
        {
            //creates a bid object without client id stores item bid reflects and ammount (typically starting amount)
            m_ClientID = -1;
            m_Item = onItem;
            m_Amount = forAmount;
        }

        public Bid(int clientMadeBid, AuctionItem onItem, float forAmount)
        {
            //creates a new bidding object that a client made on an item (onItem) with an ammount (forAmount)
            m_ClientID = clientMadeBid;
            m_Item = onItem;
            m_Amount = forAmount;
        }

        public int getClient()
        {
            //gets the client this bid belongs to
            return this.m_ClientID;
        }

        public AuctionItem getItem()
        {
            //gets the item this bid was made on
            return this.m_Item;
        }

        public float getAmount()
        {
            //gets the ammount this bid is worth
            return this.m_Amount;
        }

        public override string ToString()
        {
            //writes all relevant information to string
            if (this.m_ClientID == -1)
            {
                return ("No bids have been made. Current value to beat: " + this.m_Amount.ToString());
            }
            else
            {
                return ("Client: "+this.m_ClientID.ToString()+" has highest bid with: "+this.m_Amount.ToString());
            }
        }
    }
}
