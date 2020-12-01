using EightDirectionalSpriteSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public int hp = 15;
    public int eggCount;

    public bool isObjective = false;
    public bool isEnd = false;

    public GameObject[] enemySpawns;
    public float spawnTime = 1.29f;

    public GameObject[] bloodSplashGos;

    public int minNumberBabySpawns;
    public int maxNumberBabySpawns;
    public GameObject babyWormGo;

    private void Start()
    {
        //ObjectiveManager.Instance.targetNumber++;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0) Die();
        foreach (GameObject item in bloodSplashGos)
        {
            if (item.tag == "Hit Normal")
            {
                GameObject bloodGo = Instantiate(item, transform.position, Quaternion.identity);
                //bloodGo.transform.parent = hit.transform;
            }
            else
            {
                GameObject bloodGo = Instantiate(item, transform.position, item.transform.rotation);
                //bloodGo.transform.parent = hit.transform;
            }
        }

        if (isEnd)
        {
            if (!DialogueAssistant.Instance.playBadEnd)
            {
                DialogueAssistant.Instance.playBadEnd = true;

                DialogueAssistant.Instance.StopAllCoroutines();
                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.BadEndDialogue1());
                isEnd = false;

                GameManager.Instance.trapDoor.SetActive(true);
            }
            else { }
        }
    }

    private void Die()
    {
        if (eggCount != 0)
        {
            Debug.Log("Die Egg");
            int random = Random.Range(minNumberBabySpawns, maxNumberBabySpawns);

            for (int i = 0; i < random; i++)
            {
                Debug.Log("Spawning Baby Worms from Egg");
                Vector3 newPos = (Vector3)Random.insideUnitCircle * 1.14f + transform.position;
                GameObject babyWorm = Instantiate(babyWormGo, newPos, Quaternion.identity);
            }
            SpawnEnemies();
            ObjectiveManager.Instance.currentNumber += eggCount;
            ObjectiveManager.Instance.UpdateTargetNumberObjective();
            Destroy(gameObject);
        }

        eggCount = 0;
    }

    private void SpawnEnemies()
    {
        foreach (GameObject enemy in enemySpawns)
        {
            enemy.SetActive(true);
        }
    }
}
