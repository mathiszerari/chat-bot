using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
{
    public class Server
    {
        private int _clientNb = 0;
        private static readonly List<Socket> _sockets = new List<Socket>();

        public Server()
        {
            Console.WriteLine("Welcome to the server side !");
            // création d'un socket :  interNetwork : adresse de la famille Ipv4
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // création d'un point d'accés 
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 3042);
            // maintenant on connect les deux 
            serverSocket.Bind(ipEndPoint);

            // boucle qui tourne sur le thread principale et qui gère l'écoute de nouvelle connexion
            while (true)
            {
                Console.WriteLine("j'attend une connexion");
                serverSocket.Listen(10);
                Socket clientSocket = serverSocket.Accept();  // Quand le server identifie une connexion la fonction accept return un objet socket qui correspond au client qui vient de creer cette nouvelle connexion

                _sockets.Add(clientSocket);

                _clientNb++;
                // pour chaque nouvelle connexion on a un nouveau thread qui va gérer ce client a traver une fonction
                // le thread aura besoin d'au moins 1 info, le socket avec lequel travailler
                Thread clientThread;
                clientThread = new Thread(() => ClientConnection(clientSocket, _clientNb));
                clientThread.Start();
            }
        }


        // fonction qui tourne sur les thread secondaire créer qui gère l'écoute des message de chaque client
        private static void ClientConnection(Socket clientSocket, int clientNb)
        {
            byte[] buffer = new byte[clientSocket.SendBufferSize];
            //Console.WriteLine("Initial size of buffer" + buffer.Count());

            int readByte;

            Console.WriteLine("connexion réussis");
            // je rajoute un try catch pour la gestion des deconnexion sauvage coté client
            try
            {
                do
                {
                    // Reception
                    readByte = clientSocket.Receive(buffer); // remplie le buffer qu'on lui passe en parametre et  return un int qui correspond a la taille (nbr de byte) de ce qu'il vient de remplir dans le buffer
                    // Traitement
                    string textReceived = System.Text.Encoding.UTF8.GetString(buffer);

                    //Console.WriteLine("size of data" + readByte);
                    //Console.WriteLine("size of buffer" + buffer.Count());
                    Console.WriteLine("from (" + clientNb.ToString() + ") we got :" + textReceived);
                    // Reponse du server
                    clientSocket.Send(System.Text.Encoding.UTF8.GetBytes("message bien reçu \n"));
                    foreach (Socket s in _sockets)
                    {
                        s.Send(System.Text.Encoding.UTF8.GetBytes("someone send : " + textReceived + "\n"));
                    }
                    Array.Clear(buffer, 0, buffer.Length);



                } while (readByte > 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                clientSocket.Close();
                _sockets.Remove(clientSocket);
                Console.WriteLine("connexion perdu");
            }

            Console.ReadKey(); // pour empecher la fermeture de la console
        }


    }
}
