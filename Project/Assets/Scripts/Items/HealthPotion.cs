using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : PickUpController
{

    private int HealAmountHp;

    void Start()
    {
        PickUpController Potion = new PickUpController();
        Potion.itemName = "Heart";
        Potion.healAmount = HealAmountHp;
        unit = player.GetComponent<UnitController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            overlayImage.enabled = true;
            unit.CurHealth += healAmount;
            Debug.Log("HP PICKED!" + healAmount);
            Destroy(this.gameObject);
        }

    }




}




