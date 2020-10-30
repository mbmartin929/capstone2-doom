using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantDoor : MonoBehaviour
{
    public float waitTime = 6.9f;

    public IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<Animation>().Play();
    }
}
