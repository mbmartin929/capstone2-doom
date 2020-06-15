using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject openPanel = null;
    public float speedOfDoor;
    public GameObject Door;
    bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpened = true;
                Debug.Log("E PRESSED");
                Door.transform.position += new Vector3(0, 4, 0);

            }
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           
            openPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            openPanel.SetActive(false);
        }
    }

}
