using UnityEngine;

namespace Leopard
{
    public class MatLib
    {
        public static void RegisterMaterials()
        {
            convexHull = GameObject.Find("BOAT medi medium (50)").transform.Find("medi medium new").transform.Find("structure_container").transform.Find("mask").GetComponent<MeshRenderer>().material;
            foam = GameObject.Find("BOAT medi medium (50)/WaterFoam (1)").GetComponent<MeshRenderer>().material;
            objectInteraction = GameObject.Find("BOAT medi medium (50)").transform.Find("WaterObjectInteractionSphereBack").GetComponent<MeshRenderer>().material;
            water4 = GameObject.Find("BOAT medi medium (50)").transform.Find("medi medium new").transform.Find("damage_water").GetComponent<MeshRenderer>().material;
            splash = GameObject.Find("BOAT medi medium (50)/medi medium new/structure_container/mask_splash").GetComponent<MeshRenderer>().material;
            //particleSplash = GameObject.Find("BOAT medi medium (50)").transform.Find("overflow particles (2)").GetComponent<MeshRenderer>().material;
            //sail = GameObject.Find("")
        }

        public static Material convexHull;
        public static Material foam;
        public static Material objectInteraction;
        public static Material water4;
        public static Material splash;
        public static Material particleSplash;
        public static Material sail;
    }
}
