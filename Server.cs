using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Internal;

namespace ServerTest
{
    class Serv
    {
        private static Socket servSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> clientSockets = new List<Socket>();
        private static byte[] buffer = new byte[1024];

        static void Main(string[] args)
        {
            SetupServer();
            Console.ReadLine();
        }

        private static void SetupServer()
        {
            Console.WriteLine("start serv");
            servSocket.Bind(new IPEndPoint(IPAddress.Any, 3042));
            servSocket.Listen(5);
            servSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = servSocket.EndAccept(AR);
            clientSockets.Add(socket);
            Console.WriteLine("Client Connected");
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            servSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int dataReceived = socket.EndReceive(AR);
            byte[] dataBuff = new byte[dataReceived];
            Array.Copy(buffer, dataBuff, dataReceived);

            string txt = Encoding.ASCII.GetString(dataBuff);
            Console.WriteLine(txt);

            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            servSocket.BeginAccept(new AsyncCallback(SendCallback), null);
        }

        private static void SendCallback(IAsyncResult AR)
        {
            Console.WriteLine(AR);
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}