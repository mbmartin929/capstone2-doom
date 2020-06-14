using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]
    //protected int healAmountArmor;

    public GameObject player;
    protected UnitController unit;

    private void Start()
    {
        unit = player.GetComponent<UnitController>();
    }
    protected virtual void healHp(int healAmountHp)
    {
        unit.RestoreHealth(healAmountHp); 
    }

    public virtual void healArmor(int healAmountArmor)
    {
        unit.RestoreArmor(healAmountArmor);
    }



}
