using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ToSpace_
{
    class Connection
    {
        public Dictionary<string, RecieveDel> RecFunctions = new Dictionary<string, RecieveDel>();
        public Dictionary<string, string> Passwords;


        public void addToEvent(string name, RecieveDel func)
        {
            if(!RecFunctions.ContainsKey(name))
            {
                RecFunctions.Add(name, func);
            }
        }

        int port;
        Socket serverSocket;

        List<Player> players;

        public List<Chanel> chanels;

        public void removeChanel(Player x)
        {
            x.online = false;

            if(chanels!=null)
            {
                Chanel a = chanels.Where(c => c.who == x).FirstOrDefault();

                if(a!=null)
                {
                    send(a, new Sending { operation = "byebye" });
                    chanels.Remove(a);
                }
            }

            Console.WriteLine("Отключен " + x.name);
        }

        public void Start(List<Player> players, Dictionary<string, string> paswords)
        {
            this.players = players;
            this.Passwords = paswords;

            chanels = new List<Chanel>();

            port = 8083;
            IPHostEntry localInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint myEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            serverSocket = new Socket(myEP.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(myEP);
            serverSocket.Listen((int)SocketOptionName.MaxConnections);
            serverSocket.BeginAccept(AcceptCallback, serverSocket);
            Console.WriteLine("Запуск сервера произведен");
        }

        void AcceptCallback(IAsyncResult result)
        {
            try
            {
                Socket s = (Socket)result.AsyncState;

                Chanel joiningPlayer = new Chanel();

                joiningPlayer.socket = s.EndAccept(result);
                joiningPlayer.buffer = new byte[Sending.SizeOfMessage];
                joiningPlayer.socket.BeginReceive(joiningPlayer.buffer, 0, Sending.SizeOfMessage, SocketFlags.None, new AsyncCallback(RecieveCallbackOnJoin), joiningPlayer);

                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        BinaryFormatter binFormat = new BinaryFormatter();


        public void serverWrite(string text)
        {
            string[] m = new string[2];
            m[0] = "Server";
            m[1] = text;

            sendToAll(new Sending { operation = "chat", data = m });

            Console.WriteLine(m[0] + ": " + m[1]);
        }

        void RecieveCallbackOnJoin(IAsyncResult result)
        {
            //try
            //{
                Chanel joined = (Chanel)result.AsyncState;

                Sending soWhatsHere = sendDecode(joined);

                Player me = (Player)soWhatsHere.data;

               

                Player old = players.Where(c => c.name == me.name).FirstOrDefault();

                //Вернулся.
                if (old != null)
                {
                    if (Passwords[me.name]==soWhatsHere.password)
                    {

                        if (old.online)
                        {
                            Sending nowOnline = new Sending();
                            nowOnline.operation = "Exception";
                            nowOnline.data = new Exception("Вы уже онлайн");
                            Console.WriteLine("Уже онлайн! " + old.name);
                            send(joined, nowOnline);
                        }

                        else
                        {
                            old.online = true;
                            joined.who = old;

                            chanels.Add(joined);

                            Sending okay = new Sending();
                            okay.operation = "okYouOnline";
                            okay.data = "Вы успешно зашли на сервер. Удачной игры!";
                            send(joined, okay);

                            

                            joined.socket.BeginReceive(joined.buffer, 0, Sending.SizeOfMessage, SocketFlags.None, new AsyncCallback(RecieveCallback), joined);

                            serverWrite("В игру вернулся " + old.name);
                        }
                    }

                    else
                    {
                        Sending paswNotCorrect = new Sending();
                        paswNotCorrect.operation="Exception";
                        paswNotCorrect.data=new Exception("Ошибка в пароле!");

                        Console.WriteLine(old.name + " ошибся с паролем");

                        send(joined, paswNotCorrect);
                    }
                }

                //Значит новенький.
                else
                {
                    me.online = true;
                    players.Add(me);
                    joined.who = me;
                    chanels.Add(joined);


                    Sending okay = new Sending();
                    okay.operation = "okYouOnline";
                    okay.data = "Вы успешно зарегистрированы на сервере. Удачной игры!";

                    Passwords.Add(me.name, soWhatsHere.password);

                    send(me, okay);

                    forceCallEvent("generatePlanetForNewPlayer", me);

                    

                    joined.socket.BeginReceive(joined.buffer, 0, Sending.SizeOfMessage, SocketFlags.None, new AsyncCallback(RecieveCallback), joined);


                    serverWrite("Приветствуем нового игрока - " + me.name);
                }
            //}

            //catch (Exception e)
            //{
                
            //    Console.WriteLine("При новом подключении ошибка: " + e.Message);
            //    throw new Exception();
            //}
        }


        Sending sendDecode(Chanel inC)
        {
            MemoryStream x = new MemoryStream(inC.buffer);
            return (Sending)binFormat.Deserialize(x);
        }

        public void send(Player who, Sending command)
        {
            Chanel chan = chanels.Where(c => c.who == who).FirstOrDefault();
            if (chan != null)
            {
                send(chan, command);
            }
        }

        public void send(Chanel who, Sending command)
        {
            try
            {
                MemoryStream x = new MemoryStream();
                binFormat.Serialize(x, command);
                who.socket.Send(x.GetBuffer());
            }
            catch(Exception e)
            {
                Console.WriteLine("При предачи данных: " + e.Message);
            }
        }

        public void sendToAll(Sending command)
        {
            for(int i=0; i<chanels.Count; i++)
            {
                send(chanels[i], command);
            }
        }

        void RecieveCallback(IAsyncResult result)
        {
            Chanel connection = (Chanel)result.AsyncState;
            if (chanels.Contains(connection))
            {
                //try
                //{
                    connection.socket.EndReceive(result);

                    Sending a = sendDecode(connection);

                    if(a.name!=string.Empty && Passwords.ContainsKey(a.name) && Passwords[a.name]==a.password) forceCallEvent(a.operation, a.data);
                    else
                    {
                        Console.WriteLine("Хм, нам пытаются послать комманду с не корректным паролем. Казнить еретика?");
                    }

                    connection.socket.BeginReceive(connection.buffer, 0, Sending.SizeOfMessage, SocketFlags.None, new AsyncCallback(RecieveCallback), connection);
                //}

                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    removeChanel(connection.who);
                //}
            }
        }

        void forceCallEvent(string operation, object data)
        {
            if (RecFunctions.ContainsKey(operation)) RecFunctions[operation](data);
        }

        public void close()
        {
            try
            {
                serverSocket.Shutdown(SocketShutdown.Both);
            }
            catch
            {
                serverSocket.Close();
            }
            Console.WriteLine("Сервер был остановлен");
        }


       
    }

    class Chanel
    {
        public Socket socket;
        public byte[] buffer;
        public Player who;
    }
}
