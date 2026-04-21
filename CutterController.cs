using UnityEngine;

namespace Leopard
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

            // set the cutters position
            pos = ship.position;
            rotation = ship.rotation;
            azimuth = ship.eulerAngles.y;

            float x = Mathf.Cos(azimuth * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin((azimuth - 180f) * Mathf.Deg2Rad) * radius;


            cutter.transform.SetPositionAndRotation(new Vector3(pos.x + x, pos.y, pos.z + z), rotation);

            // hide the cutter on the decks
            ship.Find("boat leopard/structure_container/Wooden Rowboat").gameObject.SetActive(false);
            ship.Find("boat leopard/structure_container/rowboat rope").gameObject.SetActive(true);
        }

        public void EnableCutter()
        {

        }
    }
}
