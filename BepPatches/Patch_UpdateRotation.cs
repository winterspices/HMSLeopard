using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(ShipyardSailColChecker), "OnTriggerEnter")]
    public class Patch_UpdateRotation
    {
        public static void Postfix(ShipyardSailInstaller __instance, ref Sail ___sail)
        {
            try
            {
                Transform boat = ___sail.transform.parent.parent.parent.parent.parent;
                Transform mast = ___sail.transform.parent;
                Debug.LogWarning($"[Leopard] Boat: {boat.name}");

                if (boat.name == "BOAT LEOPARD (207)(Clone)")
                {
                    if (___sail.category == SailCategory.square)
                    {
                        ___sail.minAngle = -60f;
                        ___sail.maxAngle = 60f;

                        Debug.LogWarning($"[Leopard] Found square sail: {___sail.name}");

                        return;
                    }
                    else if (___sail.category == SailCategory.gaff & mast.name == "mizzen mast")
                    {
                        ___sail.minAngle = -22f;
                        ___sail.maxAngle = 22f;

                        Debug.LogWarning($"[Leopard] Found gaff sail on mizzen mast: {___sail.name}");

                        return;
                    }
                }
            }
            catch
            {

            }

        }
    }
}
