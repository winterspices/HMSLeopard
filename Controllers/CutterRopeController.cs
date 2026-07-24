using UnityEngine;

namespace Leopard.Controllers
{
    public class CutterRopeController : GoPointerButton
    {
        private Transform ship;
        private GameObject cutter;

        private void Awake()
        {
            ship = GameObject.Find("BOAT LEOPARD (207)(Clone)").transform;
        }

        public override void OnActivate()
        {
            cutter = Patches.boat;

            if (cutter.transform.Find("boat cutter").transform.childCount > 4)
            {
                Debug.Log("Items left on the cutter!");
                return;
            }

            //if (Vector3.Distance(cutter.transform.localPosition, ship.localPosition) > 15)
            //{
            //    Debug.Log("Rowboat too far away!");
            //    return;
            //}

            if (ship.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1.5f)
            {
                Debug.Log("Leopard is moving too fast to capture boat");
                return;
            }


            Debug.Log("Placing cutter back onboard");

            DisableCutter();

            // place cutter back on board
            ship.Find("boat leopard/structure_container/Wooden Rowboat").gameObject.SetActive(true);
            ship.Find("boat leopard/structure_container/rowboat rope").gameObject.SetActive(false);
        }

        public void DisableCutter()
        {
            cutter.SetActive(false);
            Patches.cutterActive = false;
        }
    }
}
