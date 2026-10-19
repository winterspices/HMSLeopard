using HarmonyLib;
using Leopard.Controllers;
using LeopardBridge;
using UnityEngine;

namespace Leopard.BepPatches
{
    [HarmonyPatch(typeof(GPButtonRopeWinch), "AttachToController")]
    public class Patch_AttachToController
    {
        // custom logic for moving the attachment point for angle winches
        public static bool Prefix(GPButtonRopeWinch __instance, RopeController controller, ref LineRenderer ___ropeEffect)
        {
            if (__instance.description.Contains("custom"))
            {
                //string transform = __instance.description.Substring(__instance.description.IndexOf(' ') + 1);
                __instance.rope = controller;
                controller.transform.position = __instance.GetComponent<SailAttachment>().att.transform.position;

                controller.transform.parent = __instance.transform.parent;
                ___ropeEffect = __instance.rope.GetComponent<LineRenderer>();
                controller.UpdateSailAttachment();

                Patches.ship.transform.Find("boat leopard/structure_container/BOWSPRIT/ROPES/rope_foremast_port").GetComponent<BowspritRopes>().Setup();
                Patches.ship.transform.Find("boat leopard/structure_container/BOWSPRIT/ROPES/rope_foremast_starboard").GetComponent<BowspritRopes>().Setup();

                Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/starboard pulley").GetComponent<PulleyController>().Setup();
                Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/port pulley").GetComponent<PulleyController>().Setup();

                return false;
            }

            return true;
        }
    }
}
