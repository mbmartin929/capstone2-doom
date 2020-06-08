﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public float distance;



    public GameObject GetPlayer()
    {
        return player;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRaycast();
        //anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }
    public void damageTo()
    {
        ;
    }
    public void enemyRaycast()
    {

        RaycastHit hitInfo;
        ////Physics.Raycast(transform.position, -Vector3.up, out hitInfo, 50.0f);
        Ray enemyRay = new Ray(transform.position, transform.forward * distance);

        Debug.DrawRay(transform.position, transform.forward * distance, Color.green);

        if (Physics.Raycast(enemyRay, out hitInfo, distance))
        {
            if (hitInfo.collider != null)
            {               
                if ( hitInfo.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
                    anim.SetTrigger("isChasing");
                    Debug.Log("PLAYER DETECTED");
                }
                else
                {
                    anim.SetTrigger("isPatrolling");                    
                }
            }          
        }
        enemyRay = new Ray(transform.position, transform.forward * distance);
    }
}
