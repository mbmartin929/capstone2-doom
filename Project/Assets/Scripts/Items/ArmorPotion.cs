using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class ArmorPotion : PickUpController
{
    public int numberOfArmor = 0;

    void Start()
    {
        PickUpController Armor = new PickUpController();
        playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = ambientSound;
        audioSource.Play();

        Armor.itemName = "Armor";

        EndGameScreen.Instance.totalArmor += numberOfArmor;
    }

    void LateUpdate()
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

    private void OnTriggerEnter(Collider other)
    {
        HealthArmorPickUp(other);

        if (other.CompareTag("Player") && numberOfArmor >= 1)
        {
            EndGameScreen.Instance.armorFound++;
        }
    }
}

