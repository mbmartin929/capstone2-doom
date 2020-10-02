using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterShrink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleOverTime(1));
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.localScale = this.transform.localScale - new Vector3(1f, 1f, 1f) * Time.deltaTime;

    }

    IEnumerator ScaleOverTime(float time)
    {

        yield return new WaitForSeconds(2f);
        Vector3 originalScale = this.transform.localScale;
        Vector3 desiredScale = new Vector3(0, 0.4205574f,0 );

        float currentTime = 0f;
   
        do
        {          
            this.transform.localScale = Vector3.Lerp(originalScale, desiredScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);
        Destroy(this.gameObject);

    }

}

