using BepInEx;
using Crest;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Leopard
{
    [BepInPlugin("com.winter.leopard", "HMS Leopard", "1.4.1")]
    [BepInDependency("com.nandbrew.shipyardexpansion")]
    [BepInDependency("com.nandbrew.sailcollisionfix")]
    public class LeopardPatcher : BaseUnityPlugin
    {
        public const string pluginGuid = "com.winter.leopard";
        public const string pluginName = "HMS Leopard";
        public const string pluginVersion = "1.4.1";

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.winter.leopard");
        }

        public void Update()
        {

        }
    }
}
