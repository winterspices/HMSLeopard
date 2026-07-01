using HarmonyLib;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(Anchor))]
    public class Patch_Anchor
    {
        [HarmonyPatch(nameof(Anchor.RegisterRopeController))]
        [HarmonyPostfix]
        public static void RegisterRopeControllerPostfix(Anchor __instance, ref float ___unsetResistance, ref float ___resistancePullLimit)
        {
            ___unsetResistance *= 0.1f;
            ___resistancePullLimit = ___unsetResistance;

        }

        [HarmonyPatch(nameof(Anchor.OnLoad))]
        [HarmonyPrefix]
        public static bool OnLoadPrefix(Anchor __instance, ref RopeControllerAnchor ___rope)
        {
            if (__instance.transform.name == "leopard anchor")
            {
                float distance = Vector3.Distance(GameObject.Find("BOAT LEOPARD (207)(Clone)/boat leopard/anchor default").transform.position, __instance.transform.position);
                Debug.LogWarning(distance);
                float num = Mathf.InverseLerp(0f, ___rope.maxLength, distance);

                if (num < 0.05f) num = 0f;
                Debug.LogWarning(num);
                ___rope.currentLength = num;

                return false;
            }

            return true;
        }
    }
}
