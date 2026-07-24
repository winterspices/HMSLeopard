using HarmonyLib;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(InteriorEffectsTrigger), "OnTriggerExit")]
    public class Patch_OnTriggerExit
    {
        public static bool Prefix(InteriorEffectsTrigger __instance, Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collider player = GameObject.Find("OVRPlayerController (observer)").GetComponent<Collider>();
                if (__instance.name == "interior trigger 2")
                {
                    Collider interior = GameObject.Find("BOAT LEOPARD (207)(Clone)/boat leopard/structure_container/interior trigger 1").GetComponent<Collider>();

                    if (player.bounds.Intersects(interior.bounds))
                    {
                        // do nothing
                        return false;
                    }
                }
                else if (__instance.name == "interior trigger 1")
                {
                    Collider interior = GameObject.Find("BOAT LEOPARD (207)(Clone)/boat leopard/structure_container/interior trigger 2").GetComponent<Collider>();

                    if (player.bounds.Intersects(interior.bounds))
                    {
                        // do nothing
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
