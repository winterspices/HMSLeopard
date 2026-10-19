using UnityEngine;

namespace Leopard.Controllers
{
    public class BowspritRopes : MonoBehaviour
    {
        public void Setup()
        {
            GameObject mast = Patches.ship.transform.Find("boat leopard/structure_container/FOREMAST/foremast").gameObject;

            if (mast.GetComponent<Mast>().sails.Count > 0)
            {
                if (name.Contains("port"))
                {
                    Transform left = mast.GetComponent<Mast>().sails[0].transform.GetComponent<SailConnections>().angleControllerLeft.GetComponent<RopeEffect>().attachment;
                    GetComponent<RopeEffect>().attachment = left;
                }
                else if (name.Contains("starboard"))
                {
                    Transform right = mast.GetComponent<Mast>().sails[0].transform.GetComponent<SailConnections>().angleControllerRight.GetComponent<RopeEffect>().attachment;
                    GetComponent<RopeEffect>().attachment = right;
                }
            }
        }
    }
}
