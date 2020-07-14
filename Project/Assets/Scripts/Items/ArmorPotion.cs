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
        Armor.recoverAmount = HealAmountArmor;
        unit = player.GetComponent<UnitController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            unit.maxArmor += recoverAmount;
            Debug.Log("Armor PICKED!" + recoverAmount);
            Destroy(this.gameObject);
        }
    }
}
