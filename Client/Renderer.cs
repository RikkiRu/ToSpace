using Contract;
using GreenRender;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Client
{
    class Renderer
    {
        public mainRenClass display;
        public GameMap currentMap;
        public textureObject backgroundQuad;
        public textureObject backgroundStatic;
        public textureObject quadSelection = null;
        public textureObject border = null;
        public Player me;
        public playerPos position = new playerPos();

        public float quadSize = 10f;

        public Renderer(GLControl x, Player me)
        {
            this.me = me;
            display = new mainRenClass(x, 100);
            textureObject.wayToTexturesFolder = Environment.CurrentDirectory + @"/Textures";
        }

        public void draw()
        {
            GL.LoadIdentity();


            if (backgroundStatic != null) display.drawObject(backgroundStatic);
            

            GL.Translate(position.x, position.y, 0);

            if (border != null) display.drawObject(border);

            if(currentMap!=null)
            {
                if (currentMap is MapPlanet) drawPlanetMap(currentMap as MapPlanet);
            }

            if(quadSelection!=null)
            {
                display.drawObject(quadSelection);
            }

        }

        public void drawPlanetMap(MapPlanet x)
        {
            if(backgroundQuad==null)
            {
                float orth = display.ortoX+5;
                if (display.ortoX < display.ortoY) orth = display.ortoY+5;

                backgroundStatic = new textureObject(@"planet/PlanetBack", 1000, orth, orth);
                backgroundStatic.x = (int)display.ortoX / 2;
                backgroundStatic.y = (int)display.ortoY / 2;


                

                if(x.defaultQuad.type == typeOfquad.grass)
                {
                    backgroundQuad = new textureObject(@"planet/planetGrass", 0, x.sizeX * quadSize, x.sizeY * quadSize, x.sizeX, x.sizeY);
                    backgroundQuad.x = (x.sizeX-1) * quadSize / 2;
                    backgroundQuad.y = (x.sizeY-1) * quadSize / 2;
                }

                border = new textureObject(@"planet/border", 0, quadSize * (x.sizeX + 0.5f), quadSize * (x.sizeY + 0.5f));
                border.x = backgroundQuad.x;
                border.y = backgroundQuad.y;
            }

            else display.drawObject(backgroundQuad);

            for(int i=0; i<x.sizeX; i++)
            {
                for(int j=0; j<x.sizeY; j++)
                {
                    float xi = i * quadSize;
                    float xj = j * quadSize;

                    if (x.quads[i, j] != null)
                    {
                        drawQuad(x.quads[i, j], xi, xj);
                    }

                    if (x.objects[i,j]!=null)
                    {
                        drawObject(x.objects[i, j], xi, xj);
                    }
                }
            }
        }

        public void drawQuad(MapPlanetQuad quad, float xi, float xj)
        {
            if(quad.forRender==null)
            {
                nullSetup.makeRender(ref quad, quadSize);
            }

            textureObject t = quad.forRender as textureObject;

            t.x = xi;
            t.y = xj;

            display.drawObject(t);
        }

        public void drawObject(GameObject x, float xi, float xj)
        {
            if (x.forRender == null)
            {
                nullSetup.makeRender(ref x, quadSize);
            }

            textureObject t = x.forRender as textureObject;

            t.x = xi;
            t.y = xj;

            display.drawObject(t);
        }

        public void makeSelection(float x, float y)
        {
            if (currentMap != null)
            {
                int i = (int)(Math.Round(x/quadSize));
                int j = (int)(Math.Round(y/quadSize));



                quadSelection = new textureObject(@"planet/QuadSelection", 0, quadSize, quadSize);
                quadSelection.x = i*quadSize;
                quadSelection.y = j*quadSize;

                //MapPlanet a = (MapPlanet)currentMap;

                //((textureObject)a.objects[i, j].forRender).mask = Color.Red;

            }
        }
    }




    public class playerPos
    {
        public float x;
        public float y;

        public bool w;
        public bool s;
        public bool a;
        public bool d;

        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;

        public void move()
        {
            if (w) y += speedY;
            if (s) y -= speedY;
            if (a) x += speedX;
            if (d) x -= speedX;

            if (x > xMax) x = xMax;
            if (x < xMin) x = xMin;
            if (y > yMax) y = yMax;
            if (y < yMin) y = yMin;
        }

        public float speedX = 3f;
        public float speedY = 3f;
    }
}
