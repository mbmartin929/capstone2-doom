using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class PlayerController : UnitController
{
    public int startingHealth = 50;
    public int startingArmor = 10;

    public bool isDamaged;
    public bool damaged;

    public Transform weapons;

    public float rayCastLength;

    public int keyAmount = 0;

    public bool fromRestart = false;

    [HideInInspector] public bool powerUpInvulnerable = false;

    public GameObject gameOverScreen;
    public Transform launcherPos;

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

    private PlayerController restartPlayer;
    [HideInInspector] public GameObject restartPlayerGo;

    void Start()
    {
        //Debug.Log("GAME MANAGER PLAYER INSTANCE");

        CurHealth = startingHealth;
        CurArmor = startingArmor;

        CurGold = currentGold;

        TextManager.Instance.UpdateHealthArmorText();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.playerGo = this.gameObject;
        //Debug.Log("UPDATE GAME MANAGER PLAYER INSTANCE");

        currentGold = CurGold;
        playerRayCast();
    }

    public void SetBeginningLevelStats()
    {
        //restartPlayer = this;
        restartPlayerGo = gameObject;
    }

    public void Restart()
    {
        //this = restartPlayer;
        //gameObject = restartPlayerGo;

    }

    public void Destroy()
    {
        Destroy(gameObject);
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
            DoorScript door = null;
            //Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.tag == "Door")
            {
                door = hit.collider.GetComponent<DoorScript>();

                //guiShow = true;
                // if (Input.GetKeyDown("e") && isOpen == false)
                // {
                //     door.ChangeDoorState(true);
                // }


                // Instantiate "E"
                //door.isNear = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (door.doorType == DoorScript.DoorType.ExitDoor)
                    {
                        if (door.keyRequirement <= keyAmount)
                        {
                            Debug.Log("Exit Door");
                            keyAmount -= 1;
                            EndGameScreen.Instance.StartEndLevelScreen();
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
            else
            {
                // if (door == null) { }
                // else door.isNear = true;
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
            if (powerUpInvulnerable) { Debug.Log("Take No Damage From Power Ups"); return; }
            else if (CheatsManager.Instance.enableGodMode) { Debug.Log("Take No Damage From Cheats"); return; }
            else if (EndGameScreen.Instance.active) { Debug.Log("Take No Damage From EndGameScreen"); return; }
            else
            {
                bloodOverlay.ChangeActiveBloodOverlayOpacity();
                passiveBloodOverlay.ChangePassiveBloodOverlayOpacity();

                if (CurArmor > 0)
                {
                    //Debug.Log("Armor Damage");

                    int armorDamage = amount / 2;
                    int healthDamage = amount - armorDamage;

                    CurArmor -= armorDamage;
                    CurHealth -= healthDamage;

                    Debug.Log("Armor Damage: " + armorDamage);
                    Debug.Log("Health Damage: " + healthDamage);
                }
                else
                {
                    //Debug.Log("Health Damage");
                    CurHealth -= amount;

                    Debug.Log("Health Damage: " + amount);
                    //if (CurHealth <= 0) CurHealth = 0;
                }

                TextManager.Instance.UpdateHealthArmorText();

                StartCoroutine(GetDamaged());

                if (CurHealth <= 0)
                {
                    Debug.Log("Player Dies");
                    CurHealth = 0;
                    gameOverScreen.SetActive(true);
                    gameOverScreen.GetComponent<GameOverScreen>().StartGameOver();
                    gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                    GetComponent<FirstPersonAIO>().ControllerPause();
                    GetComponent<FirstPersonAIO>().enabled = false;
                    //Time.timeScale = 0;

                    transform.GetChild(3).gameObject.SetActive(false);


                    Debug.Log("Paused");

                    //StartCoroutine(GameManager.Instance.RestartCurrentScene());
                }
            }
        }
    }

    private IEnumerator GetDamaged()
    {
        damaged = true;
        yield return new WaitForSeconds(0.27f);
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
