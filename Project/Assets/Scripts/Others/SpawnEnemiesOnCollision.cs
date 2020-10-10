using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesOnCollision : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Payload"))
        {
            Debug.Log("Spawn from Tag");

            SpawnEnemies();

            Destroy(gameObject, 0f);
        }

        if (other.name == "Payload")
        {
            Debug.Log("Spawn from Name");

            SpawnEnemies();

            Destroy(gameObject, 0f);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Spawn from Tag");

            SpawnEnemies();

            Destroy(gameObject, 0f);
        }
    }
}
