using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class DestructibleDoor : MonoBehaviour
    {
        public int health = 30;
        private MeshDestroy meshDestroy;

        // Start is called before the first frame update
        void Start()
        {
            meshDestroy = GetComponent<MeshDestroy>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DestroyMesh()
        {
            meshDestroy.DestroyMesh();
        }
    }
}