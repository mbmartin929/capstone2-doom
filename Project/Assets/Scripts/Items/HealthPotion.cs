using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
            unit.CurHealth += healAmount;
            Debug.Log("HP PICKED!" + healAmount);
            Destroy(this.gameObject);

        }
    }


}
