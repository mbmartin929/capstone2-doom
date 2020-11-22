using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
    public class ArenaManager : MonoBehaviour
    {
        public GameObject[] waves;

        public int activeWaveID = 0;

        public GameObject exitGo;

        private void Start()
        {
            Debug.Log(name);
        }

        public IEnumerator SpawnWaves(float time)
        {
            Debug.Log("Going to spawn wave");
            yield return new WaitForSeconds(time);

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

            if (exitGo.GetComponent<DoorScript>() != null) exitGo.GetComponent<DoorScript>().ChangeDoorState(true);
            else
            {
                exitGo.GetComponent<Animator>().SetTrigger("Open Gate");
            }

            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FinishArena());
            ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Find the exit", 0.029f, 0f));
        }
    }
}
