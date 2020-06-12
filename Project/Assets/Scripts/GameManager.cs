using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    // Instantiates Singleton
    public static GameManager Instance { set; get; }

    public int frameRate = 200;

    #endregion 

    private void Awake()
    {
        // Sets Singleton
        Instance = this;

        // Sets Framerate
        Application.targetFrameRate = frameRate;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
