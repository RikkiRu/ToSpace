using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [Serializable]
    public class GameObject
    {
        public object forRender;
        public object textureParams;
    }

    [Serializable]
    public class Technology
    {
        public string name;
        Dictionary<TechnologyTypes, int> TechLevelsToAchive;
        List<Technology> TechNeedToAchive;
    }

    [Serializable]
    public enum TechnologyTypes
    { war, constructing, bio, production, mining, social}

    [Serializable]
    public class Building:GameObject
    {
        public buildingType type;
        public string owner;
    }

    [Serializable]
    public enum buildingType
    {
        capital
    }

    [Serializable]
    public class nature:GameObject
    { }

    [Serializable]
    public class forest:nature
    { }
}
