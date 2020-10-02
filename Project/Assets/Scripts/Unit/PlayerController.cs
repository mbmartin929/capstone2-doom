using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class PlayerController : UnitController
{
    public bool isDamaged;
    public bool damaged;

    public int currentHealth;
    public int currentArmor;

    public Transform weapons;

    public int rayCastLength;

    public int keyAmount = 0;

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
            //Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.tag == "Door")
            {
                DoorScript door = hit.collider.GetComponent<DoorScript>();

                //guiShow = true;
                // if (Input.GetKeyDown("e") && isOpen == false)
                // {
                //     door.ChangeDoorState(true);
                // }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (door.doorType == DoorScript.DoorType.ExitDoor)
                    {
                        if (door.keyRequirement <= keyAmount)
                        {
                            Debug.Log("Exit Door");

                        }
                        else
                        {
                            StartCoroutine(DialogueAssistant.Instance.NeedKey());
                            Debug.Log("Need Exit Key");
                        }
                    }
                }
                //else Debug.Log(door.doorType);
            }
            // else if (hit.collider.gameObject.tag == "Special Door")
            // {
            //     //guiShow = true;
            //     if (Input.GetKeyDown("e") && isOpen == false)
            //     {
            //         hit.collider.transform.GetComponent<DoorScript>().ChangeDoorState(true);
            //     }
            // }
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

            if (CurHealth <= 0)
            {
                CurHealth = 0;
                Debug.Log("Player Dies");
                StartCoroutine(GameManager.Instance.RestartCurrentScene());
            }
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
