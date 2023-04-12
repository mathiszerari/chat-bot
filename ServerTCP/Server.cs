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
        //private static readonly List<Socket> _sockets = new List<Socket>();
        private static readonly List<User> _users = new();

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
                _clientNb++;

                User user = new User(clientSocket, _clientNb);
                _users.Add(user);

                // pour chaque nouvelle connexion on a un nouveau thread qui va gérer ce client a traver une fonction
                // le thread aura besoin d'au moins 1 info, le socket avec lequel travailler
                Thread clientThread;
                clientThread = new Thread(() => ClientConnection(user));
                clientThread.Start();
            }
        }


        // fonction qui tourne sur les thread secondaire créer qui gère l'écoute des message de chaque client
        private static void ClientConnection(User user)
        {
            byte[] buffer = new byte[user.Socket.SendBufferSize];
            //Console.WriteLine("Initial size of buffer" + buffer.Count());

            int readByte;

            Console.WriteLine("connexion réussis");
            // je rajoute un try catch pour la gestion des deconnexion sauvage coté client
            try
            {
                do
                {
                    // Reception
                    readByte = user.Socket.Receive(buffer); // remplie le buffer qu'on lui passe en parametre et  return un int qui correspond a la taille (nbr de byte) de ce qu'il vient de remplir dans le buffer
                    // Traitement
                    string textReceived = System.Text.Encoding.UTF8.GetString(buffer);
                    if (user.Pseudo.Length == 0)
                    {
                        bool pseudoAvailable = true;
                        Console.WriteLine("on verifie la dispo");
                        if(_users.Any(x => x.Pseudo == textReceived))
                        { 
                                pseudoAvailable = false;
                                Console.WriteLine("meme pseudo trouvé");
                        }

                        if (pseudoAvailable)
                        {
                            user.Pseudo = textReceived;
                            user.Socket.Send(System.Text.Encoding.UTF8.GetBytes("Votre pseudo a bien été set \n"));
                            Console.WriteLine("un pseudo a été affecté");
                        }
                        else
                        {
                            user.Socket.Send(System.Text.Encoding.UTF8.GetBytes("Pseudo déjà prit \n"));
                        }
                
                    }
                    else
                    {
                        Console.WriteLine("envoie classique du message");

                        //Console.WriteLine("from (" + user.Number.ToString() + ") we got :" + textReceived);
                        // Reponse du server
                        user.Socket.Send(System.Text.Encoding.UTF8.GetBytes("message bien reçu \n"));
                        foreach (User u in _users)
                        {
                            u.Socket.Send(System.Text.Encoding.UTF8.GetBytes(user.Pseudo + " send : " + textReceived + "\n"));
                        }
                        Array.Clear(buffer, 0, buffer.Length);

                    }



                } while (readByte > 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                user.Socket.Close();
                _users.Remove(user);
                Console.WriteLine("connexion perdu");
            }

            Console.ReadKey(); // pour empecher la fermeture de la console
        }


    }
}
