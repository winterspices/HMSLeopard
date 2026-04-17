using HarmonyLib;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Leopard
{
    [HarmonyPatch(typeof(BinaryFormatter), "Deserialize", new Type[] { typeof(Stream) })]
    class Patch_Deserialize
    {
        static void Postfix(object __result)
        {
            //if (__result is SaveContainer saveContainer)
            //{
            //    Debug.LogWarning($"Save loaded: {saveContainer}");

            //    Debug.LogWarning($"Wind: {saveContainer.wind.x}, {saveContainer.wind.y}, {saveContainer.wind.z}");

            //    foreach (SaveObjectData saveObjectData in saveContainer.savedObjects)
            //    {
            //        if (saveObjectData.sceneIndex == 207)
            //        {
            //            Debug.LogWarning($"Saved object: {saveObjectData.sceneIndex}");

            //            Debug.LogWarning($"Sails count: {saveObjectData.customization.sails.Count}");

            //            foreach (SaveSailData sail in saveObjectData.customization.sails)
            //            {
            //                Debug.LogWarning(sail.prefabIndex);
            //            }

            //            Debug.LogWarning($"Masts count: {saveObjectData.customization.masts.Length}");
                        
            //            foreach (bool mast in saveObjectData.customization.masts)
            //            {
            //                Debug.LogWarning(mast);
            //            }

            //            Debug.LogWarning($"Active part options: {saveObjectData.customization.partActiveOptions}");

            //            foreach (int option in saveObjectData.customization.partActiveOptions)
            //            {
            //                Debug.LogWarning(option);
            //            }
            //        }
            //    }
            //}
        }
    }
}
