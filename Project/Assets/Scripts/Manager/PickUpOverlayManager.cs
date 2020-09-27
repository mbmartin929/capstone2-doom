using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpOverlayManager : MonoBehaviour
{
    // Instantiates Singleton
    public static PickUpOverlayManager Instance { set; get; }

    public GameObject armorPickUpOverlay;
    public GameObject healthPickUpOverlay;
    public GameObject ammoPickUpOverlay;

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
        armorPickUpOverlay.GetComponent<Animator>().SetTrigger("Start Fade");
    }

    public void HealthOverlay()
    {
        healthPickUpOverlay.GetComponent<Animator>().SetTrigger("Start Fade");
    }

    public void AmmoOverlay()
    {
        ammoPickUpOverlay.GetComponent<Animator>().SetTrigger("Start Fade");
    }
}
