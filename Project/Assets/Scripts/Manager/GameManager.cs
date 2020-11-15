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
    public GameObject playerGo;
    public GameObject all;
    #endregion 

    #region  Options
    [Header("Options")]
    public bool enableHeadBob = false;

    #endregion

    public GameObject deadEnemies;

    [SerializeField] private GameObject internal_player;

    public float restartSceneTime = 2.9f;
    private Scene currentScene;

    public int level = 1;

    private AsyncOperation sceneAsync;

    private void Awake()
    {
        // Sets Singleton
        Instance = this;

        // Debug.Log(CrashReport.lastReport);
        // Debug.Log(CrashReport.reports);

        if (level == 1)
        {
            //SettingsManager.Instance.RestartSettingsManager();

            if (GameObject.FindGameObjectWithTag("Player"))
            {
                Debug.Log("Player already exists");
                playerGo = GameObject.FindGameObjectWithTag("Player");
                playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();
            }
            else
            {
                Debug.Log("Instantiates Player");
                playerGo = Instantiate(internal_player);
                playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();
            }
            //if (playerGo == GameObject.FindGameObjectWithTag("Player")) Debug.Log(gameObject.name + " Found Player");
        }
        else if (level == 2)
        {
            // if (GameObject.Find("Player_LVL 1.1"))
            // {
            //     Debug.Log("Player already exists");
            //     playerGo = GameObject.FindGameObjectWithTag("Player");
            //     playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();
            // }

            // if (GameObject.Find("Player_LVL 1.1"))
            // {
            //     Debug.Log("Player already exists");
            //     playerGo = GameObject.FindGameObjectWithTag("Player");
            // }
            // else
            // {
            //     Debug.Log("Instantiates Player");
            //     playerGo = Instantiate(internal_player);
            // }
        }

        // Gets player gameobject
        // playerGo = GameObject.FindGameObjectWithTag("Player");
        // if (playerGo == GameObject.FindGameObjectWithTag("Player")) Debug.Log(gameObject.name + " Found Player");

        // Sets Framerate
        Application.targetFrameRate = frameRate;

        if (Instance == this) Debug.Log("GameManager " + level + " Singleton Initialized");

        currentScene = SceneManager.GetActiveScene();

        if (introEnabled)
        {
            Debug.Log("Intro is enabled");
            if (level == 2)
            {
                Debug.Log("Level: " + level);

                playerGo = GameObject.FindGameObjectWithTag("Player");

                if (playerGo != null)
                {
                    Debug.Log("Player already exists");

                    playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();

                    internal_player = playerGo.GetComponent<PlayerController>().restartPlayerGo;

                    playerGo = null;

                    playerGo = internal_player;
                }
                else
                {
                    Debug.Log("Instantiates Player");
                    playerGo = Instantiate(internal_player);
                }



                Debug.Log("Transferring Player");

                EndGameScreen.Instance.active = false;

                playerGo.transform.position = new Vector3(-21.1f, -14.1f, 110.3f);
                playerGo.transform.eulerAngles = new Vector3(0, 181.2f, 0);

                FirstPersonAIO.Instance.playerCanMove = false;
                //GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>().ControllerPause();
                FirstPersonAIO.Instance.controllerPauseState = false;
                StartCoroutine(DialogueAssistant.Instance.IntroDialogueLvl2());
                StartCoroutine(ObjectiveManager.Instance.SetActive(ObjectiveManager.Instance.starTime - 4.2f));
                GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(true);

                SettingsManager.Instance.RestartSettingsManager();
            }
        }
    }

    private void Start()
    {
        // if (GameObject.Find("Player_LVL 1.1"))
        // {
        //     Debug.Log("Player already exists");
        //     playerGo = GameObject.FindGameObjectWithTag("Player");
        //     playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();
        // }
        // else
        // {
        //     Debug.Log("Instantiates Player");
        //     playerGo = Instantiate(internal_player);
        //     playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();
        // }
    }

    void OnGUI()
    {
        var reports = CrashReport.reports;
        GUILayout.Label("Crash reports:");
        foreach (var r in reports)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Crash: " + r.time);
            if (GUILayout.Button("Log"))
            {
                Debug.Log(r.text);
            }
            if (GUILayout.Button("Remove"))
            {
                r.Remove();
            }
            GUILayout.EndHorizontal();
        }
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
            StartCoroutine(LoadSecondScene(-1.69f));
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            Debug.Log("Press PageDown");
            //SceneManager.UnloadSceneAsync("Scene_01_URP_Martin");
        }
    }

    public IEnumerator RestartCurrentScene(float time)
    {
        Debug.Log("Restarting Scene");
        //internal_player = playerGo.GetComponent<PlayerController>().restartPlayerGo;
        yield return new WaitForSeconds(restartSceneTime + time);


        Instantiate(internal_player);
        Destroy(playerGo);
        playerGo = internal_player;
        // Destroy(playerGo);
        // Instantiate(internal_player);
        // playerGo = internal_player;

        SceneManager.LoadScene(currentScene.name);
        Debug.Log("Finish Restarting Scene");
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        //Wait until we are done loading the scene
        while (scene.progress < 0.9f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        }
        OnFinishedLoadingAllScene();
    }

    void OnFinishedLoadingAllScene()
    {
        Debug.Log("Done Loading Scene");
        EnableScene(2);
        Debug.Log("Scene Activated!");
    }

    void EnableScene(int index)
    {
        //Activate the Scene
        sceneAsync.allowSceneActivation = true;

        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Scene is Valid");
            SceneManager.MoveGameObjectToScene(playerGo, sceneToLoad);
            SceneManager.SetActiveScene(sceneToLoad);
            SceneManager.UnloadSceneAsync("Scene_01_URP_Martin");
            //Destroy(all, 1f);
        }
    }

    public IEnumerator LoadSecondScene(float time)
    {
        Debug.Log("Loading Second Scene");
        yield return new WaitForSeconds(restartSceneTime + time);
        //SceneManager.LoadSceneAsync("Scene_2_URP_Martin", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Scene_2_URP_Martin", LoadSceneMode.Single);

        yield return new WaitForSeconds(1.0f);

        //StartCoroutine(LoadScene("Scene_2_URP_Martin"));

        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(level += 1);

        // SceneManager.MoveGameObjectToScene(playerGo, sceneToLoad);
        // GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(true);

        EndGameScreen.Instance.endScreen.SetActive(false);
        EndGameScreen.Instance.blackOverlay.SetActive(false);


        if (level == 1)
        {
            Debug.Log("Repositioning Player");

            //SceneManager.UnloadSceneAsync("Scene_01_URP_Martin");
            // playerGo.transform.position = new Vector3(-21.1f, -14.0f, 110.3f);
            // playerGo.transform.eulerAngles = new Vector3(0, 181.2f, 0);

            // DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.IntroDialogueLvl2());
            // GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>().ControllerPause();
        }
        else if (level == 2)
        {

        }
    }
}
