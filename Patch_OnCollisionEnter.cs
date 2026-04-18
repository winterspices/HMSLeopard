using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(Anchor), "OnCollisionEnter")]
    public class Patch_OnCollisionEnter
    {
        private static bool Prefix(Anchor __instance, Collision collision, ref bool ___grounded)
        {
            if (collision.collider.CompareTag("Terrain") || collision.collider.CompareTag("OceanBottom"))
            {
                ___grounded = true;
            }

            return false;
        }
    }
}
