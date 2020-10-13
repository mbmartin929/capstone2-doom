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

        public float exitOpenTime = 1.5f;
        public GameObject exitGo;

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
                StartCoroutine(OpenExit(1.5f));
            }
            else
            {
                Debug.Log("Next Wave:" + activeWaveID);
                waves[activeWaveID].SetActive(true);
                Debug.Log("Activating Wave: " + waves[activeWaveID].name);
            }
        }

        public IEnumerator OpenExit(float time)
        {
            Debug.Log("Open Exit Door");

            yield return new WaitForSeconds(time);
            exitGo.GetComponent<DoorScript>().ChangeDoorState(true);

            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FinishArena());
            ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Find the exit", 0.029f));
        }
    }
}
