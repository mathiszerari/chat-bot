using System;
using System.Net;
using System.Net.Sockets;

namespace ServerTCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the server side !");
            //création d'un socket
            // interNetwork : adresse de la famille Ipv4
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //création d'un point d'accés : l'adresse qu'il écoute et le port qu'il écoute
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 3042);
            // maintenant on connect les deux //associate a socket with a local endpoint
            ServerSocket.Bind(ipEndPoint);
            //on passe notre socket serveur en mode ecoute
            ServerSocket.Listen(10);

            int clientNb = 0;
            while (true)
            {
                ServerSocket.Listen(10);
                // Quand le listener identifie une connexion // lorsqu'il detecte une nouvelle connexion la fonction accept return un objet socket qui correspond au client qui vient de creer cette nouvelle connexion
                Socket clientSocket = ServerSocket.Accept();

                clientNb++;
                Thread clientThread;
                clientThread = new Thread(() => ClientConnection(clientSocket, clientNb));
                clientThread.Start();
            }       
        }



        private static void ClientConnection(Socket clientSocket, int clientNb)
        {
            Console.WriteLine("connexion réussis");
            byte[] buffer = new byte[clientSocket.SendBufferSize];
            // remplie le buffer qu'on lui passe en parametre 
            // et en bonus return un int qui correspond a la taille (nbr de byte) de ce qu'il vient de remplir dans le buffer
            int readByte;
            do
            {
                // remplie le buffer qu'on lui passe en parametre 
                // et en bonus return un int qui correspond a la taille (nbr de byte) de ce qu'il vient de remplir dans le buffer
                readByte = clientSocket.Receive(buffer);
                byte[] rData = new byte[readByte];
                // on creer une copie a une taille nouvelle ajusté au nombre de byte reçu
                Array.Copy(buffer, rData, readByte);


                Console.WriteLine("we got :" + System.Text.Encoding.UTF8.GetString(rData));
                //Console.WriteLine("we got :" + readByte.ToString());

                // Reponse du server
                clientSocket.Send(System.Text.Encoding.UTF8.GetBytes("reponseServ \n"));

            } while (readByte > 0);
            Console.ReadKey();
        }
    }
}