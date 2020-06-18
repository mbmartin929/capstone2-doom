using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject openPanel = null;
    public float speedOfDoor;
    public GameObject Door;
    private bool isOpen = false;
    private bool canOpen = false;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isOpen && canOpen)
            {
                opening();
                isOpen = true;
                Debug.Log("E PRESSED");             
                //gameObject.transform.Translate(Vector3.up * Time.deltaTime * speedOfDoor);
               

            }
        }
      
    }

    private void opening()
    {

        Door.transform.position += Vector3.up * speedOfDoor * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canOpen = true;
            openPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canOpen = false;
            openPanel.SetActive(false);
        }
    }

}
