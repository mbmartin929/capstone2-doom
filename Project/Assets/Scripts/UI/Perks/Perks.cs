using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EightDirectionalSpriteSystem
{
    public class Perks : MonoBehaviour
    {
        protected FirstPersonAIO playerMovement;
        protected PlayerController playerController;
        protected Button upgradeButton;

        public bool isUpgraded = false;

        // public Image coolDownOverlay;
        // public Text goldText;
        // public Text costPerk;


        // [SerializeField] private float addAmount = 2.9f;
        // [SerializeField] private float perkDuration;

        public int perkCost;
        public string perkTitle;
        public string perkDescription;
        private float perkCoolDown = 1f;
        //private float currency

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            playerMovement = GameManager.Instance.playerGo.GetComponent<FirstPersonAIO>();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>(); 
            //UnitController player = GetComponent<UnitController>();
        }

        public virtual void Click()
        {

        }

        // }

        // public void Heal()
        // {
        //     if (player.CurGold >= perkCost)
        //     {
        //         player.CurGold -= perkCost;
        //         StartCoroutine(HealOverTime(20, perkDuration));

        //         Debug.Log("HEALING!");
        //     }
        // }

        // public IEnumerator HealOverTime(int healAmount, float duration)
        // {
        //     int amountHealead = 0;
        //     int healPerloop = healAmount / (int)duration;
        //     while (amountHealead < healAmount)
        //     {
        //         player.CurHealth += healPerloop;
        //         amountHealead += healPerloop;
        //         Debug.Log(amountHealead + " amount HEALED");
        //         Debug.Log(player.CurHealth);
        //         yield return new WaitForSeconds(1f);

        //     }
        // }

        // IEnumerator CoolDownDelay()
        // {
        //     yield return new WaitForSeconds(0.5f);
        //     perkCoolDown -= 0.5f;
        //     coolDownOverlay.GetComponent<Image>().fillAmount = perkCoolDown;
        //     StartCoroutine(CoolDownDelay());
        // }
    }
}
