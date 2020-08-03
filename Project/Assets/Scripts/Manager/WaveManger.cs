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

        public bool done = false;

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

            if (transform.childCount == 0 && !done)
            {
                arenaManager.activeWaveID++;
                StartCoroutine(arenaManager.SpawnWaves(0.5f));
                Destroy(gameObject, 1.0f);
                done = true;
            }
        }
    }
}
