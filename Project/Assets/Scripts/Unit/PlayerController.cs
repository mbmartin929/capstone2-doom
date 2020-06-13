using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public GameObject player;
    protected UnitController unit;
    [SerializeField]
    private int HealthPotionAmount;
    private int ArmorPotionAmount;
    // Start is called before the first frame update
    void Start()
    {
        unit = player.GetComponent<UnitController>();
     
     
    }

    // Update is called once per frame
    void Update()
    {
        

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HealthPotion")
        {
            unit.RestoreHealth(HealthPotionAmount);
            Debug.Log("Hp PICKED UP!");
            Destroy(other.gameObject);
            //if(unit.CurHealth < unit.maxHealth)
            //{
            //    unit.RestoreHealth(1);
            //    Debug.Log("Hp PICKED UP!");
            //    Destroy(other.gameObject);
            //}    
        }
        if(other.gameObject.tag == "ArmorPotion")
        {
            unit.RestoreArmor(ArmorPotionAmount);
            Debug.Log("Armor PICKED UP!");
            Destroy(other.gameObject);
        }
    }


}
