using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(ClothRope), "UpdateRope")]
    public class Patch_UpdateRope
    {
		// fix rope size
        public static bool Prefix(ClothRope __instance)
        {
			for (int i = 0; i < __instance.bones.Length; i++)
			{
				__instance.bones[i].position = __instance.ropeEffect.allRopeSections[i];
				if (i > 0)
				{
					__instance.bones[i].LookAt(__instance.bones[i - 1]);
					__instance.bones[i].Rotate(Vector3.up, -90f, Space.Self);
				}
				if (__instance.ropeEffect.ropeWidth < 0.05f)
				{
					__instance.bones[i].localScale = Vector3.one * __instance.ropeEffect.ropeWidth * 30f;
				}

				__instance.bones[i].localScale = Vector3.one * __instance.ropeEffect.ropeWidth * 30f;
			}

			return false;
        }
    }
}
