using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : MonoBehaviour
{
    // Instantiates Singleton
    public static CheatsManager Instance { set; get; }

    public GameObject[] allWeapons;

    public bool enableUnlockAllWeapons = false;
    public bool enableUnlimitedAmmo = false;
    public bool enableGodMode = false;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("CheatsManager Singleton Initialized");
    }

    public void PressUnlockAllWeapons()
    {
        enableUnlockAllWeapons = !enableUnlockAllWeapons;
        if (enableUnlockAllWeapons)
        {
            // Code when enabled
            Debug.Log("Turn On Equip All Weapons");
            PauseManager.Instance.UpdateCheatToggleButtons();

            foreach (GameObject weapon in allWeapons)
            {
                GameObject pickUp = Instantiate(weapon, GameManager.Instance.playerGo.transform.position, Quaternion.identity);
            }
        }
        else if (!enableUnlockAllWeapons)
        {
            // Code when disabled
            Debug.Log("Turn Off Equip All Weapons");
            PauseManager.Instance.UpdateCheatToggleButtons();
        }
    }

    public void PressUnlimitedAmmo()
    {
        enableUnlimitedAmmo = !enableUnlimitedAmmo;
        if (enableUnlimitedAmmo)
        {
            // Code when enabled
            Debug.Log("Turn On Unlimited Ammo");
            PauseManager.Instance.UpdateCheatToggleButtons();
        }
        else if (!enableUnlimitedAmmo)
        {
            // Code when disabled
            Debug.Log("Turn Off Unlimited Ammo");
            PauseManager.Instance.UpdateCheatToggleButtons();
        }
    }

    public void PressGodMode()
    {
        enableGodMode = !enableGodMode;
        if (enableGodMode)
        {
            // Code when enabled
            Debug.Log("Turn On God Mode");
            PauseManager.Instance.UpdateCheatToggleButtons();
        }
        else if (!enableGodMode)
        {
            // Code when disabled
            Debug.Log("Turn Off God Mode");
            PauseManager.Instance.UpdateCheatToggleButtons();
        }
    }
}
