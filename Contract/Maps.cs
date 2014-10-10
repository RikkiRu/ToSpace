using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [Serializable]
    public class GameMapGlobalMap
    {
        public List<MapPlanet> planets;

        public List<MapShip> ships;

        public List<MapStation> stations;
    }

    [Serializable]
    public class GameMap
    {
        public int sizeX;
        public int sizeY;
    }

    [Serializable]
    public class MapPlanet:GameMap
    {
        public float x;
        public float y;
        public string name;
        public bool wasRenamed;
        public Player ownerHome;

        public GameObject[,] objects;
        public MapPlanetQuad[,] quads;
        public MapPlanetQuad defaultQuad;

        public mapPlanetResource enviroment;
    }


    [Serializable]
    public class mapPlanetResource
    {
        public int o2;
        public int water;
        public int trash;
        public int temperature;

        public int d_o2;
        public int d_water;
        public int d_trash;
    }

    [Serializable]
    public class MapFight:GameMap
    { }

    [Serializable]
    public class MapStation:GameMap
    { }

    [Serializable]
    public class MapSurvival:GameMap
    { }

    [Serializable]
    public class MapWorld:GameMap
    { }

    [Serializable]
    public class MapBuilding:GameMap
    { }

    [Serializable]
    public class MapTrading:GameMap
    { }

    [Serializable]
    public class MapShip:GameMap
    { }
}
