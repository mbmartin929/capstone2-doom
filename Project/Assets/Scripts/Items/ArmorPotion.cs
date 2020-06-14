using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : PickUpController
{

    private int HealAmountArmor;


     void Start()
    {
        PickUpController Armor = new PickUpController();
        Armor.itemName = "Armor";
        Armor.healAmount = HealAmountArmor;
        unit = player.GetComponent<UnitController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            unit.maxArmor += healAmount;
            Debug.Log("Armor PICKED!" + healAmount);
            Destroy(this.gameObject);
        }
    }
}
