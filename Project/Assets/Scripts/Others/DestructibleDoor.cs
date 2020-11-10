using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class DestructibleDoor : MonoBehaviour
    {
        public int health = 30;

        private MeshDestroy meshDestroy;
        private GameObject rockParticles;

        public int secretAmount = 0;

        // Start is called before the first frame update
        void Start()
        {
            EndGameScreen.Instance.totalSecrets += secretAmount;
            //meshDestroy = transform.GetChild(1).GetComponent<MeshDestroy>();
            rockParticles = transform.GetChild(2).gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DestroyMesh()
        {
            // meshDestroy.gameObject.SetActive(true);
            // meshDestroy.transform.parent = null;
            // meshDestroy.DestroyMesh();

            Destroy(transform.GetChild(1).gameObject);

            rockParticles.SetActive(true);
            rockParticles.transform.parent = null;

            EndGameScreen.Instance.totalSecretsFound += secretAmount;

            Destroy(gameObject);
        }
    }
}