using HarmonyLib;
using LeopardBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(GPButtonRopeWinch), "AttachToController")]
    public class Patch_AttachToController
    {
        // custom logic for moving the attachment point for angle winches
        public static bool Prefix(GPButtonRopeWinch __instance, RopeController controller, ref LineRenderer ___ropeEffect)
        {
            if (__instance.description.Contains("custom"))
            {
                string transform = __instance.description.Substring(__instance.description.IndexOf(' ') + 1);

                __instance.rope = controller;
                controller.transform.position = __instance.GetComponent<SailAttachment>().att.transform.position;

                controller.transform.parent = __instance.transform.parent;
                ___ropeEffect = __instance.rope.GetComponent<LineRenderer>();
                controller.UpdateSailAttachment();

                return false;
            }

            return true;
        }
    }
}
