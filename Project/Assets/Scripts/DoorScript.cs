using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public float speedOfDoor = 0.2f;
    private bool isOpen = false;

    //public Vector3 newYPos;
    public float ypos;

    void Awake()
    {
        //gameObject.isStatic
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeDoorState(bool state)
    {
        isOpen = state;
    }
    // Update is called once per frame
    void Update()
    {
        float move = speedOfDoor * Time.deltaTime;
        Vector3 newYPos = new Vector3(transform.position.x, ypos, transform.position.z);
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, newYPos, move);
        }
    }

}
