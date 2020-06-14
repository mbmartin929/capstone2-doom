using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : PickUpController
{
    [SerializeField]
    private int healAmountArmor;


     void Start()
    {
        PickUpController Armor = new PickUpController();
        Armor.itemName = "Armor";
        Armor.healAmount = healAmountArmor;
    }

}
