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
    [HideInInspector] public bool pressEscape = false;
    private bool pressCheats = false;
    private bool pressRestart = false;
    private bool pressSettings = false;
    private bool pressQuit = false;
    private bool pressUpgrades = false;
    #endregion

    // 0=Settings 1=Cheats 2=Restart 3=Quit
    private int buttonID = 0;

    public GameObject pauseScreen;

    [SerializeField] private Animator settingsBoxAnimator;
    [SerializeField] private Animator cheatsBoxAnimator;
    [SerializeField] private Animator restartBoxAnimator;
    [SerializeField] private Animator upgradestBoxAnimator;
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
                //Time.timeScale = 0;
                pauseScreen.SetActive(true);
                animator.SetTrigger("Start");
            }
            else if (!pressEscape)
            {
                //Time.timeScale = 1;
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
        //Time.timeScale = 1;

        Debug.Log("Press Resume");

        pressEscape = false;
        animator.SetTrigger("Escape");

        GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>().ControllerPause();

        cheatsBoxAnimator.SetTrigger("Exit");
        restartBoxAnimator.SetTrigger("Exit");
        settingsBoxAnimator.SetTrigger("Exit");
        quitBoxAnimator.SetTrigger("Exit");
        //upgradestBoxAnimator.SetTrigger("Exit");
    }

    public void PressCheats()
    {
        Debug.Log("Press Cheats");

        UpdateCheatToggleButtons();

        if (pressRestart)
        {
            Debug.Log("From Cheats Window");

            StartCoroutine(StartNewWindow(1));
        }
        else if (pressSettings)
        {
            Debug.Log("From Settings Window");

            StartCoroutine(StartNewWindow(1));
        }
        else if (pressQuit)
        {
            Debug.Log("From Quit Window");

            StartCoroutine(StartNewWindow(1));
        }
        else if (pressUpgrades)
        {
            Debug.Log("From Upgrades Window");

            StartCoroutine(StartNewWindow(1));
        }
        else if (!pressCheats)
        {
            pressCheats = true;
            Debug.Log("Press Cheats: " + pressCheats);
        }

        if (pressCheats && !pressRestart && !pressSettings && !pressQuit && !pressUpgrades)
        {
            Debug.Log("From Nothing");

            cheatsBoxAnimator.gameObject.SetActive(true);
            cheatsBoxAnimator.SetTrigger("Start");
        }

        UpdateCheatToggleButtons();
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit 4=Upgrades
    public void PressRestart()
    {
        Debug.Log("Press Restart");

        if (pressCheats)
        {
            Debug.Log("From Cheats Window");

            StartCoroutine(StartNewWindow(2));
        }
        else if (pressSettings)
        {
            Debug.Log("From Settings Window");

            StartCoroutine(StartNewWindow(2));
        }
        else if (pressQuit)
        {
            Debug.Log("From Quit Window");

            StartCoroutine(StartNewWindow(2));
        }
        else if (pressUpgrades)
        {
            Debug.Log("From Upgrades Window");

            StartCoroutine(StartNewWindow(2));
        }
        else if (!pressRestart)
        {
            pressRestart = true;

            Debug.Log("Press Restart: " + pressRestart);
        }

        if (pressRestart && !pressCheats && !pressSettings && !pressQuit && !pressUpgrades)
        {
            Debug.Log("From Nothing");

            restartBoxAnimator.gameObject.SetActive(true);
            restartBoxAnimator.SetTrigger("Start");
        }
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit 4=Upgrades
    public void PressQuit()
    {
        Debug.Log("Press Quit");

        if (pressCheats)
        {
            Debug.Log("From Cheats Window");

            StartCoroutine(StartNewWindow(3));
        }
        else if (pressSettings)
        {
            Debug.Log("From Settings Window");

            StartCoroutine(StartNewWindow(3));
        }
        else if (pressRestart)
        {
            Debug.Log("From Restart Window");

            StartCoroutine(StartNewWindow(3));
        }
        else if (pressUpgrades)
        {
            Debug.Log("From Upgrades Window");

            StartCoroutine(StartNewWindow(3));
        }
        else if (!pressQuit)
        {
            pressQuit = true;
            Debug.Log("Press Quit: " + pressQuit);
        }

        if (pressQuit && !pressCheats && !pressSettings && !pressRestart && !pressUpgrades)
        {
            Debug.Log("From Nothing");

            restartBoxAnimator.gameObject.SetActive(true);
            restartBoxAnimator.SetTrigger("Start");
        }
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit 4=Upgrades
    public void PressSettings()
    {
        Debug.Log("Press Settings");

        if (pressCheats)
        {
            Debug.Log("From Cheats Window");

            StartCoroutine(StartNewWindow(0));
        }
        else if (pressQuit)
        {
            Debug.Log("From Quit Window");

            StartCoroutine(StartNewWindow(0));
        }
        else if (pressRestart)
        {
            Debug.Log("From Restart Window");

            StartCoroutine(StartNewWindow(0));
        }
        else if (pressUpgrades)
        {
            Debug.Log("From Upgrades Window");

            StartCoroutine(StartNewWindow(0));
        }
        else if (!pressSettings)
        {
            pressSettings = true;
            Debug.Log("Press Settings: " + pressSettings);
        }

        if (pressSettings && !pressCheats && !pressQuit && !pressRestart && !pressUpgrades)
        {
            Debug.Log("From Nothing");

            settingsBoxAnimator.gameObject.SetActive(true);
            settingsBoxAnimator.SetTrigger("Start");
        }
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit 4=Upgrades
    public void PressUpgrades()
    {
        Debug.Log("Press Upgrades");

        if (pressCheats)
        {
            Debug.Log("From Cheats Window");

            StartCoroutine(StartNewWindow(4));
        }
        else if (pressQuit)
        {
            Debug.Log("From Quit Window");

            StartCoroutine(StartNewWindow(4));
        }
        else if (pressRestart)
        {
            Debug.Log("From Restart Window");

            StartCoroutine(StartNewWindow(4));
        }
        else if (pressSettings)
        {
            Debug.Log("From Settings Window");

            StartCoroutine(StartNewWindow(4));
        }
        else if (!pressUpgrades)
        {
            pressUpgrades = true;
            Debug.Log("Press Settings: " + pressUpgrades);
        }

        if (pressUpgrades && !pressCheats && !pressQuit && !pressRestart && !pressSettings)
        {
            Debug.Log("From Nothing");

            settingsBoxAnimator.gameObject.SetActive(true);
            settingsBoxAnimator.SetTrigger("Start");
        }
    }
    #endregion

    public void Quit() { SceneManager.LoadScene(0); }

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
        pressUpgrades = false;

        cheatsBoxAnimator.SetTrigger("Exit");
        restartBoxAnimator.SetTrigger("Exit");
        quitBoxAnimator.SetTrigger("Exit");
        settingsBoxAnimator.SetTrigger("Exit");
        //upgradestBoxAnimator.SetTrigger("Exit");

        Debug.Log("Exit All Windows");
    }

    // 0=Settings 1=Cheats 2=Restart 3=Quit 4=Upgrades
    public IEnumerator StartNewWindow(int id)
    {
        ExitAllWindows();

        yield return new WaitForSeconds(transitionTime);

        if (id == 0)
        {
            pressSettings = true;
            settingsBoxAnimator.gameObject.SetActive(true);
            settingsBoxAnimator.SetTrigger("Start");
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
            quitBoxAnimator.gameObject.SetActive(true);
            quitBoxAnimator.SetTrigger("Start");
        }
        else if (id == 4)
        {
            pressUpgrades = true;
            upgradestBoxAnimator.gameObject.SetActive(true);
            upgradestBoxAnimator.SetTrigger("Start");
        }
    }


    public void RestartGame()
    {
        StartCoroutine(GameManager.Instance.RestartCurrentScene(0.0f));
    }
}
