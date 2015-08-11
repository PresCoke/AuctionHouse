using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace AuctionServer.Controller
{
    public class ClientControl
    {
        //reference to view and server
        private static ServerView view;
        private static ServerControl server;
        private static ASCIIEncoding encode = new ASCIIEncoding();

        private int m_ClientID;
        //one thread for each client object
        public Thread clientThread;
        //one socket for each client object
        public Socket clientSkt;
        //for writing through socket
        public NetworkStream ntwkstrm;

        public static void addReferenceToView(ServerView refToView)
        {
            view = refToView;
        }

        public static void addReferenceToMainController(ServerControl refToServer)
        {
            server = refToServer;
        }

        public ClientControl(Socket connectedFrom, int name)
        {
            try
            {
                this.m_ClientID = name;
                this.clientSkt = connectedFrom;
                //intialize stream with new socket
                ntwkstrm = new NetworkStream(clientSkt);
                //create delegate for runClient method
                ThreadStart runClientMethod = new ThreadStart(this.runClient);
                //initialize thread to run runClient
                clientThread = new Thread(runClientMethod);
                clientThread.Name = this.m_ClientID.ToString();
                clientThread.IsBackground = true;
                //start the thread
                clientThread.Start();
            }
            catch (ThreadStartException tse)
            {
                //if thread could not be started notify view
                view.Invoke(view.writeToStatusTextBox, "Thread start exception in ClientControl constructor: " + tse.ToString());
            }
            catch (Exception e)
            {
                //if any other exception occured notify view
                view.Invoke(view.writeToStatusTextBox, "Exception in ClientControl constructor: " + e.ToString());
            }
        }
        private void runClient()
        {
            /*Pre: thread created and socket initialized in constructor*/
            /*Post:runs until thread aborted.*/
            //recieve buffer
            byte[] recievedBytes = new byte[100];
            //send buffer
            byte[] sendBytes = new byte[100];

            string recievedString = "";
            int recievedLength = 0;
            try
            {
                while (true)
                {
                    try
                    {
                        //wait to recieve command from client and store command in recievedBytes
                        //clientSkt.Receive(recievedBytes);
                        if (ntwkstrm.CanRead && ntwkstrm.CanWrite)
                        {
                            //call read which returns number of bytes and stores data in first parameter - recievedBytes
                            recievedLength = ntwkstrm.Read(recievedBytes, 0, recievedBytes.Length);
                            //turn it into a string
                            recievedString = encode.GetString(recievedBytes, 0, recievedLength);

                            //output what the client sent to the view
                            view.Invoke(view.writeToStatusTextBox, "Recieving Session: " + this.clientThread.Name +
                                "\r" + recievedString);

                            //send client back the results of parsing the command message
                            this.parseMessage(recievedString);

                            ntwkstrm.Flush();
                        }
                    }
                    catch (System.IO.IOException ioe)
                    {
                        //if client socket not null
                        if (clientSkt != null)
                        {
                            //if network stream not closed close and dispose it
                            if (ntwkstrm != null)
                            {
                                ntwkstrm.Close();
                                ntwkstrm.Dispose();
                            }
                        }
                        view.Invoke(view.writeToStatusTextBox, "IOException in runClient: " + ioe.Message);
                    }
                    catch (SocketException se)
                    {
                        //if client socket not null close it
                        if (clientSkt != null)
                        {
                            clientSkt.Close();
                        }
                        //if network stream not closed close it
                        if (ntwkstrm != null)
                        {
                            ntwkstrm.Close();
                            ntwkstrm.Dispose();
                        }
                        //do not attempt to restore connection - abort thread
                        clientThread.Abort();
                        view.Invoke(view.writeToStatusTextBox, "SocketException in runClient: " + se.Message);
                    }
                    catch (Exception e)
                    {
                        view.Invoke(view.writeToStatusTextBox, "Exception in runClient: " + e.Message);
                        continue;
                    }
                }
            }
            catch (ThreadAbortException tae)
            {
                //if client socket not null close it
                if (clientSkt != null) clientSkt.Close();
                //if network stream not closed close it
                if (ntwkstrm != null)
                {
                    ntwkstrm.Close();
                    ntwkstrm.Dispose();
                }
                view.Invoke(view.writeToStatusTextBox, "ThreadAbortException in runClient: " + tae.Message);
            }
            finally
            {
                //finally after done processing remove client from servers list of clients
                server.removeClientThread(this);
            }
        }
        private void parseMessage(string message)
        {
            //get first 4 characters
            char[] temp = new char[4];
            if (message.Length >= 4)
            {
                message.CopyTo(0, temp, 0, 4);
            }
            //new byte array for return value

            string temp2 ="";
            for (int i = 0; i < temp.Length; i++)
            {
                temp2 += temp[i];
            }
            //if first 4 chars is:
            switch (temp2)
            {
                //bid parse XML byte[] make XML string and makeBid
                case "BID:":
                    char[] XML = new char[message.Length - 4];
                    message.CopyTo(4, XML, 0, message.Length - 4);

                    string xml = "";
                    for (int i = 0; i < XML.Length; i++)
                    {
                        xml += XML[i];
                    }

                    server.makeBid(xml, this);
                    //tell server to update ALL clients
                    server.sendAuctionItems();
                    return;
                //new client says HELO.
                case "HELO":
                    //server returns a welcome message
                    view.Invoke(view.writeToStatusTextBox, "The client: " +
                        (clientSkt.RemoteEndPoint as IPEndPoint).Address + ":" + clientThread.Name +
                        " has joined.");
                    //tells client what mvoies are available
                    sendMessageToClient(server.getAuctionItemsAsBytes(this));
                    return;
                //client closing
                case "EXIT":
                    this.clientThread.Abort();
                    return;
                //command not recognized
                default:
                    //server attempts to figure out source of error
                    this.sendMessageToClient(encode.GetBytes("EROR"));
                    return;
            }
        }

        public void sendMessageToClient(byte[] sendBytes)
        {
            //sends specified byte[] to client
            view.Invoke(view.writeToStatusTextBox, ("Sending Session: " + this.clientThread.Name + encode.GetString(sendBytes)));
            ntwkstrm.Write(sendBytes, 0, sendBytes.Length);
            ntwkstrm.Flush();
        }
    }
}
