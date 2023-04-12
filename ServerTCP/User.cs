using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
{
    public class User
    {
        private Socket Socket { get; set; }
        private int Number { get; set; }
        private string Pseudo { get; set; }
        private int age { get; set; }

        public User(Socket s, int num)
        {
            Socket = s;
            Number = num;
            Pseudo = "";
        }
    }
}
