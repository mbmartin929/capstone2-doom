using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    // Instantiates Singleton
    public static EndGameScreen Instance { set; get; }

    public int totalSecrets = 0;
    public int totalSecretsFound = 0;

    public int totalEnemies = 0;
    public int killedEnemies = 0;
    public int enemiesGibbed = 0;

    public int healthFound;
    public int totalHealth;

    public int armorFound;
    public int totalArmor;

    public int ammoFound;
    public int totalAmmo;


    public GameObject blackOverlay;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("EndGameScreen Singleton Initialized");
    }

    public void StartEndLevelScreen()
    {
        blackOverlay.SetActive(true);
        BGM.Instance.StopBG();

        Debug.Log("End Level");

        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(4.2f);
        Application.Quit();
    }
}
