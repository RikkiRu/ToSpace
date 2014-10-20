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
        public string owner;
    }

    [Serializable]
    public enum typeOfquad { water, grass, ice, snow, desert, oasis }


    [Serializable]
    public class planetUnit : GameObject
    {
        public static float speed = 1;

        public string owner;
        public float x;
        public float y;

        public float moveToX;
        public float moveToY;
    }

    [Serializable]
    public class workerPlanet : planetUnit
    {

    }


    [Serializable]
    public class Building : GameObject
    {
        public static int limitResource = 100000;

        public buildingType type;
        public string owner;

        public object resourses;
    }

    [Serializable]
    public enum buildingType
    {
        capital
    }


    [Serializable]
    public class capitialResource
    {
        public int eat;
        public int d_eat;
        public int wood;
        public int d_wood;
        public int myWater;
        public int d_myWater;
        public int myO2;
        public int d_myO2;

        public int populationFree;
        public int d_populdation;
        public int populationWorkers;
        public int populationLimit;

        public void emploee(int c)
        {
            populationFree -= c;
            populationWorkers += c;
        }

        public void delta()
        {
            eat += d_eat;
            myO2 += d_myO2;
            myWater += d_myWater;
            populationFree += d_populdation;
            wood += d_wood;

            if (eat < 0) eat = 0;
            if (myO2 < 0) myO2 = 0;
            if (myWater < 0) myWater = 0;
            if (populationFree < 0) populationFree = 0;
            if (wood < 0) wood = 0;

            if (eat > Building.limitResource) eat = Building.limitResource;
            if (myO2 > Building.limitResource) myO2 = Building.limitResource;
            if (myWater > Building.limitResource) myWater = Building.limitResource;
            if (populationFree + populationWorkers > populationLimit) populationFree = populationLimit - populationWorkers;
            if (wood > Building.limitResource) wood = Building.limitResource;
        }
    }



}
