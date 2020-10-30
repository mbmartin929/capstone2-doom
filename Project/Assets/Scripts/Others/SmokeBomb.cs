using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class SmokeBomb : MonoBehaviour
    {
        public GameObject enemyGo;
        public bool chasePlayerOnSpawn = false;

        public float spawnTime;

        public float smokeBombStartTime;

        public float duration = 0.5f;
        public Vector3 startingScale;
        public Vector3 targetScale;
        public float startingSize = 0f;
        public float targetSize = 1f;

        public bool isArena = false;

        // Start is called before the first frame update
        void Start()
        {
            transform.localScale = startingScale;

            //StartCoroutine(ChangeScale(startingSize, targetSize, duration));
            Invoke("StartScale", smokeBombStartTime);
            Invoke("SpawnEnemy", spawnTime);
            //enemyGo.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void StartScale()
        {
            StartCoroutine(ChangeScale(startingSize, targetSize, duration));
        }

        private IEnumerator ChangeScale(float v_start, float v_end, float duration)
        {
            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                transform.localScale = new Vector3(Mathf.Lerp(v_start, v_end, elapsed / duration), Mathf.Lerp(v_start, v_end, elapsed / duration), Mathf.Lerp(v_start, v_end, elapsed / duration));

                elapsed += Time.deltaTime;

                if (transform.localScale == targetScale)
                {
                    Invoke("ResetScale", 2.0f);
                }

                yield return null;
            }

            transform.localScale = new Vector3(v_end, v_end, v_end);
        }

        private void ResetScale()
        {
            float resetDuration = duration - 0.4f;
            StartCoroutine(ChangeScale(targetSize, startingSize, resetDuration));
            Destroy(gameObject, resetDuration);
        }

        private void SpawnEnemy()
        {
            GameObject _enemyGo = Instantiate(enemyGo, transform.position, transform.rotation);

            if (!isArena) _enemyGo.transform.parent = null;
            else if (isArena)
            {
                _enemyGo.transform.parent = transform.parent;
                //Debug.Log(_enemyGo.GetComponent<EnemyAI>().enemyController.maxHealth);
                //transform.parent.GetComponent<WaveManger>().totalHealth += _enemyGo.GetComponent<EnemyAI>().enemyController.maxHealth;
            }

            if (chasePlayerOnSpawn)
            {
                _enemyGo.GetComponent<EnemyAI>().ChasePlayer();
            }

        }

        private void EnableEnemy()
        {
            enemyGo.SetActive(true);
            enemyGo.transform.parent = null;
        }
    }
}