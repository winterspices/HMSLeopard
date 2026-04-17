using UnityEngine;

namespace Leopard
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
            }
        }

        private void Update()
        {

        }
    }
}
