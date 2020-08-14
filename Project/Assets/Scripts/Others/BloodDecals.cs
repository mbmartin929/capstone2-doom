using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecals : MonoBehaviour
{
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
            var decPrefab = Instantiate(decalPrefab, hit.point + 0.01f * hit.normal, hitRotation);

            if (gameObject.tag != "Enemy")
            {
             
                decPrefab.transform.parent = hit.transform.parent;
                //decPrefab.transform.SetParent(hit.transform.parent);
                Debug.Log(hit.transform.gameObject.name + "HERE");
            }

        }

    }
}
