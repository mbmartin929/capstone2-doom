using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class DoorScript : MonoBehaviour
{
    public enum DoorType
    {
        NormalDoor, SpecialDoor, ExitDoor
    }
    public DoorType doorType = DoorType.NormalDoor;

    public float speedOfDoor = 0.2f;

    public int keyRequirement = 0;

    private bool isOpen = false;

    //public Vector3 newYPos;
    public float ypos;

    public bool isNear = false;
    public bool interactable = false;
    //public GameObject pressE;

    void Awake()
    {
        //gameObject.isStatic
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (pressE != null) pressE.SetActive(false);
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
            if (doorType == DoorType.NormalDoor) transform.position = Vector3.MoveTowards(transform.position, newYPos, move);
            // else if (doorType == DoorType.SpecialDoor)
            // {

            // }
            // else if (doorType == DoorType.ExitDoor)
            // {
            //     if (GameManager.Instance.playerGo.GetComponent<PlayerController>().keyAmount >= keyRequirement)
            //     {
            //         Debug.Log("Exit Door");
            //     }
            //     else Debug.Log("Need key");
            // }
        }

        // if (isNear && interactable)
        // {
        //     pressE.SetActive(true);
        // }
        // else if (!isNear && interactable) pressE.SetActive(false);
    }

    public void OpenExitDoor()
    {

    }
}
