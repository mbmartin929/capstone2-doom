using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightDirection : MonoBehaviour
{
    Transform player;
    public float angle;
    Vector3 direction;

    public Renderer spriteObj;

    public Sprite[] mat;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

        direction = player.transform.position - transform.position;
        angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        ChangeDirection();

    }

    void ChangeDirection()
    {
        if (angle < 0) angle += 360; // Just in case
        spriteObj.GetComponent<SpriteRenderer>().sprite = mat[(int)Mathf.Round(angle / 360f * mat.Length) % mat.Length];
    }
}
