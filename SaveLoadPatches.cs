using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(SaveLoadManager))]
    public class SaveLoadPatches
    {
        [HarmonyPatch("SaveModData")]
        [HarmonyPostfix]
        public static void SavePatch()
        {
            KeyValuePair<string, string> entry = new KeyValuePair<string, string>(LeopardPatcher.pluginGuid, Patches.cutterActive.ToString());

            if (GameState.modData.ContainsKey(entry.Key)) GameState.modData[entry.Key] = entry.Value;
            else GameState.modData.Add(entry.Key, entry.Value);
        }

        [HarmonyPatch("LoadModData")]
        [HarmonyPostfix]
        public static void LoadPatch()
        {
            if (GameState.modData.TryGetValue(LeopardPatcher.pluginGuid, out string value)) Patches.cutterActive = bool.Parse(value);
        }

        [HarmonyPatch("LoadGame")]
        [HarmonyPostfix]
        public static void LoadGamePatch()
        {
            Transform ship = GameObject.Find("BOAT LEOPARD (207)(Clone)").transform;

            // cutter is active
            if (Patches.cutterActive)
            {
                //Debug.LogWarning(distance);
                ship.Find("boat leopard/structure_container/Wooden Rowboat").gameObject.SetActive(false);
                ship.Find("boat leopard/structure_container/rowboat rope").gameObject.SetActive(true);
            }
            else
            {
                Patches.boat.SetActive(false);
            }
        }
    }
}
