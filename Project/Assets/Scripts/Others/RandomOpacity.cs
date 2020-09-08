using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOpacity : MonoBehaviour
{
    public float minimumOpacity = 0.25f;
    public float maximumOpacity = 0.5f;

    public float minimumScale = 0.75f;
    public float maximumScale = 3.0f;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        float randomFloat = Random.Range(minimumOpacity, maximumOpacity);

        meshRenderer = GetComponent<MeshRenderer>();

        Color color = meshRenderer.material.color;
        color.a = randomFloat;

        //Debug.Log(meshRenderer.material);

        //meshRenderer.material.color = color;

        //meshRenderer.material.shader = Shader.Find("_BaseColor");
        meshRenderer.material.SetColor("_BaseColor", color);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
