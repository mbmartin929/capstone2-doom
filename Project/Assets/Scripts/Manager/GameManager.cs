using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    // Instantiates Singleton
    public static GameManager Instance { set; get; }

    public GameObject LocalVolume;

    public int frameRate = 200;
    public GameObject playerGo;

    #endregion 

    #region  Options
    [Header("Options")]
    public bool enableHeadBob = false;

    #endregion

    public GameObject DeadEnemies;

    private void Awake()
    {
        // Sets Singleton
        Instance = this;

        // Gets player gameobject
        playerGo = GameObject.FindGameObjectWithTag("Player");

        // Sets Framerate
        Application.targetFrameRate = frameRate;

        if (Instance == this) Debug.Log("GameManager Singleton Initialized");
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
