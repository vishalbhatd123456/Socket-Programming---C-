using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SocketServerStarter
{
    class Program
    {
	static void Main(string[] args)
	{
	    //create an object of Socket class
	    //accepts AddressFamily, Socket Type and the protocol type
	    //Internetwork - IPV4
	    Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	    IPAddress ipAddr = IPAddress.Any; //constant , the program can use the loopback address - 127.0.0. or any port that is empty in PC
	    IPEndPoint ipEp = new IPEndPoint(ipAddr, 23001);
	    listenerSocket.Bind(ipEp);
	    //the parameter passed here will explain the number of clients max that can wait while the server is busy serving the requests
	    listenerSocket.Listen(5);
	    //Accept is a blocking / synchronous operation - our program will not proceed further unless result from previous step is obtained
	    var client = listenerSocket.Accept();
	    Console.WriteLine("Client Connected...."+client.ToString()+"- IP End Point:"+ client.RemoteEndPoint.ToString());
	    byte[] buff = new byte[128];
	    int numberOfReceivedBytes = 0;
	    while (true)
	    {
		    numberOfReceivedBytes = client.Receive(buff);
		    Console.WriteLine("Number of received bytes:" + numberOfReceivedBytes);
		    Console.WriteLine("Data sent by the client" +
		                      Encoding.ASCII.GetString(buff, 0, numberOfReceivedBytes));
		    var receivedText = Encoding.ASCII.GetString(buff, 0, numberOfReceivedBytes);
		    client.Send(buff);
		    if (receivedText == "x")
		    {
			    break;
		    }
		    Array.Clear(buff,0,buff.Length);
		    numberOfReceivedBytes = 0;
	    }

	    //We will use Telnet for the client side

	}
    }
}
