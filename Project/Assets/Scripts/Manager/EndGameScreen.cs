using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    // Instantiates Singleton
    public static EndGameScreen Instance { set; get; }

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("EndGameScreen Singleton Initialized");
    }

    public void StartEndGameScreen()
    {

    }
}
