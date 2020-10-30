using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMeshInSeconds : MonoBehaviour
{
    public float minSeconds = 0.25f;
    public float maxSeconds = 0.6f;
    private float seconds;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        seconds = Random.Range(minSeconds, maxSeconds);

        //StartCoroutine(ActivateMesh());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ActivateMesh()
    {
        yield return new WaitForSeconds(seconds);
        meshRenderer.enabled = true;
    }
}
