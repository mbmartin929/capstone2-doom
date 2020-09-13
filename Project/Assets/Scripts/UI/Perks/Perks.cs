using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Perks : MonoBehaviour
{
    public PlayerMovement playerStats;
    public UnitController player;

    public Image coolDownOverlay;
    public Text goldText;
    public Text costPerk;


    [SerializeField] private float perkDuration;
    [SerializeField] private int perkCost;
    private float perkCoolDown = 1f;



    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement playerStats = GetComponent<PlayerMovement>();
        UnitController player = GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + player.CurGold.ToString();
        costPerk.text = "Cost: " + perkCost.ToString();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F WORKING");
            player.CurHealth -= 10;

        }
    }

    public void IncSpeed()
    {
        if (player.CurGold >= perkCost)
        {
            player.CurGold -= perkCost;
            playerStats.movementSpeed += 5;
            StartCoroutine(CoolDownDelay());
            Debug.Log("HERE" + player.CurGold);
        }
        else
        {
            Debug.Log("NOT ENOUGH GOLD");
        }


    }

    public void Heal()
    {
        if (player.CurGold >= perkCost)
        {
            player.CurGold -= perkCost;
            StartCoroutine(HealOverTime(20, perkDuration));

            Debug.Log("HEALING!");
        }

    }

    public IEnumerator HealOverTime(int healAmount, float duration)
    {
        int amountHealead = 0;
        int healPerloop = healAmount / (int)duration;
        while (amountHealead < healAmount)
        {
            player.CurHealth += healPerloop;
            amountHealead += healPerloop;
            Debug.Log(amountHealead + " amount HEALED");
            Debug.Log(player.CurHealth);
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator CoolDownDelay()
    {
        yield return new WaitForSeconds(0.5f);
        perkCoolDown -= 0.5f;
        coolDownOverlay.GetComponent<Image>().fillAmount = perkCoolDown;
        StartCoroutine(CoolDownDelay());
    }




}
