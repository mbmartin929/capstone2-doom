using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : MonoBehaviour
{
    public GameObject player;
    protected UnitController unit;
    [SerializeField]
    private int ArmorPotionAmount;
    // Start is called before the first frame update
    void Start()
    {
        unit = player.GetComponent<UnitController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            unit.RestoreHealth(ArmorPotionAmount);
            Debug.Log("Armor PICKED UP!");
            Destroy(this.gameObject);
            if (unit.CurArmor < unit.maxArmor)
            {
                unit.RestoreArmor(ArmorPotionAmount);
                Debug.Log("Hp PICKED UP!");
                Destroy(other.gameObject);
            }
        }

    }
}
