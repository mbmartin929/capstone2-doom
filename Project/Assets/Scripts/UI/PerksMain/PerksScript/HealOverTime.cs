using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class HealOverTime : PerksPlayer
    {

        [SerializeField] private UnitController player;

        [SerializeField] private int healAmount;
        [SerializeField] private float perkDuration;
        // Start is called before the first frame update
        void Start()
        {

        }

        public override bool Click()
        {
            if (base.Click())
            {
                StartCoroutine(HealthRegen(2, perkDuration));
                Debug.Log("HEALING");
                return true;
            }
            return false;
        }


        public IEnumerator HealthRegen(int healAmount, float duration)
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
    }
}