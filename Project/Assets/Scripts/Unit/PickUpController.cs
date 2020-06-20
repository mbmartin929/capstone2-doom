using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    public Image overlayImage;
    public GameObject player;
    protected UnitController unit;
    public string itemName;
    public int healAmount;
    

    private void Start()
    {
        unit = player.GetComponent<UnitController>();
        overlayImage.enabled = false;
    }

    public void Update()
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
