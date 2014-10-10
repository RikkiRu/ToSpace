using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSpace_
{
    public class GeneratorPlanet
    {
        public Random rand = new Random();

        public int planetsPerLine = 3;
        public int distansePlanetX = 10;
        public int distansePlanetY = 10;
        public int lastX = 0;
        public int lastY = 0;
        public int inLineCurrent = 0;

        public int defaultSizeX = 10;
        public int defaultSizeY = 10;

        public string makeName(List<MapPlanet> exists, string iWantThisName = "")
        {
            if (iWantThisName == "")
            {
                iWantThisName = "Безымянная";
            }

            bool ohWeHave = true;
            int index = 1;
            string backup = iWantThisName;

            while (ohWeHave)
            {
                MapPlanet f = exists.Where(c => c.name == iWantThisName).FirstOrDefault();

                if (f == null) ohWeHave = false;
                else
                {
                    index++;
                    iWantThisName = backup + " " + index.ToString();
                }
            }

            return iWantThisName;
        }

        public MapPlanet generatePlanet(List<MapPlanet> exists, int sizeX, int sizeY, MapPlanetQuad defQuad, int waterPercent, int forestPercent, int icePercent, int desertPercent, mapPlanetResource enviroment)
        {
            MapPlanet res = new MapPlanet();

            res.name = makeName(exists);
            res.defaultQuad = defQuad;

            lastX += distansePlanetX;
            res.x = lastX;
            res.y = lastY;
            if (inLineCurrent >= planetsPerLine)
            {
                lastY += distansePlanetY;
                inLineCurrent = 0;
                lastX = 0;
            }

            res.sizeX = sizeX;
            res.sizeY = sizeY;

            res.quads = new MapPlanetQuad[sizeX, sizeY];
            res.objects = new GameObject[sizeX, sizeY];


            int Ssize = sizeX * sizeY;

            int oceanTotal = Ssize * waterPercent / 100;
            int forestTotal = Ssize * forestPercent / 100;


            for (int i = 0; i < oceanTotal; i++)
            {
                int rx = rand.Next(0, sizeX);
                int ry = rand.Next(0, sizeY);

                res.quads[rx, ry] = new MapPlanetQuad();
                res.quads[rx, ry].type = typeOfquad.water;
            }

            int LineIce = res.sizeY * icePercent / 100;

            for (int j = 0; j < LineIce; j++)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    if (j == LineIce - 1)
                    {
                        if (res.quads[i, j] == null) res.quads[i, j] = new MapPlanetQuad();
                        res.quads[i, j].type = typeOfquad.snow;
                        res.quads[i, j].textureParams = 1;
                    }

                    else
                    {
                        if (res.quads[i, j] != null && res.quads[i, j].type == typeOfquad.water)
                        {
                            res.quads[i, j].type = typeOfquad.ice;
                        }
                        else
                        {
                            if (res.quads[i, j] == null) res.quads[i, j] = new MapPlanetQuad();
                            res.quads[i, j].type = typeOfquad.snow;
                            res.quads[i, j].textureParams = 0;
                        }
                    }
                }
            }



            int LineDesert = res.sizeY - res.sizeY * desertPercent / 100;

            for (int j = sizeY-1; j >= LineDesert; j--)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    if (j == LineDesert)
                    {
                        if (res.quads[i, j] == null) res.quads[i, j] = new MapPlanetQuad();
                        res.quads[i, j].type = typeOfquad.desert;
                        res.quads[i, j].textureParams = 1;
                    }

                    else
                    {
                        if (res.quads[i, j] != null && res.quads[i, j].type == typeOfquad.water)
                        {
                            if (rand.Next(0, 10) == 0)
                                res.quads[i, j].type = typeOfquad.oasis;
                            else
                            {
                                res.quads[i, j].type = typeOfquad.desert;
                                res.quads[i, j].textureParams = 0;
                            }
                        }
                        else
                        {
                            if (res.quads[i, j] == null) res.quads[i, j] = new MapPlanetQuad();
                            res.quads[i, j].type = typeOfquad.desert;
                            res.quads[i, j].textureParams = 0;
                        }
                    }
                }
            }

            setupWaterTurns(ref res.quads, typeOfquad.water);
            setupWaterTurns(ref res.quads, typeOfquad.ice);



            for (int i = 0; i < forestTotal; i++)
            {
                int rx = rand.Next(0, sizeX);
                int ry = rand.Next(0, sizeY);

                typeOfquad tQuad = defQuad.type;
                if (res.quads[rx, ry] != null) tQuad = res.quads[rx, ry].type;


                switch(tQuad)
                {
                    case typeOfquad.snow:
                        res.objects[rx, ry] = new forest();
                        res.objects[rx, ry].textureParams = 0;
                        break;
                    case typeOfquad.grass:
                        res.objects[rx, ry] = new forest();
                        res.objects[rx, ry].textureParams = 1;
                        break;
                    case typeOfquad.desert:
                        res.objects[rx, ry] = new forest();
                        if(rand.Next(0,2)==0)
                            res.objects[rx, ry].textureParams = 2;
                        else
                            res.objects[rx, ry].textureParams = 3;
                        break;
                }
            }

            res.enviroment = enviroment;
            res.enviroment.d_o2 = forestTotal;
            res.enviroment.d_water = oceanTotal;
            

            return res;
        }

        public void setupWaterTurns(ref MapPlanetQuad[,] map, typeOfquad wut)
        {
            int sx = map.GetLength(0);
            int sy = map.GetLength(1);

            for (int i = 0; i < sx; i++)
            {
                for (int j = 0; j < sy; j++)
                {

                    if (map[i, j] != null && map[i, j].type == wut)
                    {
                        bool[] round = new bool[4];
                        if (i > 0) if (map[i - 1, j] != null && map[i - 1, j].type == wut) round[0] = true;
                        if (j < sy - 1) if (map[i, j + 1] != null && map[i, j + 1].type == wut) round[1] = true;
                        if (i < sx - 1) if (map[i + 1, j] != null && map[i + 1, j].type == wut) round[2] = true;
                        if (j > 0) if (map[i, j - 1] != null && map[i, j - 1].type == wut) round[3] = true;

                        int[] paramsR = new int[2];

                        for (int k = 0; k < 4; k++)
                        {
                            if (round[k]) paramsR[0]++;
                        }

                        switch (paramsR[0])
                        {
                            case 1:
                                if (round[2]) paramsR[1] = 0;
                                if (round[1]) paramsR[1] = 90;
                                if (round[0]) paramsR[1] = 180;
                                if (round[3]) paramsR[1] = 270;
                                break;

                            case 2:
                                if (round[0] && round[1]) paramsR[1] = 90;

                                if (round[0] && round[2]) paramsR[1] = -180;//отдельная история

                                if (round[0] && round[3]) paramsR[1] = 180;
                                if (round[1] && round[2]) paramsR[1] = 0;

                                if (round[1] && round[3]) paramsR[1] = -90;//отдельная история

                                if (round[2] && round[3]) paramsR[1] = 270;
                                break;


                            case 3:
                                if (!round[0]) paramsR[1] = 270;
                                if (!round[1]) paramsR[1] = 180;
                                if (!round[2]) paramsR[1] = 90;
                                if (!round[3]) paramsR[1] = 0;
                                break;
                        }



                        map[i, j].textureParams = paramsR;

                    }

                }
            }
        }
    }
}
