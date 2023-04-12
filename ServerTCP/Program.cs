using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;

namespace ServerTCP
{
    public class Program
    {


        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to the main");
            new Server();

        }

    }
}