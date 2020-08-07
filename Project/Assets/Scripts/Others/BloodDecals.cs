using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecals : MonoBehaviour
{
    public GameObject effectPrefab;
    public GameObject decalPrefab;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fire();
        }
    }

    void fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100f))
        {
            var hitRotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Instantiate(decalPrefab, hit.point + 0.01f * hit.normal, hitRotation);
            //Instantiate(effectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            //Instantiate(decalPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }

    }
}
