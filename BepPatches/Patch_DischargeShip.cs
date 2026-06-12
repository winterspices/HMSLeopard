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

                Shipyard shipyard = GameState.currentShipyard;

                // GRC
                if (shipyard.transform.name == "shipyard Al'Ankh")
                {
                    GameObject releasePos = GameObject.Find("shipyard Al'Ankh/ship release pos");

                    if (___currentShip.name == "BOAT LEOPARD (207)(Clone)")
                    {
                        releasePos.transform.localPosition = new Vector3(-46.8f, -9.85f, -1.1f);
                    }
                    else
                    {
                        releasePos.transform.localPosition = new Vector3(13.9f, -9.85f, -0.8f);
                    }
                }
                // Aestrin
                else if (shipyard.transform.name == "shipyard Aestrin")
                {
                    GameObject releasePos = GameObject.Find("shipyard Aestrin/ship release pos");

                    if (___currentShip.name == "BOAT LEOPARD (207)(Clone)")
                    {
                        releasePos.transform.localPosition = new Vector3(-26.8f, -12.4f, -8.1f);
                    }
                    else
                    {
                        releasePos.transform.localPosition = new Vector3(-6.6f, -12.4f, 18.4f);
                    }
                }
            }
            catch (Exception)
            {
                // probably not al ankh
                return;
            }

        }
    }
}
