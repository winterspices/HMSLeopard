using HarmonyLib;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(Sail), "ApplyForce")]
    public class Patch_ApplyForce
    {
        public static bool Prefix(Sail __instance, float ___unamplifiedForwardForce, float ___unamplifiedSidewayForce, Transform ___windcenter, ref float ___outFinalForwardForce)
        {
            if (__instance.shipRigidbody.name == "BOAT LEOPARD (207)(Clone)")
            {
                float num = __instance.GetRealSailPower();

                if (__instance.category == SailCategory.junk)
                {
                    num *= 0.75f;
                }

                if (__instance.category == SailCategory.gaff)
                {
                    num *= 0.85f;
                }

                if (__instance.category == SailCategory.staysail)
                {
                    num *= 0.7f;
                }

                float num2 = 250f;
                float d = 1.5f;

                __instance.shipRigidbody.AddForceAtPosition(__instance.shipRigidbody.transform.forward * ___unamplifiedForwardForce * num * num2, ___windcenter.position, ForceMode.Force);
                __instance.shipRigidbody.AddForceAtPosition(__instance.shipRigidbody.transform.right * ___unamplifiedSidewayForce * num * num2 * d, ___windcenter.position, ForceMode.Force);
                ___outFinalForwardForce = ___unamplifiedForwardForce * num2 * num;

                return false;
            }

            return true;
        }
    }
}
