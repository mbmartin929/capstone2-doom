using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
    public class ArenaManager : MonoBehaviour
    {
        public GameObject[] spawns1;
        public GameObject[] waves;

        public int activeWaveID = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator StartSpawn1(float time)
        {

            yield return new WaitForSeconds(time);

            // foreach (GameObject spawn in spawns1)
            // {
            //     spawn.SetActive(true);
            // }
        }

        public IEnumerator SpawnWaves(float time)
        {

            yield return new WaitForSeconds(time);

            // for (int i = 0; i < waves.Length; i++)
            // {

            //     waves[i].SetActive(true);
            // }

            if (activeWaveID >= waves.Length)
            {
                Debug.Log("Finished all waves");
            }
            else waves[activeWaveID].SetActive(true);
        }
    }
}
