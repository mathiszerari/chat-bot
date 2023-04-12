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
        public Socket Socket { get; set; }
        public int Number { get; set; }
        public string Pseudo { get; set; }

        public User(Socket s, int num)
        {
            Socket = s;
            Number = num;
            Pseudo = String.Empty;
        }
    }
}
