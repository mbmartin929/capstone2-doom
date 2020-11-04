using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBlock : MonoBehaviour
{
    public int health = 290;

    public int minResourceDrops = 1;
    public int maxResourceDrops = 6;

    public GameObject[] resourceGos;
    public GameObject[] crystalEffects;

    private int deathCount = 0;

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
        if (deathCount == 0)
        {
            Debug.Log("Drop Resources");

            int amount = Random.Range(minResourceDrops, maxResourceDrops);

            for (int i = 0; i <= amount; i++)
            {
                int index = Random.Range(0, resourceGos.Length);

                GameObject itemDrop = Instantiate(resourceGos[index], transform.position, Quaternion.identity);
                itemDrop.GetComponent<ItemExplosion>().isExplode = true;
            }

            deathCount++;

            // for (int i = 0; i <= amount; i++)
            // {
            //     index = Random.Range(0, drops.Length - 1);

            //     GameObject itemDrop = Instantiate(drops[index], transform.position, Quaternion.identity);
            //     itemDrop.GetComponent<ItemExplosion>().isExplode = true;
            // }
        }
    }
}
