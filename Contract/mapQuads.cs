using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [Serializable]
    public class MapPlanetQuad : GameObject
    {
        public typeOfquad type;
    }

    [Serializable]
    public enum typeOfquad { water, grass, ice, snow, desert, oasis }
}
