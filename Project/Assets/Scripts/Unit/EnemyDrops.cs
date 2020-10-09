using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class EnemyDrops : MonoBehaviour
    {
        [Header("What Enemy Drops")]
        public GameObject[] drops;
        public GameObject[] healthDrops;
        public GameObject[] armorDrops;
        public GameObject[] pistolAmmoDrops;
        public GameObject[] shotgunAmmoDrops;

        [Header("Amount of Drops while Player is at Normal Health")]
        public int minAmountOfDropsNormal = 0;
        public int maxAmountOfDropsNormal = 4;

        [Header("Amount of Drops while Player is at Low Health")]
        public int minAmountOfDropsLow = 5;
        public int maxAmountOfDropsLow = 10;

        [Header("Amount of Drops while PistolAmmo is at Low")]
        public int minAmountOfPistolAmmoNormal = 3;
        public int maxAmountOfPistolAmmoNormal = 6;

        [Header("Amount of Drops while Shotgun is at Low")]
        public int minAmountOfShotgunAmmoNormal = 2;
        public int maxAmountOfShotgunAmmoNormal = 4;

        [Header("Player's Low Values")]
        public int lowHealth = 40;
        public int lowArmor = 10;
        public int lowPistolAmmo = 15;
        public int lowShotgunAmmo = 9;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Drop()
        {
            int index;
            int amount;

            if (GameManager.Instance.playerGo.GetComponent<PlayerController>().CurHealth <= lowHealth)
            {
                if (Random.value > 0.5)
                {
                    Debug.Log("Low Health! Will drop lots of health");

                    amount = Random.Range(minAmountOfDropsLow, maxAmountOfDropsLow);
                    Debug.Log("Amount: " + amount);

                    for (int i = 0; i <= amount; i++)
                    {
                        index = Random.Range(0, healthDrops.Length - 1);

                        GameObject healthDrop = Instantiate(healthDrops[index], transform.position, Quaternion.identity);
                        healthDrop.GetComponent<ItemExplosion>().isExplode = true;
                    }
                }
                else Debug.Log("Low Health! Will drop nothing");
            }
            else if (AmmoInventory.Instance.curPistolAmmo <= lowPistolAmmo)
            {
                if (Random.value > 0.5)
                {
                    Debug.Log("Low Pistol Ammo! Will drop lots of pistol ammo");

                    amount = Random.Range(minAmountOfPistolAmmoNormal, maxAmountOfPistolAmmoNormal);
                    Debug.Log("Amount: " + amount);

                    for (int i = 0; i <= amount; i++)
                    {
                        index = Random.Range(0, pistolAmmoDrops.Length - 1);

                        GameObject pistolAmmoDrop = Instantiate(pistolAmmoDrops[index], transform.position, Quaternion.identity);
                        pistolAmmoDrop.GetComponent<ItemExplosion>().isExplode = true;

                        pistolAmmoDrop.transform.GetChild(0).GetComponent<AmmoPickUp>().recoverAmount = Random.Range(1, 6);
                    }
                }
                else Debug.Log("Low Pistol Ammo! Will drop nothing");


            }
            else if (AmmoInventory.Instance.curShotgunAmmo <= lowShotgunAmmo)
            {
                Debug.Log("Low Shotgun Ammo! Will drop lots of shotgun ammo");

                amount = Random.Range(minAmountOfShotgunAmmoNormal, maxAmountOfShotgunAmmoNormal);
                Debug.Log("Amount: " + amount);

                for (int i = 0; i <= amount; i++)
                {
                    index = Random.Range(0, shotgunAmmoDrops.Length - 1);

                    GameObject shotgunAmmoDrop = Instantiate(shotgunAmmoDrops[index], transform.position, Quaternion.identity);
                    shotgunAmmoDrop.GetComponent<ItemExplosion>().isExplode = true;

                    shotgunAmmoDrop.transform.GetChild(0).GetComponent<AmmoPickUp>().recoverAmount = Random.Range(1, 3);
                }
            }
            else
            {
                // Debug.Log("Will drop anything");

                // amount = Random.Range(minAmountOfDropsNormal, maxAmountOfDropsNormal);
                // Debug.Log("Amount: " + amount);

                // for (int i = 0; i <= amount; i++)
                // {
                //     index = Random.Range(0, drops.Length - 1);

                //     GameObject itemDrop = Instantiate(drops[index], transform.position, Quaternion.identity);
                //     itemDrop.GetComponent<ItemExplosion>().isExplode = true;
                // }


                if (Random.value > 0.7) //%30 percent chance (1 - 0.7 is 0.3)
                {
                    Debug.Log("Will drop anything");

                    amount = Random.Range(minAmountOfDropsNormal, maxAmountOfDropsNormal);
                    //Debug.Log("Amount: " + amount);

                    for (int i = 0; i <= amount; i++)
                    {
                        index = Random.Range(0, drops.Length - 1);

                        GameObject itemDrop = Instantiate(drops[index], transform.position, Quaternion.identity);
                        itemDrop.GetComponent<ItemExplosion>().isExplode = true;
                    }
                }
                else Debug.Log("Will drop nothing");
            }
        }
    }
}