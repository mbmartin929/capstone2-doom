using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeTest : MonoBehaviour
{
    private Animator animator;

    private float x;
    private float z;

    private GameObject playerGo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerGo = GameManager.Instance.playerGo;
    }

    // Update is called once per frame
    void Update()
    {
        // x = transform.position.x + (playerGo.transform.position.x - transform.position.x) / 2;
        // z = transform.position.z + (playerGo.transform.position.z - transform.position.z) / 2;

        x = transform.position.x - playerGo.transform.position.x;
        z = transform.position.z - playerGo.transform.position.z;

        animator.SetFloat("X", x);
        animator.SetFloat("Z", z);
    }
}
