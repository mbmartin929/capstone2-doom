using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject player;
    protected UnitController unit;
    public string itemName;
    public int healAmount;
    private void Start()
    {
        unit = player.GetComponent<UnitController>();
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
