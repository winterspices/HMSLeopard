using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace Leopard.Controllers
{
    public class CutterController : GoPointerButton
    {
        private Transform ship;
        private GameObject cutter;

        private Vector3 pos;
        private Quaternion rotation;
        private float azimuth;
        private float radius = 10f;

        private void Awake()
        {
            ship = GameObject.Find("BOAT LEOPARD (207)(Clone)").transform;
        }

        public override void OnActivate(GoPointer activatingPointer)
        {
            if (ship.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1.5f)
            {
                Debug.Log("Leopard is moving too fast to deploy boat");
                return;
            }

            Debug.Log("Setting cutter");

            cutter = Patches.boat;
            cutter.SetActive(true);
            Patches.cutterActive = true;

            // set the cutters position
            pos = ship.position;
            rotation = ship.rotation;
            azimuth = ship.eulerAngles.y;

            float x = Mathf.Cos(azimuth * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin((azimuth - 180f) * Mathf.Deg2Rad) * radius;

            cutter.transform.SetPositionAndRotation(new Vector3(pos.x + x, pos.y, pos.z + z), rotation);

            // refresh the boat instantly
            BoatHorizon instance = cutter.transform.Find("boat cutter").GetComponent<BoatHorizon>();
            FieldInfo field = AccessTools.Field(typeof(BoatHorizon), "updateCooldown");
            field.SetValue(instance, 0f);

            // hide the cutter on the decks
            ship.Find("boat leopard/structure_container/Wooden Rowboat").gameObject.SetActive(false);
            ship.Find("boat leopard/structure_container/rowboat rope").gameObject.SetActive(true);
        }
    }
}
