using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public float speedOfDoor = 0.2f;
    private bool isOpen = false;




    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeDoorState()
    {
        isOpen = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (isOpen)
        {
            transform.position += Vector3.up * speedOfDoor * Time.deltaTime;
        }
    }

}
