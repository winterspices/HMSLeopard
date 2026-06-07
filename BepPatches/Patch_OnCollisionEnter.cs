using HarmonyLib;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(Anchor), "OnCollisionEnter")]
    public class Patch_OnCollisionEnter
    {
        private static bool Prefix(Anchor __instance, Collision collision, ref bool ___grounded, ref ConfigurableJoint ___joint)
        {
            if ((collision.collider.CompareTag("Terrain") || collision.collider.CompareTag("OceanBottom")) && ___joint.linearLimit.limit > 1f)
            {
                ___grounded = true;
            }

            return false;
        }
    }
}
