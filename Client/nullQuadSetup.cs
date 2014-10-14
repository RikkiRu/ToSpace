using Contract;
using GreenRender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class nullSetup
    {
        public static void makeRender(ref MapPlanetQuad quad, float quadSize)
        {
            switch (quad.type)
            {
                case typeOfquad.snow:
                    if ((int)quad.textureParams == 0)
                        quad.forRender = new textureObject(@"planet/snow/default", 0, quadSize, quadSize);
                    else
                        quad.forRender = new textureObject(@"planet/snow/border", 0, quadSize, quadSize);
                    break;

                case typeOfquad.grass:
                    quad.forRender = new textureObject(@"planet/planetGrass", 0, quadSize, quadSize);
                    break;

                case typeOfquad.oasis:
                    quad.forRender = new textureObject(@"planet/desert/oazis", 0, quadSize, quadSize);
                    break;

                case typeOfquad.ice:
                case typeOfquad.water:
                    int[] param = (int[])quad.textureParams;

                    string wut = @"planet/water/water";
                    if (quad.type == typeOfquad.ice) wut = @"planet/ice/ice";

                    string name = wut + "0";
                    int angle = param[1];
                    int animation = 0;

                    switch (param[0])
                    {
                        case 0:
                            animation = 300;
                            break;

                        case 1:
                            name = wut + "1";
                            break;

                        case 2:
                            if (param[1] < 0) name = wut + "2I";
                            else name = wut + "2T";
                            break;

                        case 3:
                            name = wut + "3";
                            break;

                        case 4:
                            name = wut + "4";
                            break;
                    }


                    quad.forRender = new textureObject(name, animation, quadSize, quadSize);
                    (quad.forRender as textureObject).angle = angle;
                    break;

                case typeOfquad.desert:
                    if ((int)quad.textureParams == 0)
                        quad.forRender = new textureObject(@"planet/desert/default", 0, quadSize, quadSize);
                    else
                        quad.forRender = new textureObject(@"planet/desert/border", 0, quadSize, quadSize);
                    break;

            }




        }

        public static void makeRender(ref GameObject quad, float quadSize)
        {
            if (quad is forest)
            {
                int p = (int)(quad as forest).textureParams;

                switch (p)
                {
                    case 0: quad.forRender = new textureObject(@"planet/Forest/pine", 0, quadSize, quadSize); break;
                    case 1: quad.forRender = new textureObject(@"planet/Forest/apple", 0, quadSize, quadSize); break;
                    case 2: quad.forRender = new textureObject(@"planet/Forest/palm", 0, quadSize, quadSize); break;
                    case 3: quad.forRender = new textureObject(@"planet/Forest/cactus", 0, quadSize, quadSize); break;
                }
                
            }

            if (quad is Building)
            {
                switch ((quad as Building).type)
                {
                    case buildingType.capital:
                        quad.forRender = new textureObject(@"planet/Capital", 0, quadSize, quadSize);
                        break;
                }
            }

            if(quad is planetUnit)
            {
                if (quad is workerPlanet)
                {
                    quad.forRender = new textureObject(@"planet/Worker", 0, quadSize/2, quadSize/2);
                }
            }
        }
    }

    
}
