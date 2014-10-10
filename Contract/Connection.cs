using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public delegate void RecieveDel(object x);

    [Serializable]
    public class Sending
    {
        public string operation;
        public object data;

        public string name;
        public string password;

        public static int SizeOfMessage = 10000;
    }

    [Serializable]
    public class SignedData
    {
        public Player from;
        public object data;
    }
}
