using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace AuctionServer.Controller
{
    public class ServerControl
    {
        //for creating random numbers
        private static Random r;

        public static int NameOfServer;//not used

        //a delegate for removing a client from the server
        public delegate void removeClientFromServer(ClientControl clnt);
        public removeClientFromServer removeClientThread;

        //a constant describing the maximum number of allowable clients
        private const int MAXCLIENTS = 5;

        //a linked list of RTSPClient's i.e. client threads
        private static LinkedList<ClientControl> clientList = new LinkedList<ClientControl>();
        //a list of items currently in the auction
        private Model.AuctionItemList listOItems;

        //the socket the server is listening for incoming connections from.
        Socket tcpServer;
        //the thread waiting for client connections
        Thread listeningOnPort;
        //boolean describing whether currently listening for connections
        private bool listening;

        private ASCIIEncoding encode;
        //reference to view
        private ServerView view;

        public ServerControl()
        {
            //creates and initializes basic server object
            listening = false;
            r = new Random();
            //name server
            NameOfServer = r.Next(10000);
            removeClientThread = new removeClientFromServer(removeClient);
            //initializes auction item list (from XML file)
            listOItems = new AuctionServer.Model.AuctionItemList();
            encode = new ASCIIEncoding();
        }

        public void listenOnPortButtonClick(object sender, EventArgs e)
        {
            /*Pre: User supplies 16 bit inetger port number to listen on.*/
            /*Post:A new thread is created to constantly listen for new connections.
             *returns a string to inform the user how successful the method was.*/

            //get reference to view
            view = (ServerView)(((Button)sender).FindForm());

            //find internal IP address
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }

            //display IP address
            view.changeIPAddress(localIP);
            //get port number
            string portNumberTemp = view.getPortNumber();
            Int16 portNumber = 0;

            if (portNumberTemp != "" && listening == false)
            {
                try
                {
                    //parse text in port number txtbox to 16 bit integer
                    portNumber = (short) UInt16.Parse(portNumberTemp);
                }
                catch (FormatException fe)
                {
                    //if text was not 16 bit integer write to main view
                    view.writeToStatusTextBox("Format exception in MainView by: "+fe.Source+"\rwith: " + fe.ToString());
                    return;
                }
            }
            else if (listening)
            {
                view.writeToStatusTextBox("Already Listening.");
                return;
            }
            //if nothing in port number textbox display message box 
            else
            {
                MessageBox.Show("Please enter a port number");
                return;
            }


            try
            {
                //create new endpoint listening for any IPAdress on portNumber
                IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, portNumber);
                //create a new TCP socket of streamtype(?)
                tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //bind the socket to the endpoint
                tcpServer.Bind(listenEndPoint);
                try
                {
                    //create delegate for thread method
                    ThreadStart acceptConnectionMethod = new ThreadStart(this.acceptConnection);
                    //create new thread that runs acceptConnection
                    listeningOnPort = new Thread(acceptConnectionMethod);
                    listeningOnPort.IsBackground = true;
                    //start the thread
                    listeningOnPort.Start();
                }
                catch (ThreadStartException tse)
                {
                    //if thread could not be started
                    view.writeToStatusTextBox("Main listen Thread Start failed in StreamingServer by: " + tse.Source + "\rwith: " + tse.Message);
                }
                catch (Exception te)
                {
                    //if some other exception occured
                    view.writeToStatusTextBox("Main listen Thread failed in StreamingServer by: " + te.Source + "\rwith: " + te.Message);
                }
                view.writeToStatusTextBox("Listening on Port:" + portNumber + " with socket type: " + tcpServer.SocketType);
                view.writeToStatusTextBox("Selling:\n" + listOItems.ToString());
            }
            catch (SocketException se)
            {
                //if socket could not be created
                view.writeToStatusTextBox("Main Socket failed in StreamingServer by: " + se.Source + "\rwith: " + se.Message);
            }
        }

        private void acceptConnection()
        {
            /*Pre: the listenOnPort method created a thread.*/
            /*Post:the method will continually look for new connections*/
            listening = true;
            ClientControl.addReferenceToMainController(this);
            ClientControl.addReferenceToView(this.view);
            while (listening == true)
            {
                //blocking - the socket starts listening
                tcpServer.Listen(int.MaxValue);
                try
                {
                    //wait for connection attempted, store socket info inside tryToConnect
                    Socket tryToConnect = tcpServer.Accept();
                    //if number of clients does not exceed MAXCLIENTS
                    int numClients = clientList.Count;
                    if (numClients < MAXCLIENTS && tryToConnect != null)
                    {
                        //create new client
                        ClientControl client = new ClientControl(tryToConnect, (r.Next(10000) + r.Next(10)));
                        //and add client object to linked list
                        clientList.AddLast(client);
                        //tell main view that client was added
                        view.Invoke(view.writeToStatusTextBox, "Client: "+ client.clientThread.Name +" added.");
                    }
                    else
                    {
                        //otherwise tell main view client was not added
                        view.Invoke(view.writeToStatusTextBox, "Client could not be added.");
                        //create new network stream
                        NetworkStream ntwkstrm = new NetworkStream(tryToConnect);
                        ASCIIEncoding encode = new ASCIIEncoding();
                        byte[] sendBytes = new byte[4];
                        //read anything already on stream
                        ntwkstrm.Read(sendBytes, 0, 4);
                        //send last client BUSY signal
                        sendBytes = encode.GetBytes("BUSY");
                        ntwkstrm.Write(sendBytes, 0, sendBytes.Length);
                        //close network stream
                        ntwkstrm.Flush();
                        ntwkstrm.Close();
                    }
                }
                catch (SocketException se)
                {
                    //if socket fails close it
                    if (tcpServer != null)
                        tcpServer.Close();
                    //and abort thread
                    if (listeningOnPort != null)
                        listeningOnPort.Abort();
                }
                catch (ThreadAbortException tae)
                {
                    //if thread is attempted to abort check if socket is closed if not close it.
                    if (tcpServer != null)
                        tcpServer.Close();
                    //then remove all clients
                    for (int i = clientList.Count; i>0; i--)
                        clientList.RemoveFirst();
                }
                catch (Exception e)
                {
                }
            }
        }

        public void removeClient(ClientControl removeThisClient)
        {
            /*Pre: a client object to be removed is supplied*/
            /*Post:the client object has been removed from the linked list making room for other clients*/
            try
            {
                //if client object is in the list
                if (clientList.Contains(removeThisClient))
                {
                    //remove the object from the list
                    clientList.Remove(removeThisClient);
                    view.Invoke(view.writeToStatusTextBox, "Client: "+ removeThisClient.clientThread.Name +" removed.");
                }
            }
            catch (ArgumentException ae)
            {
                //if a bad argument was supplied tell the main view what caused it
                if (view != null)
                    view.Invoke(view.writeToStatusTextBox, "Argument exception in RemoveClient StreamingServer: " + ae.Message);
            }
        }
        public void CloseSocket()//not used
        {
            listening = false;
        }

        public void closeServer(object sender, EventArgs evt)
        {
            //closes and cleans up current server
            try
            {
                //tell currently connected clients server is closing
                LinkedListNode<ClientControl> node = clientList.First;

                for (int i = 0; i < clientList.Count; i++)
                {
                    node.Value.sendMessageToClient(encode.GetBytes("GBYE"));
                    node = node.Next;
                }

            }
            catch (Exception e)
            {
                view.Invoke(view.writeToStatusTextBox, "Exception in ServerControl.sendAuctionItems() with: " + e.Message);
            }

            //close socket
            if (tcpServer != null)
                tcpServer.Close();
            //abort thread
            if (listeningOnPort != null)
                listeningOnPort.Abort();
        }

        public byte[] getAuctionItemsAsBytes(ClientControl client)
        {
            /*Returns auction item at position i as bytes where every 
             *item field is seperated by a two byte int16 containing
             *the length of the next field.
             */
            try
            {
                //creates an XML file containg all auction items to be sent to specific client
                LinkedListNode<Model.AuctionItem> temp = this.listOItems.getItemFirstNode();
                Model.AuctionItem work;
                //add root node tag
                string outString = "<Auction>";

                for (int i = 0; i < this.listOItems.getNumberOfItems(); i++)
                {
                    work = temp.Value;
                    //add item node tag
                    outString += "<item>";
                    //get AuctionItem XML string
                    outString += work.makeXMLNode();
                    //open status tag
                    outString += "<status>";
                    //determine status of auction item for specific client
                    if (work.getCurrentHighBid().getClient().ToString() == "-1")
                    {
                        if (work.getClosingTime() < DateTime.Now)
                        {
                            outString += "This auction is over. No one won this item.";
                        }
                        else
                        {
                            outString += "This auction is not over. No one has bid yet.";
                        }
                    }
                    else if (work.getCurrentHighBid().getClient().ToString() == client.clientThread.Name)
                    {
                        if (work.getClosingTime() < DateTime.Now)
                        {
                            outString += "This auction is over. You won this item.";
                        }
                        else
                        {
                            outString += "This auction is not over. You are the highest bidder.";
                        }
                    }
                    else
                    {
                        if (work.getClosingTime() < DateTime.Now)
                        {
                            outString += "This auction is over. You lost this item.";
                        }
                        else
                        {
                            outString += "This auction is not over. You are not the highest bidder.";
                        }
                    }
                    //close status tag and item node
                    outString += "</status>";
                    outString += "</item>";

                    //next auction item
                    temp = temp.Next;
                }
                //close Auction root node
                outString += "</Auction>";

                //view.Invoke(view.writeToStatusTextBox, "Sending: "+outString);
                //return byte[] of XML string
                byte[] packet = encode.GetBytes(outString);
                return packet;
            }
            catch (NullReferenceException nre)
            {
                //if null reference exception occured return byte[] with error string.
                string error = nre.ToString();
                byte[] errorPacket = new byte[error.Length];
                encode.GetBytes(error.ToCharArray(), 0, error.Length, errorPacket, 0);
                return errorPacket;
            }
            catch (Exception e)
            {
                //if exception occured return byte[] with error string
                string error = e.ToString();
                byte[] errorPacket = new byte[error.Length];
                encode.GetBytes(error.ToCharArray(), 0, error.Length, errorPacket, 0);
                return errorPacket;
            }
        }

        public int getNumberOfAuctionItems()
        {
            //gets number of items currently in auction
            return (listOItems.getNumberOfItems());
        }

        public void makeBid(string xml, ClientControl cle)
        {
            try
            {
                //client attempts to bid on item
                XmlDocument doc = new XmlDocument();
                StringReader red = new StringReader(xml);
                doc.Load(red);

                XmlElement root = doc.DocumentElement;
                //read item id and bid ammount from xml string
                string id = root.SelectSingleNode("itemID").InnerText;
                string bid = root.SelectSingleNode("amount").InnerText;

                //attempt to get auction item from id
                Model.AuctionItem temp = this.listOItems.getItemByID(Int32.Parse(id));

                //creates new bid
                Model.Bid newBid = new AuctionServer.Model.Bid(Int32.Parse(cle.clientThread.Name), temp, float.Parse(bid));

                //attempt to bid on item
                if (temp.setCurrentHighBid(newBid))
                    view.Invoke(view.writeToStatusTextBox, newBid.ToString());
                else
                    view.Invoke(view.writeToStatusTextBox, "Client: " + cle.clientThread.Name + " was UNSUCCESSFUL.");
            }
            catch (FormatException fe)
            {
                view.Invoke(view.writeToStatusTextBox, "FormatException in ServerControl.makeBid(string, ClientControl) with: " +fe.Message);
            }
            catch (/*SomeKindOf*/Exception e)
            {
                view.Invoke(view.writeToStatusTextBox, "Exception in ServerControl.makeBid(string, ClientControl) with: " + e.Message);
            }
        }
        public void sendAuctionItems()
        {
            try
            {
                //send auction items to ALL clients
                LinkedListNode<ClientControl> node = clientList.First;

                for (int i = 0; i < clientList.Count; i++)
                {
                    //sends XML file (as bytes) to each client
                    node.Value.sendMessageToClient(this.getAuctionItemsAsBytes(node.Value));
                    node = node.Next;
                }

            }
            catch (Exception e)
            {
                view.Invoke(view.writeToStatusTextBox, "Exception in ServerControl.sendAuctionItems() with: " + e.Message);
            }
        }
        public void checkItemTimes(object sender, EventArgs evt)
        {
            //every time this even fires checks if any auction is over
            if (this.listOItems.checkCloseTimes())
                this.sendAuctionItems();
        }

        public void addAuctionItem(object sender, EventArgs evt)
        {
            //creats a new "AddItemForm" and displays it.
            AddItemForm newItemForm = new AddItemForm(this);
            newItemForm.Show();
        }

        public void addItem(string id, string description, string startBid, string imagePath)
        {
            //called from "AddItemForm" attempts to create a new Auction Item and add it to the list if successful
            try
            {
                //get int ID
                int ID = Int32.Parse(id);
                //if startbid begins with "$" remove it.
                if (startBid[0] == '$')
                {
                    char[] work = new char[startBid.Length - 1];
                    startBid.CopyTo(1, work, 0, work.Length);
                    startBid = "";
                    for (int i = 0; i < work.Length; i++)
                    {
                        startBid += work[i];
                    }
                }
                //get starting bid
                float startingBid = float.Parse(startBid);
                //attempts to add item to auction
                if (this.listOItems.addAuctionItemToList(ID, description, startingBid, imagePath))
                {
                    MessageBox.Show("Item added.");
                    this.sendAuctionItems();
                }
                else MessageBox.Show("Unable to add item.");
            }
            catch (FormatException fe)
            {
                //if either id or starting bid did not parse tell server
                MessageBox.Show(fe.Message + " Item not added.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " Item not added.");
            }
        }
    }
}
