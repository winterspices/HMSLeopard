using HarmonyLib;
using Leopard.Controllers;
using LeopardBridge;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(GPButtonTrapdoor), "OnActivate")]
    public class GunportController
    {
        // game logic for opening and closing gunports
        public static bool Prefix(GPButtonTrapdoor __instance)
        {
            // prevent recursive method calling
            if (Gunports.recursive)
            {
                return true;
            }

            if (__instance.name.Contains("gunport"))
            {
                // gunport was clicked, toggle all gunports
                Gunports.recursive = true;

                if (__instance.name.Contains("lower"))
                {
                    // check if any cannons are sticking out
                    Transform deck = Patches.ship.transform.Find("boat leopard/structure_container/CANNONS/lower");

                    foreach (Transform cannon in deck)
                    {
                        if (cannon.Find("cannon").GetComponent<CannonController>().ready)
                        {
                            Gunports.recursive = false;
                            return false;
                        }
                    }

                    // no cannons, let's close up
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
                    bool open = Gunports.lowerGunports[0].GetComponent<GPButtonTrapdoor>().IsOpen();
                    GameObject mask1 = Patches.ship.transform.Find("boat leopard/mask water half").gameObject;
                    mask1.SetActive(open);

                    GameObject mask2 = Patches.ship.transform.Find("boat leopard/mask water full").gameObject;
                    mask2.SetActive(!open);

                }
                else if (__instance.name.Contains("upper"))
                {
                    // check if the first two and last two cannons are sticking out
                    Transform deck = Patches.ship.transform.Find("boat leopard/structure_container/CANNONS/upper");

                    foreach (Transform cannon in deck)
                    {
                        bool hasGunport = cannon.Find("cannon/carriage").GetComponent<CarriageGunport>().gunport;
                        if (hasGunport && cannon.Find("cannon").GetComponent<CannonController>().ready)
                        {
                            Gunports.recursive = false;
                            return false;
                        }
                    }


                    // no cannons, lets close up
                    foreach (Transform gunport in Gunports.upperGunports)
                    {
                        if (gunport.name != __instance.name)
                        {
                            gunport.GetComponent<GPButtonTrapdoor>().OnActivate();
                        }
                    }

                    // toggle the forecastle interior trigger
                    Gunports.ToggleAudio("interior trigger 3");

                }
                else if (__instance.name.Contains("quarter"))
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

            return true;
        }
    }
}
