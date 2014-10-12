using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [Serializable]
    public class Player
    {
        public string name;
        public Color colorMask;
        public bool online;

        [NonSerialized]
        public GameMap currentMap;

        [NonSerialized]
        public GameObject resourceUpdater;

        public CivSettings civSettings;
        public List<Technology> achivedTechnology;
    }

    [Serializable]
    public class CivSettings
    {
        public string name;
        public Dictionary<TechnologyTypes, int> technologySetup;
    }
}
