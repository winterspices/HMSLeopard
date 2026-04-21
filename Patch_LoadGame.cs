using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(SaveLoadManager), "LoadGame")]
    public class Patch_LoadGame
    {
        private static void Postfix(SaveLoadManager __instance, int backupIndex)
        {
            float distance = Vector3.Distance(Patches.boat.transform.localPosition, new Vector3(-500000f, 0f, -500000f));
            Debug.LogWarning(Patches.boat.transform.localPosition);
            Transform ship = GameObject.Find("BOAT LEOPARD (207)(Clone)").transform;

            // cutter is active
            if (distance > 50000f)
            {
                Debug.LogWarning(distance);
                ship.Find("boat leopard/structure_container/Wooden Rowboat").gameObject.SetActive(false);
                ship.Find("boat leopard/structure_container/rowboat rope").gameObject.SetActive(true);
            }
        }
    }
}
