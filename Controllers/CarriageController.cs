using LeopardBridge;
using System.Collections;
using UnityEngine;

namespace Leopard.Controllers
{
    public class CarriageController : GoPointerButton
    {
        Transform cannon;
        Transform deck;
        GPButtonTrapdoor gunport;

        bool ready;
        bool moving;
        bool port;

        private void Awake()
        {
            cannon = transform.parent;
            deck = cannon.parent.parent;
            
            if (GetComponent<CarriageGunport>().gunport != null)
            {
                gunport = GetComponent<CarriageGunport>().gunport.GetComponent<GPButtonTrapdoor>();
            }

            port = transform.parent.parent.name.Contains("port");
        }

        public override void OnActivate()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(MoveAll());
            }
            else
            {
                Move();
            }
        }

        public void Move()
        {
            ready = GetComponentInParent<CannonController>().ready;

            if (!moving && !cannon.GetComponent<CannonController>().firing && (gunport == null || gunport.IsOpen()))
            {
                if (!ready)
                {
                    StartCoroutine(Push());
                    moving = true;
                }
                else
                {
                    StartCoroutine(Pull());
                    moving = true;
                }
            }
        }

        IEnumerator MoveAll()
        {
            foreach (Transform child in deck)
            {
                if (child.name.Contains("port") == port)
                {
                    CarriageController carriage = child.Find("cannon/carriage").GetComponent<CarriageController>();

                    if (!carriage.moving)
                    {
                        carriage.Move();

                        yield return null;
                    }
                }
            }
        }

        IEnumerator Push()
        {
            Vector3 stowed = new Vector3(0f, port ? 0.3f : -0.3f, 0f);
            Vector3 pulled = new Vector3(0f, port ? 1.1f : -1.1f, 0f);
            Vector3 pushed = new Vector3(0f, 0f, 0f);

            Quaternion rotated = Quaternion.Euler(0f, 0f, -90f);
            Quaternion normal = Quaternion.Euler(0f, 0f, port ? 0f : 180f);

            Transform walk = Patches.embarkLeopard.transform.Find($"structure_container/CANNONS/{deck.name}/{transform.parent.parent.name}/cannon");


            // pull it out
            float t = 0f;

            while (t < 1.5f)
            {
                t += Time.deltaTime;

                cannon.localPosition = Vector3.Lerp(stowed, pulled, t / 1.5f);
                walk.localPosition = Vector3.Lerp(stowed, pulled, t / 1.5f);

                yield return null;
            }

            // rotate the son of a bitch
            t = 0f;

            while (t < 1.5f)
            {
                t += Time.deltaTime;

                cannon.localRotation = Quaternion.Lerp(rotated, normal, t / 1.5f);
                walk.localRotation = Quaternion.Lerp(rotated, normal, t / 1.5f);

                yield return null;
            }

            // push out the gunport
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;

                cannon.localPosition = Vector3.Lerp(pulled, pushed, t / 1f);
                walk.localPosition = Vector3.Lerp(pulled, pushed, t / 1f);

                yield return null;
            }

            cannon.GetComponent<CannonController>().ready = true;
            moving = false;
        }

        IEnumerator Pull()
        {
            Vector3 stowed = new Vector3(0f, port ? 0.3f : -0.3f, 0f);
            Vector3 pulled = new Vector3(0f, port ? 1.1f : -1.1f, 0f);
            Vector3 pushed = new Vector3(0f, 0f, 0f);

            Quaternion rotated = Quaternion.Euler(0f, 0f, -90f);
            Quaternion normal = Quaternion.Euler(0f, 0f, port ? 0f : 180f);

            Transform walk = Patches.embarkLeopard.transform.Find($"structure_container/CANNONS/{deck.name}/{transform.parent.parent.name}/cannon");

            cannon.GetComponent<CannonController>().ready = false;

            // pull it out
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;

                cannon.localPosition = Vector3.Lerp(pushed, pulled, t / 1f);
                walk.localPosition = Vector3.Lerp(pushed, pulled, t / 1f);

                yield return null;
            }

            // rotate her
            t = 0f;

            while (t < 1.5f)
            {
                t += Time.deltaTime;

                cannon.localRotation = Quaternion.Lerp(normal, rotated, t / 1.5f);
                walk.localRotation = Quaternion.Lerp(normal, rotated, t / 1.5f);

                yield return null;
            }

            // stow her
            t = 0f;

            while (t < 1.5f)
            {
                t += Time.deltaTime;

                cannon.localPosition = Vector3.Lerp(pulled, stowed, t / 1.5f);
                walk.localPosition = Vector3.Lerp(pulled, stowed, t / 1.5f);

                yield return null;
            }

            moving = false;
        }
    }
}
