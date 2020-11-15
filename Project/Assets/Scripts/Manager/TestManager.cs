using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Transform[] spawnLocs;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            int iteration = 0;
            foreach (var item in enemies)
            {
                GameObject enemy = Instantiate(item, spawnLocs[iteration].position, Quaternion.identity);
                iteration++;
            }
            iteration = 0;
        }
    }
}
