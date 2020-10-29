using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpawnAfterCollision : MonoBehaviour
{
    public bool isEnabled = false;

    public GameObject[] dialogueColliderGo;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in dialogueColliderGo)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spawned DialogueCollider after Collision");
            foreach (var item in dialogueColliderGo)
            {
                item.SetActive(true);
            }

        }
    }
}
