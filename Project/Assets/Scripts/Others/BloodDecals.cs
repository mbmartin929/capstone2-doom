using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecals : MonoBehaviour
{
    // Default Ground
    public int layerMask = 8;
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

            Fire();
        }
    }

    void Fire()
    {
        //Debug.Log("Fire");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Raycast");

            if (hit.transform.gameObject.tag == "Enemy")
            {
                return;
                //decPrefab.transform.SetParent(hit.transform.parent);
            }

            var hitRotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            var decPrefab = Instantiate(decalPrefab, hit.point + 0.01f * hit.normal, hitRotation);
            decPrefab.transform.parent = hit.transform.parent;

            Debug.Log("Name: " + hit.transform.gameObject.name + " || " + "Tag: " + hit.transform.gameObject.tag);

        }

    }
}
