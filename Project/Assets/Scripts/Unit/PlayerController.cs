using System.Collections;
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

        public int currentGold;


        //[Header("Player Attributes")]
        //public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        //[Header("Player Skills Enabled")]
        //public List<Perk> playerSkills = new List<Perk>();
        // Start is called before the first frame update

        [SerializeField]
        private BloodOverlay bloodOverlay;
        [SerializeField]
        private BloodOverlay passiveBloodOverlay;

        void Start()
        {
            CurHealth = maxHealth;
            //Debug.Log(CurHealth);

            CurArmor = maxArmor;
            CurGold = currentGold;

            TextManager.Instance.UpdateHealthArmorText();
        }

        // Update is called once per frame
        void Update()
        {
            currentHealth = CurHealth;
            currentArmor = CurArmor;

            currentGold = CurGold;
            playerRayCast();
        }

        public void RecoverHealth(int amount)
        {
            CurHealth += amount;
            StartCoroutine(passiveBloodOverlay.PassiveFadeOut());
        }

        public void RecoverArmor(int amount)
        {
            //Debug.Log("Amount: " + amount);
            //Debug.Log("Before: " + CurArmor);
            CurArmor += amount;
            //Debug.Log("After: " + CurArmor);
        }

        private void playerRayCast()
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
        public void GetPerk(int amount)
        {
            CurGold -= amount;
        }
        public void TakeDamage(int amount)
        {
            if (damaged)
            {
                Debug.Log("Recently taken damage. Negating Damage");
            }
            else
            {
                bloodOverlay.ChangeActiveBloodOverlayOpacity();
                passiveBloodOverlay.ChangePassiveBloodOverlayOpacity();

                if (CurArmor > 0)
                {
                    //Debug.Log("Armor Damage");
                    CurArmor -= amount;
                    //if (CurArmor <= -1) CurArmor = 0;
                }
                else
                {
                    //Debug.Log("Health Damage");
                    CurHealth -= amount;
                    //if (CurHealth <= 0) CurHealth = 0;
                }

                TextManager.Instance.UpdateHealthArmorText();

                StartCoroutine(GetDamaged());
            }
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