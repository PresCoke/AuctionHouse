using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Threading;

namespace AuctionClient.Controller
{
    public class ClientController
    {
        //reference to view
        ClientView view;
        //delegate for updating auction items
        public delegate bool updateAuctionStatus(String xml);
        public updateAuctionStatus updateAuction;
        //delegate for recieving messages from server
        public delegate string receive();
        public receive receiveMessage;

        //streams out and in
        private NetworkStream sendStream, listenStream;
        private Thread listening;
        //to encode/decode messages
        private ASCIIEncoding encode;

        //socket for connection to server
        private Socket listeningSocket;
        //sent/recieved message buffers
        private byte[] inMessage;
        private byte[] outMessage;

        bool connected;
        IPAddress serverIP;
        Int16 portNumber;
        
        public ClientController()
        {
            //create and intialize client
            encode = new ASCIIEncoding();
            inMessage = new byte[1024*1024];
            outMessage = new byte[1024*1024];
            connected = false;

            updateAuction = new updateAuctionStatus(this.updateAuctionItems);
            receiveMessage = new receive(this.recieveMessageFromServer);
        }

        public void JoinExitAuction(object sender, EventArgs evt)
        {
            //when client attempts to dis/connect to server
            view = (ClientView)(((Button)sender).FindForm());
            if (!connected)
            {
                //if not already connected attempt connection
                //get supplied IP Address and Port number from view
                string tempServerIP = view.getServerIP(), tempPort = view.getServerPortNumber();
                if (tempServerIP != "" && tempPort != "")
                {
                    try
                    {
                        //if not null attempt to parse
                        this.serverIP = IPAddress.Parse(tempServerIP);
                        this.portNumber = Int16.Parse(tempPort);
                        this.connectToServer();
                    }
                    catch (FormatException fe)
                    {
                        //if parse fails tell user
                        MessageBox.Show("Please enter correct text for all fields.");
                        return;
                    }
                }
                else
                {
                    //tell user to add data to all fiels
                    MessageBox.Show("Please enter text for all fields.");
                    return;
                }
            }
            else if (connected)
            {
                //or if already connected discconect from server
                this.disconnectFromServer();
            }
        }
        private void connectToServer()
        {
            /*Pre: attempts to connect to server with ipaddress and port number specified by user*/
            /*Post:client connects to server or error message written to view*/

            try
            {
                //remote end point
                IPEndPoint serverEndPoint = new IPEndPoint(this.serverIP, this.portNumber);
                //new tcp socket of stream type
                this.listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //attempt to connect to server
                this.listeningSocket.Connect(serverEndPoint);
                this.sendStream = new NetworkStream(this.listeningSocket);
                this.listenStream = new NetworkStream(this.listeningSocket);
                //send greeting
                string message = "HELO";
                this.sendMessageToServer(message);
                //recieve message from server
                message = recieveMessageFromServer();
                
                /*Parse Message and DO SOMETHING*/
                //if server response is busy end
                if (message == "BUSY")
                {
                    return;
                }
                if (!this.connected)
                {
                    //if not already connected set connected and change text on connect button
                    connected = true;
                    view.Invoke(view.changeButtonText);
                }
                //start thread for listening to server updates
                ThreadStart runListener = new ThreadStart(this.listenForUpdates);
                this.listening = new Thread(runListener);
                this.listening.Name = "Listener";
                this.listening.IsBackground = true;
                this.listening.Start();

            }
                //if exceptions occur dissconnect from server
            catch (SocketException se)
            {
                this.disconnectFromServer();
                //tell user why socket failed
                MessageBox.Show("Socket exception in connectToServer: " + se.ToString());
            }
            catch (Exception e)
            {
                this.disconnectFromServer();
                //any other exception tell user why it failed
                MessageBox.Show("Exception in connectToServer: " + e.ToString());
            }
        }

