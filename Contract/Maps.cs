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
        public static float quadSize = 10f;

        public float x;
        public float y;
        public string name;
        public bool wasRenamed;

        public MapPlanetQuad[,] quads;
        public MapPlanetQuad defaultQuad;
        public GameObject[,] objects;

        [NonSerialized]
        public mapSector[,] sectors;

        public mapPlanetResource enviroment;
    }

    [Serializable]
    public class mapSector:GameMap
    {
        public static int sectorQuadsCount = 10;

        public MapPlanetQuad quadTemplate;
        public Building[] buildings;
        public natureSector[] natures;

        public mapSectorResourses resourses;
    }

    [Serializable]
    public class nature : GameObject
    { }

    [Serializable]
    public class mapSectorResourses
    { }

    [Serializable]
    public class mapPlanetResource
    {
        public int o2;
        public int temperature;
        public int d_o2;
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
