using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class IntroCollider : MonoBehaviour
    {
        public bool isEnd = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isEnd)
                {
                    IntroManager.Instance.EndArena();
                }
                else IntroManager.Instance.InstantiateTitle();


                Destroy(gameObject);
            }
        }
    }
}
