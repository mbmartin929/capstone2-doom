using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplosion : MonoBehaviour
{
    public bool isExplode = false;

    public float minY = 420f;
    public float maxY = 960;

    public float minX = 240;
    public float maxX = 420;

    public float minZ = 240;
    public float maxZ = 420;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isExplode)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);

            GetComponent<Rigidbody>().AddForce(x, y, z);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
