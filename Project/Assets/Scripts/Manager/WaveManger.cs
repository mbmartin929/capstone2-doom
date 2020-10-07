using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class WaveManger : MonoBehaviour
    {
        public ArenaManager arenaManager;
        public bool lastWave = true;
        public int totalHealth;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // foreach (Transform child in transform)
            // {
            //     int health = child.GetComponent<EnemyAI>().enemyController.CurHealth;

            //     totalHealth += health;
            // }

            if (transform.childCount == 0)
            {
                arenaManager.activeWaveID += 1;
                //StartCoroutine(arenaManager.SpawnWaves(1.69f));
                arenaManager.StartCoroutine(arenaManager.SpawnWaves(1.69f));
                Debug.Log(gameObject.name + " is empty");
                Debug.Log("Next wave is: " + arenaManager.activeWaveID);
                Destroy(gameObject, 0.0f);
            }
        }
    }
}
