using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class AfterMeshDestroy : MonoBehaviour
    {
        public float shrinkTime = 1.0f;
        public float shrinkSpeed = 2.0f;

        private MeshCollider collider;
        private float curScale;

        private bool shrinking = false;
        private Vector3 targetScale = new Vector3(0, 0, 0);

        // Start is called before the first frame update
        void Start()
        {
            shrinking = false;
            collider = GetComponent<MeshCollider>();

            StartCoroutine(DisableCollider());
            StartCoroutine(StartShrink());

            Destroy(gameObject, 5.0f);
        }

        // Update is called once per frame
        void Update()
        {
            // curScale = Mathf.MoveTowards(curScale, 1.0f, Time.deltaTime * shrinkSpeed);
            // gameObject.transform.localScale = new Vector3(curScale, curScale, curScale);

            if (shrinking)
            {
                transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;

                if (transform.localScale.x < 0) shrinking = false;
            }
        }

        public IEnumerator StartShrink()
        {
            yield return new WaitForSeconds(1.0f);
            shrinking = true;
        }

        public IEnumerator DisableCollider()
        {
            yield return new WaitForSeconds(shrinkTime);

            collider.enabled = false;
        }
    }
}
