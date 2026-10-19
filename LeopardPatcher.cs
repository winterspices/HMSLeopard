using BepInEx;
using Crest;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace Leopard
{
    [BepInPlugin("com.winter.leopard", "HMS Leopard", "1.6.2")]
    [BepInDependency("com.nandbrew.shipyardexpansion")]
    [BepInDependency("com.nandbrew.sailcollisionfix")]
    [BepInIncompatibility("com.Aquilartz.OldChronian")]
    public class LeopardPatcher : BaseUnityPlugin
    {
        public const string pluginGuid = "com.winter.leopard";
        public const string pluginName = "HMS Leopard";
        public const string pluginVersion = "1.6.2";

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.winter.leopard");
        }

        public void Update()
        {
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    Vector3 vector = Patches.ship.GetComponent<Rigidbody>().velocity;
            //    float drag = Patches.ship.GetComponent<BoatProbes>().addedHullDrag;

            //    Debug.Log($"Forward speed: {Vector3.Dot(transform.forward, -vector):F2}");
            //    Debug.Log($"Applied drag: {Vector3.Dot(transform.forward, -vector) * drag}");
            //    Debug.Log($"Added hull drag: {drag}");
            //}
        }
    }
}
