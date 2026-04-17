using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(BoatCamera), "UpdateZoom")]
    class Patch_UpdateZoom
    {
        static bool Prefix(BoatCamera __instance)
        {
            FieldInfo zoomField = AccessTools.Field(__instance.GetType(), "zoomLevel");
            FieldInfo speedField = AccessTools.Field(__instance.GetType(), "zoomSpeed");

            float zoomLevel = (float)zoomField.GetValue(__instance);
            float multiplier = Mathf.InverseLerp(-8f, -80f, zoomLevel) * 40f + 10f;
            float newSpeed = GameInput.GetScrollAxis() * multiplier;


            // Equivalent of:
            // zoomLevel += GameInput.GetScrollAxis() * zoomSpeed;
            zoomField.SetValue(__instance, zoomLevel + newSpeed);

            // Set min and max zoom
            if (zoomLevel > -8f)
            {
                zoomField.SetValue(__instance, -8f);
            }
            else if (zoomLevel < -80f)
            {
                zoomField.SetValue(__instance, -80f);
            }

            return false;
        }
    }
}
