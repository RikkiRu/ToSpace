using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class ClientConnectionSys
    {

        Dictionary<string, RecieveDel> RecFunctions; 
        public bool working = true;
        public string myPassword = string.Empty;

        public void addToEvent(string name, RecieveDel func)
        {
            if (!RecFunctions.ContainsKey(name))
            {
                RecFunctions.Add(name, func);
            }
        }

        public void discFinal(object a)
        {
            working = false;
            //MessageBox.Show("Вас отключили от сервера");
        }

        public void ExceptionF(object a)
        {
            working = false;

            Exception e = (Exception)a;

            FormBegin.SuccesConnected = false;
            FormBegin.Tested = true;

            throw (e);
        }

        public void OkayWeConnected(object o)
        {
            FormBegin.SuccesConnected = true;
            FormBegin.Tested = true;
            FormBegin.welcomeStr = (string)o;
        }

        public ClientConnectionSys(Player me, string ip, int port, string myPassword)
        {
            this.RecFunctions = new Dictionary<string, RecieveDel>();
            this.myPassword = myPassword;

            addToEvent("byebye", discFinal);
            addToEvent("Exception", ExceptionF);
            addToEvent("okYouOnline", OkayWeConnected);
            addToEvent("message", messageShow);

            this.me = me;

            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(ip, port);

            listen = new Thread(StartListen);
            listen.Start();

            Sending s = new Sending();
     
            s.data = me;
            send(s);
        }

        private void messageShow(object x)
        {
            MessageBox.Show((string)x);
        }

        Player me;
        Socket ClientSocket;
        Thread listen;
        BinaryFormatter binFormat = new BinaryFormatter();

        public void send(Sending x)
        {
            x.name = me.name;
            x.password = myPassword;

            MemoryStream data = new MemoryStream();
            binFormat.Serialize(data, x);
            ClientSocket.Send(data.GetBuffer());
        }

        void StartListen()
        {
            try
            {
                while (working)
                {
                    byte[] buffer = new byte[Sending.SizeOfMessage];
                    ClientSocket.Receive(buffer);
                    MemoryStream x = new MemoryStream(buffer);
                    Sending data = (Sending)binFormat.Deserialize(x);

                    if (RecFunctions.ContainsKey(data.operation)) RecFunctions[data.operation](data.data);
                }

                ClientSocket.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
        }

        public void disconnect()
        {
            if(working) send(new Sending { operation = "disconnect", data = me });
        }
    }
}
