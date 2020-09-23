using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float defaultX;
    public float defaultY;
    public float xStrength = 1.2f;
    public float yStrength = 1.0f;

    public float lerpSpeed = 5f;

    private Animator animator;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("MouseX", Input.GetAxis("Mouse X"));

        if (Input.GetAxis("Mouse X") != 0) Debug.Log("X: " + Input.GetAxis("Mouse X"));
        if (Input.GetAxis("Mouse Y") != 0) Debug.Log("Y: " + Input.GetAxis("Mouse Y"));

        // Look Left
        if (Input.GetAxis("Mouse X") < 0)
        {
            float newTarget = (defaultX + (-(Input.GetAxis("Mouse X") * xStrength)));
            float x = Mathf.Lerp(rectTransform.anchoredPosition.x, newTarget, lerpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(x, 0);
        }
        // Look Right
        else if (Input.GetAxis("Mouse X") > 0)
        {
            float newTarget = (defaultX + (Input.GetAxis("Mouse X") * xStrength));
            float x = Mathf.Lerp(rectTransform.anchoredPosition.x, -newTarget, lerpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(x, 0);
        }
        // Look Up
        else if (Input.GetAxis("Mouse Y") > 0)
        {
            float newTarget = (defaultY + (Input.GetAxis("Mouse Y") * yStrength));
            float y = Mathf.Lerp(rectTransform.anchoredPosition.y, -newTarget, lerpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(0, y);
        }
        // Look Down
        else if (Input.GetAxis("Mouse Y") < 0)
        {
            float newTarget = (defaultY + (-(Input.GetAxis("Mouse Y") * yStrength)));
            float y = Mathf.Lerp(rectTransform.anchoredPosition.y, newTarget, lerpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(0, y);
        }
        // Look Straight X
        else if (Input.GetAxis("Mouse X") == 0)
        {
            float x = Mathf.Lerp(rectTransform.anchoredPosition.x, 0, lerpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(x, 0);
        }
        // Look Straight Y
        else if (Input.GetAxis("Mouse Y") == 0)
        {
            float y = Mathf.Lerp(rectTransform.anchoredPosition.y, 0, lerpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(0, y);
        }
    }
}