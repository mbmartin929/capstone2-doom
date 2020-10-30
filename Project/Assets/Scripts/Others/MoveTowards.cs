using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class MoveTowards : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (IntroManager.Instance.movePodium)
            {
                transform.position += Vector3.down * Time.deltaTime;

                Destroy(gameObject, 0.0f);
            }
        }
    }
}
