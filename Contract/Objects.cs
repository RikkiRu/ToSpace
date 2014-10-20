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
        public static int timeTick = 3000;

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

   


}
