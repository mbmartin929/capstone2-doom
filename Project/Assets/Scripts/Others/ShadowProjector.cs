using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class ShadowProjector : MonoBehaviour
    {
        public bool isEnabled = false;
        public float offset = 1f;

        private EnemyController enemyController;

        // Start is called before the first frame update
        void Start()
        {
            enemyController = transform.parent.GetChild(0).GetComponent<EnemyController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (enemyController.IsDead()) Destroy(gameObject);

            if (isEnabled)
            {
                RaycastHit hit;
                int layerMask = LayerMask.GetMask("Ground");

                if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10f, layerMask))
                {
                    Vector3 offsetPoint = Vector3.Lerp(hit.point, transform.position, offset);
                    transform.position = offsetPoint;

                    //StartCoroutine(GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, 1, 1.0f, 0));
                }
            }
        }
    }
}
