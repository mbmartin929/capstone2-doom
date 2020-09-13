using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkTree : MonoBehaviour
{
    // Instantiates Singleton
    public static PerkTree Instance { set; get; }

    [SerializeField]
    private PerksPlayer[] perks;

    [SerializeField]
    private PerksPlayer[] unlockedByDefault;

    [SerializeField]
    private UnitController player;

    [SerializeField]
    private Text goldText;

    void Awake()
    {
        // Sets Singleton
        Instance = this;
        if (Instance == this) Debug.Log("Perks Singleton Initialized");

        UnitController player = GameManager.Instance.playerGo.GetComponent<UnitController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetPerks();
    }

    public void buyPerk(PerksPlayer playerPerk)
    {
        Debug.Log("WORKING");
        if (player.CurGold > 0 && playerPerk.Click())
        {
            player.CurGold--;
        }
        if (player.CurGold == 0)
        {
            foreach (PerksPlayer p in perks)
            {
                if (p.MyCurrentCount == 0)
                {
                    p.LockPerk();
                }
            }
        }
    }
    private void ResetPerks()
    {
        UpdateGoldText();
        foreach (PerksPlayer perk in perks)
        {
            perk.LockPerk();
        }

        foreach (PerksPlayer perk in unlockedByDefault)
        {
            perk.unlockPerk();
        }
    }

    public void UpdateGoldText()
    {

        goldText.text = player.CurGold.ToString();

    }
}
