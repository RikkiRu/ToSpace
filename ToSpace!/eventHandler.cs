using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ToSpace_
{
    class eventHandler
    {
        Connection connect;
        StructureObjects structure;
        GeneratorPlanet generator;
        System.Timers.Timer gameTick;

        public eventHandler(Connection connect, StructureObjects structure)
        {
            generator = new GeneratorPlanet();

            this.connect = connect;
            this.structure = structure;

            connect.addToEvent("disconnect", disconnectCommand);
            connect.addToEvent("chat", chatWriter);
            connect.addToEvent("generatePlanetForNewPlayer", generatorForNewPlayer);

            connect.addToEvent("IwantMyMap!", sendMeMapPlease);
            connect.addToEvent("getPlayerList", playerListGet);

            connect.addToEvent("newPlanetName", newPlanetName);
            connect.addToEvent("getCapitalResourse", getCapitalResource);

            connect.addToEvent("makeWorker", makeWorker);

            gameTick = new System.Timers.Timer(GameObject.timeTick);
            gameTick.Elapsed += gameTick_Elapsed;
            gameTick.Start();
        }

        private void makeWorker(object x)
        {
            SignedData trans = (SignedData)x;
            int[] coord = (int[])trans.data;

            Player me = structure.PlayerList.Where(c => c.name == trans.from.name).FirstOrDefault();
            if(me!=null)
            {
                MapPlanet map = (MapPlanet)me.currentMap;

                if (map.objects[coord[0], coord[1]] != null && map.objects[coord[0], coord[1]] is Building)
                {
                    Building cap = (Building)map.objects[coord[0], coord[1]];

                    if(cap.type== buildingType.capital && cap.owner==me.name)
                    {
                        capitialResource resourse = (capitialResource)cap.resourses;
                        if (resourse.populationFree > 0)
                        {
                            resourse.emploee(1);

                            float xC = coord[0] * MapPlanet.quadSize;
                            float yC = coord[1] * MapPlanet.quadSize;

                            map.units.Add(new workerPlanet { owner = me.name, x = xC, y = yC });

                            for (int i = 0; i < structure.PlayerList.Count; i++)
                            {
                                if (structure.PlayerList[i].currentMap == map)
                                {
                                    sendMeUnitsPlease(structure.PlayerList[i], map.units);
                                }
                            }
                        }
                        else
                        {
                            connect.send(me, new Sending { operation = "message", data = "Нужен хотя бы один безработный" });
                        }
                    }
                }
            }
        }

        private void getCapitalResource(object x)
        {
            SignedData sd = (SignedData)x;

            int[] coord = (int[])sd.data;

            Player p = structure.PlayerList.Where(c => c.name == sd.from.name).FirstOrDefault();

            if(p!=null)
            {
                if(p.currentMap is MapPlanet)
                {
                    GameObject cap = (p.currentMap as MapPlanet).objects[coord[0], coord[1]];
                    if(cap!=null && cap is Building)
                    {
                        if((cap as Building).resourses is capitialResource)
                        {
                            connect.send(p, new Sending { operation = "takeCapResourse", data = (cap as Building).resourses });
                        }
                    }
                }
            }
        }

        private void newPlanetName(object x)
        {
            string[] rec = (string[])x;

            Player player = structure.PlayerList.Where(c => c.name == rec[1]).FirstOrDefault();

            if(player!=null)
            {
                if(player.currentMap!=null)
                {
                    if(player.currentMap is MapPlanet)
                    {
                        if((player.currentMap as MapPlanet).wasRenamed==false)
                        {
                            if (structure.world.planets.Where(c => c.name == rec[0]).FirstOrDefault() == null)
                            {
                                (player.currentMap as MapPlanet).wasRenamed = true;
                                (player.currentMap as MapPlanet).name = rec[0];
                                sendMeMapPlease(player);
                            }

                            else
                            {
                                connect.send(player, new Sending { operation = "message", data = "Такая планета уже есть." });
                            }

                        }
                        else
                        {
                            connect.send(player, new Sending { operation = "message", data = "Планета может быть названа лишь однажды." });
                        }
                    }
                }
            }
        }

        void gameTick_Elapsed(object sender, ElapsedEventArgs e)
        {
            for(int i=0; i<structure.PlayerList.Count; i++)
            {
                if (structure.PlayerList[i].currentMap is MapPlanet)
                {
                    resourseDelta(ref (structure.PlayerList[i].currentMap as MapPlanet).enviroment);

                    capitalResourceDelta(ref structure.PlayerList[i].resourceUpdater);

                    connect.send(structure.PlayerList[i], new Sending { operation = "plenetResource", data = (structure.PlayerList[i].currentMap as MapPlanet).enviroment });
                }
            }

         
        }

        void resourseDelta(ref mapPlanetResource x)
        {
            x.water += x.d_water;
            if (x.water > 100) x.water = 100;
            if (x.water < 0) x.water = 0;

            x.trash += x.d_trash;
            if (x.trash > 100)
            {
                x.trash = 100;
                x.water = 0;
                x.o2 = 0;
            }
            if (x.trash < 0) x.trash = 0;

            x.o2 += x.d_o2;
            if (x.o2 > 100) x.o2 = 100;
            if (x.o2 < 0) x.o2 = 0;

            if (x.temperature > 60 || x.temperature < -60)
            {
                x.o2 = 0;
                x.water = 0;
            }
            
        }

        void capitalResourceDelta (ref GameObject capital)
        {
            capitialResource res = (capitialResource)((capital as Building).resourses);
            res.delta();
        }

        public void disconnectCommand(object x)
        {
            Player x1 = structure.PlayerList.Where(c => c.name == ((Player)x).name).FirstOrDefault();

            if (x1 != null)
            {
                x1.online = false;
                
                connect.removeChanel(x1);

                connect.serverWrite("Отключился " + x1.name);
            }
        }

       

        public void chatWriter(object o)
        {
            SignedData x = (SignedData)o;

            string[] m = new string[2];
            m[0] = x.from.name;
            m[1] = (string)x.data;

            connect.sendToAll(new Sending { operation = "chat", data = m });

            Console.WriteLine(m[0] + ": " + m[1]);

        }

        public void generatorForNewPlayer(object p)
        {
            Player player =  structure.PlayerList.Where(c=>c.name==((Player)p).name).FirstOrDefault();

            MapPlanet yourSweetHome = generator.generatePlanet(structure.world.planets, generator.defaultSizeX, generator.defaultSizeY, new MapPlanetQuad { type = typeOfquad.grass }, 50, 30, 30, 30, new mapPlanetResource { o2 = 100, temperature = 27, water=100 });
            yourSweetHome.ownerHome = player;

            structure.world.planets.Add(yourSweetHome);
            player.currentMap = yourSweetHome;
           

            bool govPlaced = false;
            while (!govPlaced)
            {
                int rx = generator.rand.Next(0, yourSweetHome.sizeX);
                int ry = generator.rand.Next(0, yourSweetHome.sizeY);

                if (yourSweetHome.quads[rx, ry] == null || yourSweetHome.quads[rx, ry].type == typeOfquad.grass || yourSweetHome.quads[rx, ry].type == typeOfquad.snow || yourSweetHome.quads[rx, ry].type == typeOfquad.desert)
                {
                    govPlaced = true;
                    yourSweetHome.objects[rx, ry] = new Building { type = buildingType.capital, owner = player.name, resourses = new capitialResource { populationLimit=1, d_populdation=1, d_eat=1} };
                    player.resourceUpdater = yourSweetHome.objects[rx, ry];
                }
            }


            Console.WriteLine("Сгенерирована карта для " + player.name);
        }


        public void sendMeMapPlease(object mememe)
        {
            Console.WriteLine("Запрос на карту ");
            Player whoAreYou = structure.PlayerList.Where(c=>c.name==((Player)mememe).name).FirstOrDefault();
            GameMap map = whoAreYou.currentMap;

            if(map!=null)
            {
                connect.send(whoAreYou, new Sending { operation = "takeYourMap!", data = map});


                if (map is MapPlanet)
                {
                    List<planetUnit> bebe = (map as MapPlanet).units;
                    sendMeUnitsPlease(whoAreYou, bebe);
                }
            }
        }


        public void sendMeUnitsPlease(Player meme, List<planetUnit> a)
        {
            connect.send(meme, new Sending { operation = "clearYourUnits" });

            int t = 0;
            while (t < a.Count)
            {
                List<planetUnit> bebe = new List<planetUnit>();
                for (int i = t; i < t + 10; i++)
                {
                    if(i<a.Count)
                    {
                        bebe.Add(a[t]);
                    }
                }
                t += 10;
                
                connect.send(meme, new Sending { operation = "addUnits", data = bebe });
            }
        }

        public void playerListGet(object x)
        {
            Player x1 = structure.PlayerList.Where(c => c.name == ((Player)x).name).FirstOrDefault();
            if(x1!=null)
            {
                Dictionary<string, bool> who = new Dictionary<string, bool>();

                for(int i=0; i<structure.PlayerList.Count; i++)
                {
                    if (structure.PlayerList[i].online) who.Add(structure.PlayerList[i].name, true);
                }

                for (int i = 0; i < structure.PlayerList.Count; i++)
                {
                    if (!structure.PlayerList[i].online) who.Add(structure.PlayerList[i].name, false);
                }

                connect.send(x1, new Sending { operation = "playerList", data = who });
            }
        }

        
    }
}
