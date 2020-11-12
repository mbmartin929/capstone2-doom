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

        public bool movePodium = false;

        public float gameTitleTime = 3.5f;

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
            if (GameManager.Instance.level == 1)
            {
                foreach (GameObject item in disabledGos)
                {
                    Destroy(item, 0f);
                }

                movePodium = true;

                entranceDoor.SetActive(true);

                StartCoroutine(GameTitle(gameTitleTime));
            }
        }

        private IEnumerator GameTitle(float time)
        {
            MusicManager.Instance.FadeOutAmbientMusicCaller();
            MusicManager.Instance.FadeInActiveMusicCaller(4, false, 2);

            //gameTitle.SetActive(true);
            yield return new WaitForSeconds(time);
            //gameTitle.SetActive(false);
            StartCoroutine(arenaManager.SpawnWaves(0.5f));
        }
    }
}