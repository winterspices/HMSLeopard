using System.Collections;
using UnityEngine;

namespace Leopard.Controllers
{
    public class LeopardBellInteract : GoPointerButton
    {
        private AudioSource audio;

        private void Awake()
        {
            audio = GetComponent<AudioSource>();

            if (!audio)
            {
                Debug.LogError("[BellInteract] No audio source found!");
            }

            Collider col = GetComponent<Collider>();

            if (col != null && !col.isTrigger)
            {
                col.isTrigger = true;
            }
        }

        public override void OnActivate(GoPointer activatingPointer)
        {
            if (audio)
            {
                audio.Play();
                StartCoroutine(Swing());
            }
        }

        private void Update()
        {

        }

        IEnumerator Swing()
        {
            float t = 0f;

            while (t < 3f)
            {
                t += Time.deltaTime;

                float angle = 30 * Mathf.Sin(6 * t) * ((3 - t) / 3);
                transform.localRotation = Quaternion.Euler(0f, angle, 0f);

                yield return null;
            }

            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