        private void disconnectFromServer()
        {
            //if connected to server
            if (this.connected)
            {
                //tell server client exiting
                this.sendMessageToServer("EXIT");
                //change text on dis/connect button
                view.Invoke(view.changeButtonText);
                this.connected = false;
            }
            //delete all auction items in view
            view.Invoke(view.deleteAllItems);
            
            //if socket/streams/threads still open/running close and destroy
            if (this.listeningSocket != null)
                this.listeningSocket.Close();
            if (this.sendStream != null)
            {
                this.sendStream.Close();
                this.sendStream.Dispose();
            }
            if (this.listenStream != null)
            {
                this.listenStream.Close();
                this.listenStream.Dispose();
            }
            if (this.listening != null)
            {
                this.listening.Abort();
            }
        }

        private void sendMessageToServer(string msg)
        {
            /*Pre: message that needs to be sent to server supplied in msg parameter*/
            /*Post:message sent or error message written to view*/

            try
            {
                if (sendStream.CanWrite)
                {
                    //encode string into byte array and store in outMessage
                    outMessage = encode.GetBytes(msg);
                    //write sent string to view
                    //MessageBox.Show("Sending server: " + msg);
                    //send the message
                    sendStream.Write(outMessage, 0, msg.Length);
                    sendStream.Flush();
                }
            }
            catch (IOException ioe)
            {
                //close the socket (if it hasn't been already)
                if (this.listeningSocket != null)
                    this.listeningSocket.Close();
                //close network stream if not closed already
                if (sendStream != null)
                    sendStream.Close();
                this.disconnectFromServer();
                MessageBox.Show("IO exception in sendMessageToServer: " + ioe.ToString());
            }
            catch (FormatException fe)
            {
                //if something wasn't formatted correctly tell the view what
                MessageBox.Show("Format exception in sendMessageToServer: " + fe.ToString());
            }
            catch (SocketException se)
            {
                //if socket fails tell view why
                MessageBox.Show("Socket exception in sendMessageToServer: " + se.ToString());
                //close the socket (if it hasn't been already)
                if (this.listeningSocket != null)
                    this.listeningSocket.Close();
                //close network stream if not closed already
                if (sendStream != null)
                    sendStream.Close();
                this.disconnectFromServer();
            }
            catch (Exception e)
            {
                //any other exception - tell the view
                MessageBox.Show("Exception in sendMessageToServer: " + e.ToString());
            }
        }

        private string recieveMessageFromServer()
        {
            /*Pre : client sent message to server and expects ACK
             *Post: ACK recieved or cannot read from server*/
            try
            {
                if (listenStream.CanRead)
                {
                    int size = listenStream.Read(inMessage, 0, inMessage.Length);
                    //if XML file parsed correctly
                    if (this.updateAuctionItems(encode.GetString(inMessage, 0, size)))
                        return ("Updated Auction Items.");
                    else return ("Error.");
                }
                else
                {
                    return ("Cannot read from server.");
                }
            }
            catch (IOException ioe)
            {
                //close network stream if not closed already
                if (listenStream != null)
                {
                    listenStream.Close();
                    listenStream.Dispose();
                    listenStream = new NetworkStream(this.listeningSocket);
                }
                return ("IOException in recieveMessageFromServer: " + ioe.ToString());
            }
            catch (FormatException fe)
            {
                //if something wasn't formatted correctly tell the view what
                //this.disconnectFromServer();
                return ("FormatException in recieveMessageFromServer: " + fe.ToString());
            }
            catch (SocketException se)
            {
                this.disconnectFromServer();
                return ("SocketException in recieveMessageFromServer: " + se.ToString());
            }
            catch (Exception e)
            {
                //any other exception - tell the view
                this.disconnectFromServer();
                return ("Exception in recieveMessageFromServer: "+e.ToString());
            }
        }

