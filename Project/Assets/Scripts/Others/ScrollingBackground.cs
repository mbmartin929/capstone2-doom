using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.5f;
    public float reset = 29f;

    private RawImage image;
    private Renderer renderer;
    private Material material;
    private float currentscroll;
    private Vector2 savedOffset;

    private Vector2 startPos;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        // renderer = GetComponent<Renderer>();
        // material = GetComponent<Renderer>().material;
        // savedOffset = renderer.material.mainTextureOffset;

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = (Time.time * speed);

        // if (sprite != null)
        // {
        // currentscroll += speed * Time.deltaTime;
        // currentscroll += speed * Time.deltaTime;
        // material.mainTextureOffset = new Vector2(currentscroll, 0);
        //}
        //else if (image != null) image.uvRect = new Rect(offset, 0, 1, 1);
        image.uvRect = new Rect(offset, 0, 1, 1);

        // float x = Mathf.Repeat(Time.time * speed, 1);
        // Vector2 offset = new Vector2(x, savedOffset.y);
        // renderer.material.mainTextureOffset = offset;

        // float newPos = Mathf.Repeat(Time.time * speed, reset);
        // transform.position = startPos + Vector2.right * newPos;
    }
}
