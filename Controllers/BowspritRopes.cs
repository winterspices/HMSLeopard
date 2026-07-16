using UnityEngine;

namespace Leopard.Controllers
{
    public class BowspritRopes : MonoBehaviour
    {
        public void PlaceholderName()
        {
            GameObject mast = GameObject.Find("BOAT LEOPARD (207)(Clone)/boat leopard/structure_container/FOREMAST/foremast");

            if (mast.GetComponent<Mast>().sails.Count > 0)
            {
                if (name.Contains("port"))
                {
                    Transform left = mast.GetComponent<Mast>().sails[0].transform.GetComponent<SailConnections>().angleControllerLeft.GetComponent<RopeEffect>().attachment;/*Find("rope controller angle left").GetComponent<RopeEffect>().attachment;*/
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
