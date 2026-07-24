using Crest;
using System.Collections;
using UnityEngine;

namespace Leopard.Controllers
{
    public class FoamController : MonoBehaviour
    {
        private SampleHeightHelper helper;
        private float waterHeight;

        Vector3 pos;
        Vector3 newpos;

        float extraHeight = 0.3f;

        private void Awake()
        {
            helper = new SampleHeightHelper();
        }

        public void Update()
        {
            // every half second adjust the height of the foam at the front
            StartCoroutine(Float());
        }

        private IEnumerator Float()
        {
            while (true)
            {
                // do work here
                pos = transform.position;

                helper.Init(pos);
                helper.Sample(out waterHeight);

                newpos = transform.position;
                newpos.y = waterHeight + extraHeight;
                transform.position = newpos;


                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
