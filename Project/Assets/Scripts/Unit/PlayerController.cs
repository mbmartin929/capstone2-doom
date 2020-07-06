using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public GameObject player;
    protected UnitController unit;

    
   
    // Start is called before the first frame update
    void Start()
    {
        unit = player.GetComponent<UnitController>();
        
     
     
    }

    // Update is called once per frame
    void Update()
    {
        

       
    }




}
