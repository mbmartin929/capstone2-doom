using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBlock : MonoBehaviour
{
    public int health = 290;

    public int resourceDrops;
    public GameObject[] resourceGos;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DropResources();
            Destroy(gameObject);
        }
    }

    public void DropResources()
    {
        for (int i = 0; i <= resourceDrops; i++)
        {
            int random = Random.Range(0, resourceGos.Length);

            //Instantiate
        }
    }
}
