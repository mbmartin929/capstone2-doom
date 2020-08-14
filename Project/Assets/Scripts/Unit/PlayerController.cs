﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class PlayerController : UnitController
    {
        public bool isDamaged;
        public bool damaged;

        public int currentHealth;
        public int currentArmor;

        public Transform weapons;

        public int rayCastLength;

        [Header("Door function")]
        bool guiShow = false;
        bool isOpen = false;

        // Start is called before the first frame update
        void Start()
        {
            CurHealth = maxHealth;
            CurArmor = maxArmor;

            TextManager.Instance.UpdateHealthArmorText();
        }

        // Update is called once per frame
        void Update()
        {
            currentHealth = CurHealth;
            currentArmor = CurArmor;

            playerRayCast();
        }

        void playerRayCast()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, rayCastLength))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.gameObject.tag == "Door")
                {
                    guiShow = true;
                    if (Input.GetKeyDown("e") && isOpen == false)
                    {
                        hit.collider.transform.GetComponent<DoorScript>().ChangeDoorState(true);
                    }
                }
            }
            else
            {
                guiShow = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy Attack"))
            {
                GetDamaged();
            }
        }

        public void TakeDamage(int amount)
        {
            if (CurArmor > 0)
            {
                //Debug.Log("Armor Damage");
                CurArmor -= amount;
            }
            else
            {
                //Debug.Log("Health Damage");
                CurHealth -= amount;
            }

            StartCoroutine(GetDamaged());
        }

        private IEnumerator GetDamaged()
        {
            damaged = true;
            yield return new WaitForSeconds(0.15f);
            damaged = false;
        }

        void OnGUI()
        {
            //DOOR
            if (guiShow == true && isOpen == false)
            {
                GUI.Box(new Rect(Screen.width / 3, Screen.height / 3, 150, 50), "PRESS " + "E" + " Open Door");
            }
        }
    }
}