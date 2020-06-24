using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{

    public Camera fpsCam;
    public float range = 50f;

    //controls spread of cone
    public float scaleLimit;
    public float z = 10f;

    public int pelletCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < pelletCount; ++i) //test 
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector3 direction = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction.z = z;// circle is at Z units 
        direction = transform.TransformDirection(direction.normalized); //converting the Vector3.forward to transform.forward
        RaycastHit hit;
        Ray ray = new Ray(fpsCam.transform.position,direction);

        if (Physics.Raycast( ray, out hit, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction * range, Color.red);
            Debug.Log(hit.transform.name);
        }
    }
}
