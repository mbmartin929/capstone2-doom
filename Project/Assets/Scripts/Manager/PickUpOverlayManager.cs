using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpOverlayManager : MonoBehaviour
{
    // Instantiates Singleton
    public static PickUpOverlayManager Instance { set; get; }

    public GameObject shieldPickUpOvelay;
    public GameObject healthPickUpOvelay;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("PickUpOverlayManager Singleton Initialized");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShieldOverlay()
    {
        shieldPickUpOvelay.GetComponent<Animator>().SetTrigger("Start Fade");
        //StartCoroutine(shieldPickUpOvelay.GetComponent<LerpRawImage>().FadeIn());
    }
}
