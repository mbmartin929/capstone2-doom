using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightDirection : MonoBehaviour
{
    const int numberOrientations = 8;
    const float anglePerOrientation = 360f / numberOrientations;

    Transform player;
    public float angle;
    Vector3 direction;

    public GameObject[] planes;

    public Renderer spriteObj;

    public Sprite[] mat;

    private Animator animator;
    private float facingAngle;
    private int lastOrientation;


    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        foreach (GameObject item in planes)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {

        //Vector3 viewDirection = -new Vector3(player.transform.forward.x, 0, player.transform.forward.z);
        //transform.LookAt(transform.position + viewDirection);

        direction = player.transform.position - transform.position;
        angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        //dir.y = 0;
        angle = Quaternion.LookRotation(transform.forward).eulerAngles.y;

        //ChangeDirection();
        //SpriteDirection();
        PlaneDirection();
    }

    void ChangeDirection()
    {
        if (angle < 0) angle += 360; // Just in case
        spriteObj.GetComponent<SpriteRenderer>().sprite = mat[(int)Mathf.Round(angle / 360f * mat.Length) % mat.Length];

        int directionID = (int)Mathf.Round(angle / 360f * mat.Length) % mat.Length;

        Debug.Log(directionID);
    }

    void SpriteDirection()
    {


        if (angle < 0) angle += 360;
        int directionID = (int)Mathf.Round(angle / 360f * 8) % 8;
        Debug.Log(directionID);


        animator.SetInteger("directionID", directionID);

        // if (directionID == 0)
        // {
        //     animator.SetInteger("directionID", directionID);
        // }
        // else if (directionID == 1)
        // {
        //     animator.SetInteger("directionID", directionID);
        // }
    }

    void PlaneDirection()
    {
        if (angle < 0) angle += 360;
        int directionID = (int)Mathf.Round(angle / 360f * 8) % 8;
        Debug.Log(directionID);

        for (int i = 0; i < planes.Length; i++)
        {
            if (i == directionID)
            {
                planes[i].SetActive(true);
                //planes[i].GetComponent<Animator>().SetInteger("directionID", directionID);
            }
            else planes[i].SetActive(false);
        }

    }

    private void UpdateOrientation()
    {
        Transform parent = transform.parent;
        if (parent == null)
            return;

        // Get direction normal to camera, ignore y axis
        Vector3 dir = Vector3.Normalize(
            new Vector3(player.transform.position.x, 0, player.transform.position.z) -
            new Vector3(transform.position.x, 0, transform.position.z));

        // Get parent forward normal, ignore y axis
        Vector3 parentForward = transform.parent.forward;
        parentForward.y = 0;

        // Get angle and cross product for left/right angle
        facingAngle = Vector3.Angle(dir, parentForward);
        facingAngle = facingAngle * -Mathf.Sign(Vector3.Cross(dir, parentForward).y);

        // Facing index
        int orientation = -Mathf.RoundToInt(facingAngle / anglePerOrientation);
        // Wrap values to 0 .. numberOrientations-1
        orientation = (orientation + numberOrientations) % numberOrientations;

        // // Change person to this orientation
        // if (orientation != lastOrientation)
        //     UpdatePerson(orientation);
    }
}
