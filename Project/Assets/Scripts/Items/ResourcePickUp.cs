using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class ResourcePickUp : PickUpController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (itemName == "Resource")
        {
            if (CheckCloseToTag("Player", distanceToPickUp))
            {
                if (fraction < 1)
                {
                    fraction += lerpSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, target, fraction);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerWeapons = other.GetComponent<PlayerController>().weapons;

            ResourceManager.Instance.curResources += recoverAmount;

            Destroy(gameObject, 0f);
        }
    }
}
