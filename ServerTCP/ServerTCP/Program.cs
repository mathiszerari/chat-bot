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

            // Quand le listener identifie une connexion // lorsqu'il detecte une nouvelle connexion la fonction accept return un objet socket qui correspond au client qui vient de creer cette nouvelle connexion
            Socket clientSocket = ServerSocket.Accept();
            Console.WriteLine("connexion reussis !"); //pop lorsqu'on se connect, pas lorsqu'on envoie premier message
            byte[] buffer = new byte[clientSocket.SendBufferSize];

            // remplie le buffer qu'on lui passe en parametre 
            // et en bonus return un int qui correspond a la taille (nbr de byte) de ce qu'il vient de remplir dans le buffer
            int readByte = clientSocket.Receive(buffer);
            Console.WriteLine(readByte);
            Console.WriteLine(buffer);
            Console.ReadKey();
        }
    }
}