using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : PickUpController
{
    [SerializeField]
    private int healAmountArmor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            healArmor(healAmountArmor);
            Debug.Log("Armor PICKED UP! " + healAmountArmor);
            Destroy(this.gameObject);
        }
    }

}
