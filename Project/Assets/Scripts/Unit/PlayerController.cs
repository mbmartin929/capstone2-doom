using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public GameObject player;
    protected UnitController unit;
    public bool isDamaged;


    // Start is called before the first frame update
    void Start()
    {
        unit = player.GetComponent<UnitController>();
        unit.CurHealth = 100;
        
     
     
    }

    // Update is called once per frame
    void Update()
    {
        

       
    }




}
