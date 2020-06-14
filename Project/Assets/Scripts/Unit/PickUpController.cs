using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public string itemName;
    public int healAmount;
    private void Start()
    {
        
    }

    public string ItemName
    {
        get { return itemName; }

        set
        {
            itemName = value;       
        }
    }

    public int HealAmount
    {
        get { return healAmount; }
        set
        {
            healAmount = value;
        }
    }
  



}
