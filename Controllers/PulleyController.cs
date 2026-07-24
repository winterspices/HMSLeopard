using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leopard.Controllers
{
    public class PulleyController : MonoBehaviour
    {
        public float lastDistance;
        public float distanceCheck = 0.5f;
        public bool setup;


        public GameObject leftPulley;
        public GameObject rightPulley;
        public GameObject mast;
        public GameObject sail;

        public Transform leftAtt;
        public Transform rightAtt;
        public Transform leftWinchAtt;
        public Transform rightWinchAtt;

        public void Awake()
        {
            leftPulley = Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/port pulley").gameObject;
            rightPulley = Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/starboard pulley").gameObject;
            mast = Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/mainmast").gameObject;
        }

        public void Setup()
        {
            Debug.LogWarning("[Leopard] Setting up pulley system");
            List<GameObject> sails = mast.GetComponent<Mast>().sails;

            if (sails.Count > 0)
            {
                sail = sails[0];
                leftAtt = sail.transform.GetComponent<SailConnections>().angleControllerLeft.GetComponent<RopeEffect>().attachment;
                rightAtt = sail.transform.GetComponent<SailConnections>().angleControllerRight.GetComponent<RopeEffect>().attachment;

                leftWinchAtt = Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/ATT/port att winch").transform;
                rightWinchAtt = Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/ATT/starboard att winch").transform;

                lastDistance = 0f;

                setup = true;
            }
            else
            {
                setup = false;

                leftPulley.SetActive(false);
                rightPulley.SetActive(false);

                Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/port pulley/port att").gameObject.SetActive(false);
                Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/starboard pulley/starboard att").gameObject.SetActive(false);
                Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/port pulley strap").gameObject.SetActive(false);
                Patches.ship.transform.Find("boat leopard/structure_container/MAINMAST/starboard pulley strap").gameObject.SetActive(false);
            }
        }

        public void Update()
        {
            if (setup)
            {
                float distance = Vector3.Distance(rightAtt.position, rightPulley.transform.position);

                if (Mathf.Abs(distance - lastDistance) > distanceCheck)
                {
                    // calculate right pulley
                    Vector3 ab = rightWinchAtt.position - rightPulley.transform.position;
                    Vector3 bc = rightAtt.position - rightPulley.transform.position;

                    Vector3 bisector = (ab.normalized + bc.normalized).normalized;
                    Vector3 normal = Vector3.Cross(ab, bc).normalized;

                    Quaternion rotation = Quaternion.LookRotation(normal, bisector);

                    rightPulley.transform.rotation = rotation;
                    sail.transform.GetComponent<SailConnections>().angleControllerRight.transform.position = rightPulley.transform.Find("starboard att").position;

                    // calculate left pulley
                    ab = leftWinchAtt.position - leftPulley.transform.position;
                    bc = leftAtt.position - leftPulley.transform.position;

                    bisector = (ab.normalized + bc.normalized).normalized;
                    normal = Vector3.Cross(ab, bc).normalized;

                    rotation = Quaternion.LookRotation(normal, bisector);

                    leftPulley.transform.rotation = rotation;
                    sail.transform.GetComponent<SailConnections>().angleControllerLeft.transform.position = leftPulley.transform.Find("port att").position;

                    lastDistance = distance;
                }
            }
        }
    }
}
