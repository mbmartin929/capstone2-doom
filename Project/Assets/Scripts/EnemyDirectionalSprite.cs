using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionalSprite : MonoBehaviour
{
    public GameObject[] faces;

    private int directionID = 0;

    private AISFM AIFSM;

    public bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {
        AIFSM = GetComponent<AISFM>();
        //Debug.Log(tempNavMesh.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (!AIFSM.tempPatrol)
            {
                //Debug.Log("8D: " + directionID);
                directionID = AIFSM.waypointID;
            }
            else directionID = AIFSM.tempID;

            for (int i = 0; i < faces.Length; i++)
            {
                if (i == directionID)
                {
                    faces[i].SetActive(true);
                }
                else faces[i].SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {

    }
}
