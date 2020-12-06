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
        public ArenaManager endArenaManager;

        [SerializeField] private bool level3Gate = false;

        private Animator anim;

        void Awake()
        {
            // Sets Singleton
            Instance = this;

            if (Instance == this) Debug.Log("IntroManager Singleton Initialized");

            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            if (GameManager.Instance.level == 3) GameManager.Instance.trapDoor.SetActive(false);
        }

        public void InstantiateTitle()
        {
            Debug.Log("Intro Manager Lvl: " + GameManager.Instance.level);
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
            else if (GameManager.Instance.level == 3)
            {
                if (level3Gate)
                {
                    anim.SetTrigger("Open Door");

                    StartCoroutine(OpenBigGate(4.2f));
                }
            }
        }

        public void EndArena()
        {
            Debug.Log("End Arena");

            if (GameManager.Instance.level == 3)
            {
                // GameManager.Instance.isEnding = true;

                // GameManager.Instance.trapDoor.SetActive(true);

                Debug.Log("Changing Music");
                MusicManager.Instance.FadeOutAmbientMusicCaller();
                MusicManager.Instance.FadeInActiveMusicCaller(4, false, 2);

                Debug.Log("Spawning First Wave");

                StartCoroutine(endArenaManager.SpawnWaves(1.69f));
            }
        }

        private IEnumerator OpenBigGate(float time)
        {
            yield return new WaitForSeconds(time);

            Debug.Log("Changing Music");
            MusicManager.Instance.FadeOutAmbientMusicCaller();
            MusicManager.Instance.FadeInActiveMusicCaller(4, false, 2);

            Debug.Log("Spawning First Wave");

            StartCoroutine(arenaManager.SpawnWaves(0.69f));
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