        public void makeBid(object sender, EventArgs evt)
        {
            //when auction item bid button clicked
            //figure out which auction item
            AuctionItemControl aicTemp = (AuctionItemControl) (( (Button) sender).Parent);
            //get users entered bid
            float userBid = (float) aicTemp.Invoke(aicTemp.getUserBid);
            //get highest bid
            float bidToBeat = (float) aicTemp.Invoke(aicTemp.getHighBid);
            //get closing time
            DateTime close = (DateTime) aicTemp.Invoke(aicTemp.getCloseTime);
            //has close time passed?
            bool time = close.CompareTo(DateTime.Now) > 0;
            //if both bids not -1 and close time has not passed
            if (bidToBeat != -1 && userBid != -1 && time)
            {
                //and if users bid is greater than current bid
                if (userBid > bidToBeat)
                {
                    //get item id and create XML file and send it to server
                    int id = (int) aicTemp.Invoke(aicTemp.getItemID);
                    string outString = "BID:<bid><itemID>" + id.ToString() + "</itemID><amount>" + userBid.ToString() + "</amount></bid>";
                    this.sendMessageToServer(outString);

                    MessageBox.Show("Bid: " + userBid.ToString() + " on item: " + id.ToString());
                }
                else
                {
                    MessageBox.Show("Bid: " + userBid.ToString() + " not high enough to beat: " +bidToBeat+".");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid bid.");
            }
        }

        private void listenForUpdates()
        {
            try
            {
                //listen for updates to auction items and server status from server
                string inMsg = "";
                while (true)
                {
                    this.receiveMessage();
                }
            }
            catch (ThreadAbortException tae)
            {
                MessageBox.Show(tae.StackTrace);
                this.disconnectFromServer();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
                this.disconnectFromServer();
            }
        }

        private bool updateAuctionItems(string XML)
        {
            //delete all items currently in view
            view.Invoke(view.deleteAllItems);
            //if string is not error string
            if (XML != "EROR")
            {
                //if server is closing
                if (XML == "GBYE")
                {
                    this.disconnectFromServer();
                    return false;
                }
                //otherwise parse XML document
                XmlDocument doc = new XmlDocument();
                StringReader red = new StringReader(XML);
                doc.Load(red);

                XmlElement root = doc.DocumentElement;
                XmlNodeList setOnodes = root.SelectNodes("item");

                string id = "";
                string description = "";
                string imagePath = "";
                string bid = "";
                string closeTime = "";
                string status = "";

                foreach (XmlNode node in setOnodes)
                {
                    try
                    {
                        id = node["id"].InnerText;
                        description = node["description"].InnerText;
                        imagePath = node["imagepath"].InnerText;
                        bid = node["bid"].InnerText;
                        closeTime = node["closetime"].InnerText;
                        status = node["status"].InnerText;
                        /*MessageBox.Show("Adding:" +
                            "\rID: " + id +
                            "\rDescription: " + description);*/
                        //add auction items to view
                        view.Invoke(view.addItemToAuction, id, description, bid, closeTime, imagePath, status);
                        //view.addAuctionItem(id, description, bid, closeTime, imagePath, status);
                    }
                    catch (XmlException xe)
                    {
                        this.listenStream.Flush();
                    }
                    catch (NullReferenceException nre)
                    {
                        System.Windows.Forms.MessageBox.Show("NullReferenceException in ClientController.updateAuctionItems(string) by: " + nre.TargetSite + "\rwith: " + nre.Message);
                        return false;
                    }
                    catch (FormatException fe)
                    {
                        System.Windows.Forms.MessageBox.Show("FormatException in ClientController.updateAuctionItems(string) by: " + fe.TargetSite + "\rwith: " + fe.Message);
                        return false;
                    }
                    catch (ArgumentNullException ane)
                    {
                        System.Windows.Forms.MessageBox.Show("ArgumentNullException in ClientController.updateAuctionItems(string) by: " + ane.TargetSite + "\rwith: " + ane.Message);
                        return false;
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Exception in ClientController.updateAuctionItems(string) by: " + e.TargetSite + "\rwith: " + e.Message);
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }
    }
}
