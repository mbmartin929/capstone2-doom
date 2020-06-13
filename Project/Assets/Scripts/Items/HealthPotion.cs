using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public GameObject player;
    protected UnitController unit;
    [SerializeField]
    private int HealthPotionAmount;
    // Start is called before the first frame update
    void Start()
    {
        unit = player.GetComponent<UnitController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            unit.RestoreHealth(HealthPotionAmount);
            Debug.Log("Hp PICKED UP!");
            Destroy(this.gameObject);
            //if(unit.CurHealth < unit.maxHealth)
            //{
            //    unit.RestoreHealth(HealthPotionAmount);
            //    Debug.Log("Hp PICKED UP!");
            //    Destroy(other.gameObject);
            //} 
        }

    }
}
