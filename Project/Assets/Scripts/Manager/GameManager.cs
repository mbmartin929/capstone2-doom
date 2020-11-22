using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

    public static GameObject savedPlayer;
    string path = "Assets/Save";

    public static GameObject[] GameManagers;
    [SerializeField] private bool main = false;
    [SerializeField] private bool firstPlayer = true;

    private void Awake()
    {
        GameManagers = GameObject.FindGameObjectsWithTag("Manager");
        if (GameManagers.Length >= 2)
        {
            if (!main)
            {
                Debug.Log("Detecting multiple game managers. Manager Name: " + gameObject.name);
                Destroy(gameObject);
            }
        }
        else
        {
            main = true;
            Debug.Log("Number of Game Managers: " + GameManagers.Length);
            Debug.Log("Manager Name: " + gameObject.name);
        }

        // Sets Singleton
        Instance = this;
        if (Instance == this) Debug.Log("GameManager " + level + " Singleton Initialized");

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

        }

        // Sets Framerate
        Application.targetFrameRate = frameRate;

        currentScene = SceneManager.GetActiveScene();

        if (introEnabled)
        {
            Debug.Log("Intro is enabled");

            if (level == 1)
            {
                FirstPersonAIO.Instance.playerCanMove = false;
                FirstPersonAIO.Instance.controllerPauseState = false;
            }

            if (level == 2)
            {
                Debug.Log("Level: " + level);

                playerGo = GameObject.FindGameObjectWithTag("Player");

                if (playerGo != null)
                {
                    Debug.Log("Player already exists");

                    // playerGo.GetComponent<PlayerController>().SetBeginningLevelStats();
                    // internal_player = playerGo.GetComponent<PlayerController>().restartPlayerGo;

                    //Debug.Log("Internal Player is now Restart Player");

                    //playerGo = internal_player;

                    //playerGo = GameObject.FindGameObjectWithTag("Player");
                    // Debug.Log("NAME: " + playerGo.name);
                    // StartCoroutine(LateStart());
                }
                else
                {
                    Debug.Log("Instantiates Player");
                    playerGo = Instantiate(internal_player);
                }

                //PrefabUtility.SaveAsPrefabAsset(playerGo, "Assets/Resources/PlayerLVL2.prefab", out bool success);
                //PrefabUtility.LoadPrefabContents("Assets/Resources/PlayerLVL2.prefab");
                //GameObject newPlayer = Instantiate(Resources.Load())

                Debug.Log("Transferring Player");

                EndGameScreen.Instance.active = false;

                playerGo.transform.position = new Vector3(-21.1f, -14.1f, 110.3f);
                playerGo.transform.eulerAngles = new Vector3(0, 181.2f, 0);


                FirstPersonAIO.Instance.playerCanMove = false;
                FirstPersonAIO.Instance.controllerPauseState = false;
                StartCoroutine(DialogueAssistant.Instance.IntroDialogueLvl2());
                StartCoroutine(ObjectiveManager.Instance.SetActive(ObjectiveManager.Instance.starTime - 4.2f));
                GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(true);

                SettingsManager.Instance.RestartSettingsManager();


            }

            if (level == 3)
            {
                Debug.Log("Level: " + level);

                playerGo = GameObject.FindGameObjectWithTag("Player");

                if (playerGo != null)
                {
                    Debug.Log("Player already exists");

                }
                else
                {
                    Debug.Log("Instantiates Player");
                    playerGo = Instantiate(internal_player);
                }

                Debug.Log("Transferring Player");

                EndGameScreen.Instance.active = false;

                // playerGo.transform.position = new Vector3(-21.1f, -14.1f, 110.3f);
                // playerGo.transform.eulerAngles = new Vector3(0, 181.2f, 0);

                FirstPersonAIO.Instance.playerCanMove = false;
                FirstPersonAIO.Instance.controllerPauseState = false;
                StartCoroutine(DialogueAssistant.Instance.IntroDialogueLvl3());
                StartCoroutine(ObjectiveManager.Instance.SetActive(ObjectiveManager.Instance.starTime - 4.2f));
                GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(true);

                SettingsManager.Instance.RestartSettingsManager();


            }
        }
        else
        if (level == 3)
        {
            Debug.Log("Level: " + level);

            playerGo = GameObject.FindGameObjectWithTag("Player");

            if (playerGo != null)
            {
                Debug.Log("Player already exists");

            }
            else
            {
                Debug.Log("Instantiates Player");
                playerGo = Instantiate(internal_player);
            }

            Debug.Log("Transferring Player");

            EndGameScreen.Instance.active = false;

            // playerGo.transform.position = new Vector3(-21.1f, -14.1f, 110.3f);
            // playerGo.transform.eulerAngles = new Vector3(0, 181.2f, 0);

            StartCoroutine(ObjectiveManager.Instance.SetActive(ObjectiveManager.Instance.starTime - 4.2f));
            GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(true);

            SettingsManager.Instance.RestartSettingsManager();


        }
    }

    private void Start()
    {

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
        yield return new WaitForSeconds(restartSceneTime + time);

        // GameObject newPlayer = Instantiate(internal_player) as GameObject;
        // Debug.Log("Instantiate internal player");

        playerGo.GetComponent<PlayerController>().Destroy();
        Debug.Log("Destroy old player");

        //playerGo = newPlayer;

        //Debug.Log(currentScene.name);
        if (level == 1) SceneManager.LoadScene("Scene_01_URP_Martin");
        else if (level == 2) SceneManager.LoadScene("Scene_2_URP_Martin");
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
