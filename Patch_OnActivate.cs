using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(GPButtonTrapdoor), "OnActivate")]
    public class Patch_OnActivate
    {
        // game logic for opening and closing gunports
        public static void Prefix(GPButtonTrapdoor __instance)
        {
            // prevent recursive method calling
            if (Gunports.recursive)
            {
                return;
            }

            if (__instance.name.Contains("gunport"))
            {
                // gunport was clicked, toggle all gunports
                Gunports.recursive = true;

                if (__instance.name.Contains("lower"))
                {
                    foreach (Transform gunport in Gunports.lowerGunports)
                    {
                        if (gunport.name != __instance.name)
                        {
                            gunport.GetComponent<GPButtonTrapdoor>().OnActivate();
                        }
                    }

                    // toggle the upper and lower overflows
                    Gunports.ToggleOverflows();

                    // toggle the lower deck interior trigger
                    Gunports.ToggleAudio("interior trigger 2");

                    // toggle the lower deck water mask
                    GameObject mask1 = Patches.ship.transform.Find("boat leopard/mask water half").gameObject;
                    mask1.SetActive(!mask1.activeSelf);

                    GameObject mask2 = Patches.ship.transform.Find("boat leopard/mask water full").gameObject;
                    mask2.SetActive(!mask2.activeSelf);

                } else if (__instance.name.Contains("upper"))
                {
                    foreach (Transform gunport in Gunports.upperGunports)
                    {
                        if (gunport.name != __instance.name)
                        {
                            gunport.GetComponent<GPButtonTrapdoor>().OnActivate();
                        }
                    }

                    // toggle the forecastle interior trigger
                    Gunports.ToggleAudio("interior trigger 3");
                    
                } else if (__instance.name.Contains("quarter"))
                {
                    foreach (Transform gunport in Gunports.quarterGunports)
                    {
                        if (gunport.name != __instance.name)
                        {
                            gunport.GetComponent<GPButtonTrapdoor>().OnActivate();
                        }
                    }
                }

                Gunports.recursive = false;
            }
        }
    }
}
