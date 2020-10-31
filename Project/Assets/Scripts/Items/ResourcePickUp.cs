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

            GameObject pickUpSFX = new GameObject();
            GameObject _pickUpSFX = Instantiate(pickUpSFX, transform.position, Quaternion.identity);
            _pickUpSFX.name = "PickUp SFX";

            _pickUpSFX.AddComponent<AudioSource>();
            _pickUpSFX.GetComponent<AudioSource>().priority = 29;
            _pickUpSFX.GetComponent<AudioSource>().volume = 0.5f;
            _pickUpSFX.GetComponent<AudioSource>().PlayOneShot(pickUpSound);

            Destroy(_pickUpSFX, 2.9f);

            Debug.Log("Pick up Ammo");

            PickUpOverlayManager.Instance.AmmoOverlay();

            ResourceManager.Instance.curResources += recoverAmount;

            Destroy(gameObject, 0f);
        }
    }
}
