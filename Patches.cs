using BepInEx;
using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Leopard
{
    internal class Patches
    {
        public static GameObject ship;
        public static GameObject leopard;
        public static GameObject embarkLeopard;

        public static bool leopardInstalled;

        [HarmonyPatch(typeof(FloatingOriginManager))]
        public static class FloatingOriginManagerPatches
        {
            [HarmonyPrefix]
            [HarmonyPatch("Start")]
            public static void StartPatch(FloatingOriginManager __instance)
            {
                if(__instance.name == "_shifting world")
                {
                    LoadAssets();
                    
                    if (leopard)
                    {
                        try
                        {
                            ship = UnityEngine.Object.Instantiate<GameObject>(leopard, __instance.transform);
                            embarkLeopard = GameObject.Find("WALK boat leopard");

                            Transform transform = GameObject.Find("walk cols").transform;

                            MatLib.RegisterMaterials();

                            embarkLeopard.transform.SetParent(transform);
                            embarkLeopard.transform.localPosition = new Vector3(-300f, 0f, 100f);

                            ship.transform.Find("boat leopard").transform.Find("mask water half").GetComponent<MeshRenderer>().material = MatLib.convexHull;
                            ship.transform.Find("boat leopard").transform.Find("mask water full").GetComponent<MeshRenderer>().material = MatLib.convexHull;
                            ship.transform.Find("WaterObjectInteractionSphereBack").GetComponent<MeshRenderer>().material = MatLib.objectInteraction;
                            ship.transform.Find("WaterObjectInteractionSphereFront").GetComponent<MeshRenderer>().material = MatLib.objectInteraction;
                            ship.transform.Find("water foam").GetComponent<MeshRenderer>().material = MatLib.foam;
                            ship.transform.Find("boat leopard").transform.Find("damage_water").GetComponent<MeshRenderer>().material = MatLib.water4;
                            ship.transform.Find("boat leopard/mask splash").GetComponent<MeshRenderer>().material = MatLib.splash;

                            // add bell script
                            Transform bell = ship.transform.Find("boat leopard/structure_container/bell");

                            if (bell)
                            {
                                AudioSource audio = bell.GetComponent<AudioSource>();
                                Collider col = bell.GetComponent<Collider>();

                                if (audio && col)
                                {
                                    bell.gameObject.AddComponent<LeopardBellInteract>();
                                    Debug.LogWarning("Bell script added");
                                } else
                                {
                                    Debug.LogWarning("Not col and bell");
                                }
                            } else
                            {
                                Debug.LogWarning("not bell");
                            }

                            Gunports.Setup();

                            //foreach (GameObject overflow in Gunports.overflows)
                            //{
                            //    overflow.GetComponent<MeshRenderer>().material = MatLib.particleSplash;
                            //}

                            Debug.LogWarning("Leopard loaded");
                        } catch (Exception e)
                        {
                            Debug.LogError($"Could not load Leopard due to exception:\n{e}");
                        }
                    } else
                    {
                        Debug.LogError("Could not load Leopard due to prefab not loading correctly. Did you assign the leopard prefab to an asset bundle in Unity?");
                    }
                } 
                else
                {
                    GameState.currentBoat = null;
                    GameState.lastBoat = null;
                }
            }
        }

        private static void LoadAssets()
        {
            string path = Paths.PluginPath + "\\Leopard";

            // load .dll file `LeopardBridge.dll`
            if (File.Exists(path + "\\LeopardBridge.dll"))
            {
                Assembly.LoadFrom(path + "\\LeopardBridge.dll");
            }

            // load assetbundle
            if (!File.Exists(path + "\\leopard"))
            {
                Debug.LogError("Leopard not installed correctly");
                leopardInstalled = false;
            } else
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(path + "\\leopard");
                string prefab = "Assets/Leopard/BOAT LEOPARD (207).prefab";
                leopard = (bundle.LoadAsset(prefab) as GameObject);
                leopardInstalled = true;
            }
        }
    }
}