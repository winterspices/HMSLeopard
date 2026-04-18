using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(Shipyard), "DischargeShip")]
    public class Patch_DischargeShip
    {
        public static void Prefix(Shipyard __instance, GameObject ___currentShip)
        {
            GameObject obj = GameObject.Find("shipyard Al'Ankh/ship release pos");

            if (___currentShip.name == "BOAT LEOPARD (207)(Clone)")
            {
                obj.transform.localPosition = new Vector3(-46.8f, -9.85f, -1.1f);
            } else
            {
                obj.transform.localPosition = new Vector3(13.9f, -9.85f, -0.8f);
            }
        }
    }
}
