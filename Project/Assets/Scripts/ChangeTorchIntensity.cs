using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTorchIntensity : MonoBehaviour
{
    public float minIntensity = 1.1f;
    public float maxIntensity = 1.9f;
    public float minRange = 2.5f;
    public float maxRange = 3.5f;

    public float changeTime = 0.05f;

    private Light light;
    private float intensity;

    void Awake()
    {
        light = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        intensity = minIntensity;
        InvokeRepeating("ChangeIntensity", 0f, changeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeIntensity()
    {
        //intensity = Random.Range(minIntensity, maxIntensity);

        if (intensity == minIntensity)
        {
            light.intensity = maxIntensity;
            light.range = maxRange;
            intensity = maxIntensity;
            //Debug.Log("Change to MAX");
        }
        else if (intensity == maxIntensity)
        {
            light.intensity = minIntensity;
            light.range = minRange;
            intensity = minIntensity;
            //Debug.Log("Change to MINIMUM");
        }

        //light.intensity = intensity;
    }
}
