using LeopardBridge;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leopard.Controllers
{
    public class CannonController : GoPointerButton
    {
        private Vector3 originalPos;
        float recoilDist = 1.2f;

        ParticleSystem smoke;
        ParticleSystem flash;
        ParticleSystem barrel;
        AudioSource audio;

        public bool firing;
        public bool ready;
        bool port;

        float min = 0.1f;
        float max = 0.45f;

        Transform deck;

        private void Awake()
        {
            CannonParticles refs = GetComponent<CannonParticles>();
            smoke = refs.smoke;
            flash = refs.flash;
            barrel = refs.barrelSmoke;
            audio = GetComponent<AudioSource>();
            deck = transform.parent.parent;
            port = transform.parent.name.Contains("port");
        }

        public override void OnActivate()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(FireAll());
            }
            else
            {
                Fire();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Fire them all!");

                StartCoroutine(FireAll());
            }
        }

        public void Fire()
        {
            if (firing)
            {
                return;
            }

            if (!ready)
            {
                return;
            }

            firing = true;

            audio.pitch = Random.Range(0.9f, 1.1f);
            audio.volume = Random.Range(0.9f, 1f);
            audio.Play();
            flash.Play();
            smoke.Play();
            barrel.Play();

            StartCoroutine(Recoil());
        }

        IEnumerator FireAll()
        {
            Transform cannons = Patches.ship.transform.Find("boat leopard/structure_container/CANNONS");
            List<string> order = new List<string>();
            string side = port ? "port" : "starboard";
            order.Add($"upper/{side} 1");
            order.Add($"lower/{side} 1");
            order.Add($"upper/{side} 2");
            order.Add($"lower/{side} 2");
            order.Add($"upper/{side} 3");
            order.Add($"lower/{side} 3");
            order.Add($"upper/{side} 4");
            order.Add($"lower/{side} 4");
            order.Add($"upper/{side} 5");
            order.Add($"lower/{side} 5");
            order.Add($"upper/{side} 6");
            order.Add($"lower/{side} 6");
            order.Add($"upper/{side} 7");
            order.Add($"lower/{side} 7");
            order.Add($"upper/{side} 8");
            order.Add($"lower/{side} 8");
            order.Add($"upper/{side} 9");
            order.Add($"lower/{side} 9");
            order.Add($"quarter/{side} 1");
            order.Add($"upper/{side} 10");
            order.Add($"lower/{side} 10");
            order.Add($"quarter/{side} 2");
            order.Add($"upper/{side} 11");
            order.Add($"lower/{side} 11");
            order.Add($"quarter/{side} 3");
            order.Add($"upper/{side} 12");

            foreach (string gun in order)
            {
                CannonController cannon = cannons.Find(gun).Find("cannon").GetComponent<CannonController>();

                if (cannon.ready)
                {
                    cannon.Fire();

                    yield return new WaitForSeconds(Random.Range(min, max));
                }
            }
        }

        public IEnumerator Recoil()
        {
            originalPos = transform.localPosition;
            Vector3 recoilPos = originalPos + transform.forward * recoilDist * (port ? 1 : -1);

            Transform walk = Patches.embarkLeopard.transform.Find($"structure_container/CANNONS/{deck.name}/{transform.parent.name}/cannon");

            // fast recoil
            float t = 0f;

            while (t < 0.08f)
            {
                t += Time.deltaTime;

                transform.localPosition = Vector3.Lerp(originalPos, recoilPos, t / 0.08f);
                walk.localPosition = Vector3.Lerp(originalPos, recoilPos, t / 0.08f);

                yield return null;
            }

            // wait
            yield return new WaitForSeconds(0.7f);

            // slow return
            t = 0f;

            while (t < 1.5f)
            {
                t += Time.deltaTime;

                transform.localPosition = Vector3.Lerp(recoilPos, originalPos, t / 1.5f);
                walk.localPosition = Vector3.Lerp(recoilPos, originalPos, t / 1.5f);

                yield return null;
            }

            transform.localPosition = originalPos;
            walk.localPosition = originalPos;
            firing = false;
        }
    }
}
