using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    // Instantiates Singleton
    public static EndGameScreen Instance { set; get; }

    [SerializeField] private TextWriter evaluationTitle;
    [SerializeField] private TextWriter nameTitle;

    public int totalSecrets = 0;
    public int totalSecretsFound = 0;

    public int totalCrystals = 0;
    public int crystalsFound = 0;

    public int totalEnemies = 0;
    public int killedEnemies = 0;
    public int enemiesGibbed = 0;

    public int healthFound;
    public int totalHealth;

    public int armorFound;
    public int totalArmor;

    public int powerUpFound;
    public int totalPowerUp;

    public int ammoFound;
    public int totalAmmo;

    public float defaultTypeTime = 0.29f;

    public GameObject blackOverlay;
    public GameObject endScreen;

    public bool active = false;

    private AudioSource audioSource;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("EndGameScreen Singleton Initialized");

        audioSource = GetComponent<AudioSource>();
        endScreen.SetActive(false);
    }

    public void StartEndLevelScreen()
    {
        active = true;

        blackOverlay.SetActive(true);
        TimeManager.Instance.StopTimer();
        MusicManager.Instance.FadeOutActiveMusicCaller();
        MusicManager.Instance.FadeOutAmbientMusicCaller();


        GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>().ControllerPause();

        Debug.Log("End Level");

        StartCoroutine(EvaluationTitle(0f));
        StartCoroutine(EndScreenActive(2.9f));

        //StartCoroutine(Quit());
    }

    public void LoadNextScene()
    {
        if (GameManager.Instance.level == 1) StartCoroutine(GameManager.Instance.LoadSecondScene(-1.69f));
        else if (GameManager.Instance.level == 2) StartCoroutine(GameManager.Instance.LoadSecondScene(-1.69f));

    }

    private IEnumerator EndScreenActive(float time)
    {
        yield return new WaitForSeconds(time);

        MusicManager.Instance.FadeInActiveMusicCaller(5, true, 2);

        Debug.Log("EndScreen Active");
        endScreen.SetActive(true);
        GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(false);
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(4.2f);
        Application.Quit();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator EvaluationTitle(float additionalTime)
    {
        //StartTransition();
        yield return new WaitForSeconds(1.69f);

        evaluationTitle.AddWriter("EVALUATION", 0.069f + additionalTime, true);
        yield return new WaitForSeconds(0.69f);
        nameTitle.AddWriter("Prv. Lin Erevoir", 0.069f + additionalTime, true);
        yield return new WaitForSeconds(0.42f);
        //StartCoroutine(EndTransition());
    }
}
