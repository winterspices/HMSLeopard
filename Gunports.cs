using System.Collections.Generic;
using UnityEngine;

namespace Leopard
{
    public static class Gunports
    {
        public static void Setup()
        {
            Transform boat = GameObject.Find("BOAT LEOPARD (207)(Clone)/boat leopard").transform;
            string name;

            for (int i = 0; i < boat.childCount; i++)
            {
                name = boat.GetChild(i).name;

                if (name.Contains("gunport"))
                {
                    if (boat.GetChild(i).name.Contains("lower")) lowerGunports.Add(boat.GetChild(i));
                    if (boat.GetChild(i).name.Contains("upper")) upperGunports.Add(boat.GetChild(i));
                    if (boat.GetChild(i).name.Contains("quarter")) quarterGunports.Add(boat.GetChild(i));
                }
            }

            boat = GameObject.Find("BOAT LEOPARD (207)(Clone)").transform;

            overflows.Add(boat.Find("overflow particles lower 1").gameObject);
            overflows.Add(boat.Find("overflow particles lower 2").gameObject);
            overflows.Add(boat.Find("overflow particles lower 3").gameObject);
            overflows.Add(boat.Find("overflow particles lower 4").gameObject);
            overflows.Add(boat.Find("overflow particles upper 1").gameObject);
            overflows.Add(boat.Find("overflow particles upper 2").gameObject);
            overflows.Add(boat.Find("overflow particles upper 3").gameObject);
            overflows.Add(boat.Find("overflow particles upper 4").gameObject);
            overflows.Add(boat.Find("overflow particles upper 5").gameObject);
        }

        public static void ToggleOverflows()
        {
            foreach (GameObject overflow in overflows)
            {
                overflow.SetActive(!overflow.activeSelf);
            }
        }

        public static void ToggleAudio(string gameObject)
        {
            Transform trigger = GameObject.Find("BOAT LEOPARD (207)(Clone)/boat leopard/structure_container").transform.Find(gameObject);
            bool open = false;

            switch (gameObject)
            {
                case "interior trigger 2":
                    open = Gunports.lowerGunports[0].GetComponent<GPButtonTrapdoor>().IsOpen();
                    break;
                case "interior trigger 3":
                    open = Gunports.upperGunports[0].GetComponent<GPButtonTrapdoor>().IsOpen();
                    break;
                default:
                    break;
            }

            trigger.gameObject.SetActive(!open);

            if (open)
            {
                AudioMixers.instance.outdoorSnapshot.TransitionTo(2f);
            }
            else
            {
                AudioMixers.instance.indoorSnapshot.TransitionTo(2f);
            }
        }

        public static bool recursive;
        public static List<Transform> lowerGunports = new List<Transform>();
        public static List<Transform> upperGunports = new List<Transform>();
        public static List<Transform> quarterGunports = new List<Transform>();
        public static List<GameObject> overflows = new List<GameObject>();
    }
}
