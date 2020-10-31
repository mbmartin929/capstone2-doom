using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    // Instantiates Singleton
    public static GameManager Instance { set; get; }
    public bool introEnabled = false;
    public int frameRate = 200;
    public Volume volume;
    [HideInInspector] public GameObject playerGo;

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
        if (playerGo == GameObject.FindGameObjectWithTag("Player")) Debug.Log("GameManager Found Player");

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

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Debug.Log("Press PageUp");
            StartCoroutine(LoadNextScene(-1.69f));
        }
    }

    public IEnumerator RestartCurrentScene(float time)
    {
        Debug.Log("Restarting Scene");
        yield return new WaitForSeconds(restartSceneTime + time);
        SceneManager.LoadScene(currentScene.name);
    }

    public IEnumerator LoadNextScene(float time)
    {
        Debug.Log("Loading Second Scene");
        yield return new WaitForSeconds(restartSceneTime + time);
        SceneManager.LoadSceneAsync("Scene_2_URP_Martin", LoadSceneMode.Additive);

        yield return new WaitForSeconds(1.0f);

        // Debug.Log("Scene 0 Name: " + SceneManager.GetSceneByBuildIndex(0).name);
        // Debug.Log("Scene 1 Name: " + SceneManager.GetSceneByBuildIndex(1).name);
        // Debug.Log("Scene 2 Name: " + SceneManager.GetSceneByBuildIndex(2).name);

        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(level += 1);

        SceneManager.MoveGameObjectToScene(playerGo, sceneToLoad);
        GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(level -= 1);

        EndGameScreen.Instance.endScreen.SetActive(false);
        EndGameScreen.Instance.blackOverlay.SetActive(false);


        if (level == 1)
        {
            playerGo.transform.position = new Vector3(-21.1f, -14.0f, 110.3f);
            playerGo.transform.eulerAngles = new Vector3(0, 181.2f, 0);

            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.IntroDialogueLvl2());
            GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>().ControllerPause();
        }
        else if (level == 2)
        {

        }
    }

    public void CountDeadEnemies()
    {
        foreach (Transform child in deadEnemies.transform)
        {
            deadEnemiesNumber++;
        }
    }
}
