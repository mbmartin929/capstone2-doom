using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollider : MonoBehaviour
{
    public string objecttiveText;
    public float additionalTime = 0.01f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with " + gameObject.name);

            ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective(objecttiveText, 0.01f, 2.9f));

            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 5.0f);
        }
    }
}
