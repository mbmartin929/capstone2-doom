using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Instantiates Singleton
    public static ResourceManager Instance { set; get; }

    public int curResources;

    private void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("ResourceManager Singleton Initialized");
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
