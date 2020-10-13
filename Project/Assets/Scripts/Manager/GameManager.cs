using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    // Instantiates Singleton
    public static GameManager Instance { set; get; }
    public bool introEnabled = false;

    public GameObject psxVolume;

    public int frameRate = 200;
    public GameObject playerGo;

    public int deadEnemiesNumber;
    #endregion 

    #region  Options
    [Header("Options")]
    public bool enableHeadBob = false;

    #endregion

    public GameObject deadEnemies;


    public float restartSceneTime = 2.9f;
    private Scene currentScene;

    public int level = 1;

    private void Awake()
    {
        // Sets Singleton
        Instance = this;

        // Gets player gameobject
        playerGo = GameObject.FindGameObjectWithTag("Player");

        // Sets Framerate
        Application.targetFrameRate = frameRate;

        if (Instance == this) Debug.Log("GameManager Singleton Initialized");

        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Press BackSpace");
            StartCoroutine(RestartCurrentScene(-1.69f));
        }
    }

    public IEnumerator RestartCurrentScene(float time)
    {
        Debug.Log("Restarting Scene");
        yield return new WaitForSeconds(restartSceneTime + time);
        SceneManager.LoadScene(currentScene.name);
    }

    public void CountDeadEnemies()
    {
        foreach (Transform child in deadEnemies.transform)
        {
            deadEnemiesNumber++;
        }
    }


}
