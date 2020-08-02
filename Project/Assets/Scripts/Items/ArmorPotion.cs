using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
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

                TextManager.Instance.UpdateHealthArmorText();

                Destroy(this.gameObject);
            }
        }
    }
}
