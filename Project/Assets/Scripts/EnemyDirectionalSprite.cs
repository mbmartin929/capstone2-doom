using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionalSprite : MonoBehaviour
{
    public GameObject[] faces;

    private int directionID = 0;

    private TempNavMesh tempNavMesh;

    // Start is called before the first frame update
    void Start()
    {
        tempNavMesh = GetComponent<TempNavMesh>();
        //Debug.Log(tempNavMesh.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        directionID = tempNavMesh.waypointID;

        for (int i = 0; i < faces.Length; i++)
        {
            if (i == directionID)
            {
                faces[i].SetActive(true);
                //planes[i].GetComponent<Animator>().SetInteger("directionID", directionID);
            }
            else faces[i].SetActive(false);
        }
    }
}
