using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpRawImage : MonoBehaviour
{
    private RawImage image;

    public float fadeInRate = 0.5f;
    public float fadeOutRate = 0.5f;

    public float defaultAlpha = 0.167f;
    private float curAlpha;
    public Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();

        //Debug.Log(image);
        //Debug.Log(image.color.a);

        // Sets alpha to 0
        Color color = new Vector4(defaultColor.r, defaultColor.g, defaultColor.b, 0);
        image.color = color;

        //StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeIn()
    {
        Color curColor = image.color;
        while (Mathf.Abs(curColor.a - defaultAlpha) > 0.0001f)
        {
            Debug.Log(image.color.a);
            curColor.a = Mathf.Lerp(curColor.a, defaultAlpha, fadeInRate * Time.deltaTime);
            image.color = curColor;
            yield return null;
        }
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        Color curColor = image.color;
        while (Mathf.Abs(curColor.a - 0) > 0.0001f)
        {
            Debug.Log(image.color.a);
            curColor.a = Mathf.Lerp(curColor.a, 0, fadeOutRate * Time.deltaTime);
            image.color = curColor;
            yield return null;
        }

        Color color = new Vector4(defaultColor.r, defaultColor.g, defaultColor.b, 0);
        image.color = color;
    }
}
