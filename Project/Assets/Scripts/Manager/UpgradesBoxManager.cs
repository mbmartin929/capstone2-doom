using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EightDirectionalSpriteSystem;
using UnityEngine.UI;

public class UpgradesBoxManager : MonoBehaviour
{
    // Instantiates Singleton
    public static UpgradesBoxManager Instance { set; get; }

    [SerializeField] TextMeshPro titleText;
    [SerializeField] TextMeshPro costText;
    [SerializeField] TextMeshPro descriptionText;

    [SerializeField] Button upgradeButton;
    public Perks[] perks;

    private void Awake()
    {
        Instance = this;

        if (Instance == this) Debug.Log("UpgradesBoxManager Singleton Initialized");
    }

    public void UpdateTextBox(int id)
    {
        if (perks[id].isUpgraded) upgradeButton.interactable = false;
        else upgradeButton.interactable = true;

        titleText.text = "" + perks[id].perkTitle;
        costText.text = "Cost: " + perks[id].perkCost;
        descriptionText.text = "" + perks[id].perkDescription;

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(perks[id].Click);
    }

    public void UpdateButton(bool interactable, bool upgraded)
    {
        if (upgraded) upgradeButton.interactable = interactable;
        else upgradeButton.interactable = interactable;
    }
}
