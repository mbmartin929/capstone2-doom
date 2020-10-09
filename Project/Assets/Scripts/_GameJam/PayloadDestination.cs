using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PayloadDestination : MonoBehaviour
{
    //public Image loadingImage;
    public UnityEngine.UI.Image image;

    public GameObject payloadGo;

    public float radius = 6.9f;

    public float waitTime = 6.9f;
    public bool arrived = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, payloadGo.transform.position) < radius && !arrived))
        {
            Payload payload = payloadGo.GetComponent<Payload>();
            StartCoroutine(CollidePayload(payload));
            arrived = true;
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.gameObject.name);

    //     if (other.CompareTag("Payload"))
    //     {
    //         Payload payload = other.GetComponent<Payload>();
    //         arrived = true;
    //         StartCoroutine(CollidePayload(payload));

    //         Debug.Log("Arrived");
    //     }
    // }

    private IEnumerator CollidePayload(Payload payload)
    {
        float speed = payload.moveSpeed;
        payload.moveSpeed = 0;
        yield return new WaitForSeconds(waitTime);

        payload.current++;
        payload.moveSpeed = speed;

        Debug.Log("Payload going next");
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }
}
