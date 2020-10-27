using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Instantiates Singleton
    public static PauseManager Instance { set; get; }

    private bool pressEscape = false;

    public GameObject pauseScreen;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("PauseManager Singleton Initialized");

        pauseScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pressEscape = !pressEscape;
            pauseScreen.SetActive(pressEscape);
        }
    }

    public void PressResume()
    {
        Debug.Log("Press Resume");
    }

    public void PressCheats()
    {
        Debug.Log("Press Cheats");
    }

    public void PressRestart()
    {
        Debug.Log("Press Restart");
    }

    public void PressQuit()
    {
        Debug.Log("Press Quit");
    }
}
