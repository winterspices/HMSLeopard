using HarmonyLib;
using System;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(Shipyard), "DischargeShip")]
    public class Patch_DischargeShip
    {
        public static void Prefix(Shipyard __instance, GameObject ___currentShip)
        {
            try
            {
                GameObject obj = GameObject.Find("shipyard Al'Ankh/ship release pos");
                Shipyard shipyard = GameState.currentShipyard;

                if (shipyard.transform.name == "shipyard Al'Ankh")
                {
                    if (___currentShip.name == "BOAT LEOPARD (207)(Clone)")
                    {
                        obj.transform.localPosition = new Vector3(-46.8f, -9.85f, -1.1f);
                    }
                    else
                    {
                        obj.transform.localPosition = new Vector3(13.9f, -9.85f, -0.8f);
                    }
                }
            } catch (Exception)
            {
                // probably not al ankh
                return;
            }
            
        }
    }
}
