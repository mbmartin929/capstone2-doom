using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.5f;

    private RawImage image;

    private void Awake()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = (Time.time * speed);

        image.uvRect = new Rect(offset, 0, 1, 1);
    }
}
