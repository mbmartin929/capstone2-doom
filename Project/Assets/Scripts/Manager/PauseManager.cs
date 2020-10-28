using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Instantiates Singleton
    public static PauseManager Instance { set; get; }

    public float transitionTime = 0.9f;

    #region  Press Button Booleans
    private bool pressEscape = false;
    private bool pressCheats = false;
    private bool pressRestart = false;
    private bool pressSettings = false;
    private bool pressQuit = false;
    #endregion

    // 0=Settings 1=Cheats 2=Restart 3=Quit
    private int buttonID = 0;

    public GameObject pauseScreen;

    [SerializeField] private Animator settingsBoxAnimator;
    [SerializeField] private Animator cheatsBoxAnimator;
    [SerializeField] private Animator restartBoxAnimator;
    [SerializeField] private Animator quitBoxAnimator;
    private Animator animator;

    [Header("Cheat Buttons")]
    public GameObject unlockWeaponsToggleOn;
    public GameObject unlimitedAmmoToggleOn;
    public GameObject godModeToggleOn;

    public GameObject unlockWeaponsToggleOff;
    public GameObject unlimitedAmmoToggleOff;
    public GameObject godModeToggleOff;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("PauseManager Singleton Initialized");

        animator = GetComponent<Animator>();

        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pressEscape = !pressEscape;
            //pauseScreen.SetActive(pressEscape);
            if (pressEscape)
            {
                pauseScreen.SetActive(true);
                animator.SetTrigger("Start");
            }
            else if (!pressEscape)
            {
                animator.SetTrigger("Escape");
                ExitAllWindows();
            }
        }
    }

    // Used as Animation Event
    public void DisableGameObject()
    {
        Debug.Log("Disable Pause Screen");
        pauseScreen.SetActive(false);
    }

    #region Pause Menu Buttons
    public void PressResume()
    {
        Debug.Log("Press Resume");
        pressEscape = false;
        animator.SetTrigger("Escape");

        cheatsBoxAnimator.SetTrigger("Exit");
        restartBoxAnimator.SetTrigger("Exit");
        settingsBoxAnimator.SetTrigger("Exit");
        quitBoxAnimator.SetTrigger("Exit");

        GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>().ControllerPause();
    }

    public void PressCheats()
    {
        Debug.Log("Press Cheats");

        UpdateCheatToggleButtons();

        // pressCheats = !pressCheats;
        // if (pressCheats)
        // {
        //     cheatsBoxAnimator.gameObject.SetActive(true);
        //     cheatsBoxAnimator.SetTrigger("Start");
        // }
        // else if (!pressCheats) cheatsBoxAnimator.SetTrigger("Escape");

        //pressCheats = !pressCheats;



        if (pressRestart)
        {
            Debug.Log("From Cheats Window");

            //pressCheats = !pressCheats;
            StartCoroutine(StartNewWindow(1));
        }
        else if (pressSettings)
        {
            Debug.Log("From Settings Window");

            //pressCheats = !pressCheats;
            StartCoroutine(StartNewWindow(1));
        }
        else if (pressQuit)
        {
            Debug.Log("From Quit Window");

            //pressCheats = !pressCheats;
            StartCoroutine(StartNewWindow(1));
        }
        else if (!pressCheats)
        {
            //pressCheats = !pressCheats;
            pressCheats = true;
            Debug.Log("Press Cheats: " + pressCheats);
        }
        // else if (pressCheats)
        // {
        //     Debug.Log("From Nothing");

        //     //pressCheats = !pressCheats;
        //     cheatsBoxAnimator.gameObject.SetActive(true);
        //     cheatsBoxAnimator.SetTrigger("Start");
        // }

        if (pressCheats && !pressRestart && !pressSettings && !pressQuit)
        {
            Debug.Log("From Nothing");

            //pressCheats = !pressCheats;
            cheatsBoxAnimator.gameObject.SetActive(true);
            cheatsBoxAnimator.SetTrigger("Start");
        }

        UpdateCheatToggleButtons();
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit
    public void PressRestart()
    {
        Debug.Log("Press Restart");

        //pressRestart = !pressRestart;

        if (pressCheats)
        {
            Debug.Log("From Cheats Window");

            //pressRestart = !pressRestart;
            StartCoroutine(StartNewWindow(2));
        }
        else if (pressSettings)
        {
            Debug.Log("From Settings Window");

            //pressRestart = !pressRestart;
            StartCoroutine(StartNewWindow(2));
        }
        else if (pressQuit)
        {
            Debug.Log("From Quit Window");

            // pressRestart = !pressRestart;
            StartCoroutine(StartNewWindow(2));
        }
        else if (!pressRestart)
        {
            //pressRestart = !pressRestart;
            pressRestart = true;
            Debug.Log("Press Restart: " + pressRestart);
        }
        // else if (pressRestart)
        // {
        //     Debug.Log("From Nothing");

        //     //pressRestart = !pressRestart;
        //     restartBoxAnimator.gameObject.SetActive(true);
        //     restartBoxAnimator.SetTrigger("Start");
        // }

        if (pressRestart && !pressCheats && !pressSettings && !pressQuit)
        {
            Debug.Log("From Nothing");

            //pressCheats = !pressCheats;
            restartBoxAnimator.gameObject.SetActive(true);
            restartBoxAnimator.SetTrigger("Start");
        }
    }

    public void PressQuit()
    {
        Debug.Log("Press Quit");
        SceneManager.LoadScene(0);
    }
    #endregion

    public void UpdateCheatToggleButtons()
    {
        if (CheatsManager.Instance.enableUnlockAllWeapons)
        {
            unlockWeaponsToggleOn.SetActive(true);
            unlockWeaponsToggleOff.SetActive(false);
        }
        else
        {
            unlockWeaponsToggleOn.SetActive(false);
            unlockWeaponsToggleOff.SetActive(true);
        }

        if (CheatsManager.Instance.enableUnlimitedAmmo)
        {
            unlimitedAmmoToggleOn.SetActive(true);
            unlimitedAmmoToggleOff.SetActive(false);
        }
        else
        {
            unlimitedAmmoToggleOn.SetActive(false);
            unlimitedAmmoToggleOff.SetActive(true);
        }

        if (CheatsManager.Instance.enableGodMode)
        {
            godModeToggleOn.SetActive(true);
            godModeToggleOff.SetActive(false);
        }
        else
        {
            godModeToggleOn.SetActive(false);
            godModeToggleOff.SetActive(true);
        }
    }

    public void ExitAllWindows()
    {
        pressCheats = false;
        pressRestart = false;
        pressSettings = false;
        pressQuit = false;

        cheatsBoxAnimator.SetTrigger("Exit");
        restartBoxAnimator.SetTrigger("Exit");

        Debug.Log("Exit All Windows");
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit
    public IEnumerator StartNewWindow(int id)
    {
        //Debug.Log("Start New Window");
        ExitAllWindows();

        yield return new WaitForSeconds(transitionTime);

        if (id == 0)
        {
            pressSettings = true;
        }
        else if (id == 1)
        {
            Debug.Log("Coroutine Cheat");

            pressCheats = true;
            cheatsBoxAnimator.gameObject.SetActive(true);
            cheatsBoxAnimator.SetTrigger("Start");
        }
        else if (id == 2)
        {
            Debug.Log("Coroutine Restart");

            pressRestart = true;
            restartBoxAnimator.gameObject.SetActive(true);
            restartBoxAnimator.SetTrigger("Start");
        }
        else if (id == 3)
        {
            pressQuit = true;
        }
    }


    public void RestartGame()
    {
        StartCoroutine(GameManager.Instance.RestartCurrentScene(0.0f));
    }
}
