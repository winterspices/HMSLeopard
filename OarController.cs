using cakeslice;
using UnityEngine;

namespace Leopard
{
    public class OarController : GoPointerButton
    {
        private Rigidbody rigidbody;
        public float forceAmount = 1000f;
        public float turnForce = 3f;

        private GameObject leftOar;
        private GameObject rightOar;
        private GameObject paddles;

        private bool recursive;

        private float time = 0f;
        private float timeIncrease = 3f;
        private float forwardAngle = 30f;
        private float upAngle = 20f;

        private Outline outline;

        private void Awake()
        {
            rigidbody = Patches.boat.GetComponent<Rigidbody>();
            leftOar = GameObject.Find("BOAT CUTTER (212)(Clone)/boat cutter/structure_container/hull/oar left");
            rightOar = GameObject.Find("BOAT CUTTER (212)(Clone)/boat cutter/structure_container/hull/oar right");
            paddles = GameObject.Find("BOAT CUTTER (212)(Clone)/boat cutter/structure_container/hull/paddles");

            leftOar.GetComponent<MeshRenderer>().enabled = false;
            rightOar.GetComponent<MeshRenderer>().enabled = false;

            outline = base.gameObject.GetComponent<Outline>();
        }

        public override void OnActivate(GoPointer activatingPointer)
        {
            StickyClick(activatingPointer);

            SetOars(true);
            recursive = false;

            outline.meshFilter = null;
        }

        public override void OnUnactivate(GoPointer activatingPointer)
        {
            MouseLook.ToggleMouseLook(true);
        }

        public override void ExtraLateUpdate()
        {
            if (stickyClickedBy || isClicked)
            {
                if (GameInput.GetKey(InputName.MoveUp))
                {
                    rigidbody.AddForce(rigidbody.transform.forward * forceAmount, ForceMode.Force);

                    time += Time.deltaTime * timeIncrease;

                    float zAngle = Mathf.Sin(time) * forwardAngle;
                    float xAngle = -(Mathf.Sin(time + 1.5f) * upAngle);

                    leftOar.transform.localRotation = Quaternion.Euler(xAngle, 0f, zAngle);
                    rightOar.transform.localRotation = Quaternion.Euler(-xAngle, 0f, -zAngle);
                }
                
                if (GameInput.GetKey(InputName.MoveDown))
                {
                    rigidbody.AddForce(-rigidbody.transform.forward * forceAmount, ForceMode.Force);

                    time -= Time.deltaTime * timeIncrease;

                    float zAngle = Mathf.Sin(time) * forwardAngle;
                    float xAngle = -(Mathf.Sin(time + 1.5f) * upAngle);

                    leftOar.transform.localRotation = Quaternion.Euler(xAngle, 0f, zAngle);
                    rightOar.transform.localRotation = Quaternion.Euler(-xAngle, 0f, -zAngle);
                }
                
                // add code for dealing with left/right and backwards
                if (GameInput.GetKey(InputName.MoveLeft))
                {
                    rigidbody.AddTorque(Vector3.up * -forceAmount * turnForce, ForceMode.Force);

                    if (!GameInput.GetKey(InputName.MoveUp) && !GameInput.GetKey(InputName.MoveDown)) time += Time.deltaTime * timeIncrease;

                    float zAngle = Mathf.Sin(time) * forwardAngle;
                    float xAngle = -(Mathf.Sin(time + 1.5f) * upAngle);

                    leftOar.transform.localRotation = Quaternion.Euler(-xAngle, 0f, zAngle);
                    rightOar.transform.localRotation = Quaternion.Euler(-xAngle, 0f, -zAngle);
                }
                
                if (GameInput.GetKey(InputName.MoveRight))
                {
                    rigidbody.AddTorque(Vector3.up * forceAmount * turnForce, ForceMode.Force);

                    if (!GameInput.GetKey(InputName.MoveUp) && !GameInput.GetKey(InputName.MoveDown)) time += Time.deltaTime * timeIncrease;

                    float zAngle = Mathf.Sin(time) * forwardAngle;
                    float xAngle = -(Mathf.Sin(time + 1.5f) * upAngle);

                    leftOar.transform.localRotation = Quaternion.Euler(xAngle, 0f, zAngle);
                    rightOar.transform.localRotation = Quaternion.Euler(xAngle, 0f, -zAngle);
                }

            } else
            {
                if (!recursive)
                {
                    SetOars(false);
                    recursive = true;
                }
            }
        }

        private void SetOars(bool set)
        {
            Debug.Log($"Setting oars to {set}");

            // hide the dynamics and set the static to active
            paddles.GetComponent<MeshRenderer>().enabled = !set;
            leftOar.GetComponent<MeshRenderer>().enabled = set;
            rightOar.GetComponent<MeshRenderer>().enabled = set;
            //paddles.SetActive(!set);
            //leftOar.SetActive(set);
            //rightOar.SetActive(set);
        }
    }
}
