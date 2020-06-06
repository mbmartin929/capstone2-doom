﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooksAtPlayer : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 targetRotation = new Vector3(target.transform.position.x,
                                     transform.position.y,
                                     target.transform.position.z);

        transform.LookAt(targetRotation);
    }
}
