using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class IntroManager : MonoBehaviour
    {
        // Instantiates Singleton
        public static IntroManager Instance { set; get; }

        public GameObject[] disabledGos;
        public GameObject entranceDoor;

        public bool introEnabled = true;
        public bool movePodium = false;

        public float gameTitleTime = 3.5f;
        public GameObject gameTitle;

        public ArenaManager arenaManager;

        void Awake()
        {
            // Sets Singleton
            Instance = this;

            if (Instance == this) Debug.Log("IntroManager Singleton Initialized");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InstantiateTitle()
        {
            //Debug.Log("Ravage");
            foreach (GameObject item in disabledGos)
            {
                Destroy(item, 0f);
            }

            movePodium = true;

            entranceDoor.SetActive(true);

            StartCoroutine(GameTitle(gameTitleTime));
        }

        private IEnumerator GameTitle(float time)
        {
            gameTitle.SetActive(true);
            yield return new WaitForSeconds(time);
            gameTitle.SetActive(false);

            StartCoroutine(arenaManager.StartSpawn1(0.5f));
            StartCoroutine(arenaManager.SpawnWaves(0.5f));
        }
    }
}