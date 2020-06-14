using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUpController
{
    [SerializeField]
    protected int healAmountHp;

     void Start()
    {
        PickUpController Potion = new PickUpController();
        Potion.itemName = "Heart";
        Potion.healAmount = healAmountHp;
    }

 
}
