using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSpace_
{
    class StructureObjects
    {
        public List<Player> PlayerList = new List<Player>();
        public Dictionary<string, string> passwords = new Dictionary<string, string>();

        public GameMapGlobalMap world;

        public StructureObjects()
        {
            world = new GameMapGlobalMap();

            world.planets = new List<MapPlanet>();
        }
    }
}
