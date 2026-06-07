using HarmonyLib;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(Anchor), "SetAnchor")]
    public class Patch_SetAnchor
    {
        public static bool Prefix(Anchor __instance, ref Rigidbody ___body, AudioSource ___audio, ref ConfigurableJoint ___joint)
        {
            Debug.Log("Setting anchor...");
            ___body.drag = __instance.anchorDrag;
            
            
            if (___audio)
            {
                ___audio.enabled = true;
                ___audio.pitch = 1f;
                ___audio.Play();
            }

            return false;
        }
    }
}
