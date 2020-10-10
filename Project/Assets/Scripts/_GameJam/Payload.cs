using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class Payload : UnitController
{
    public GameObject[] waypoints;
    public float radius = 2f;
    public float startTime = 2.9f;
    public int current = 0;
    public float rotSpeed;

    public float defaultMoveSpeed = 6.9f;
    public float curMoveSpeed = 1.0f;
    float waypointRadius = 1f;

    public bool startMove = false;

    [SerializeField] public Transform middle;

    public void TakeDamage(int amount)
    {
        //bloodOverlay.ChangeActiveBloodOverlayOpacity();
        //passiveBloodOverlay.ChangePassiveBloodOverlayOpacity();

        if (CurArmor > 0)
        {
            int armorDamage = amount / 2;
            int healthDamage = amount - armorDamage;

            CurArmor -= armorDamage;
            CurHealth -= healthDamage;

            Debug.Log("Armor Damage: " + armorDamage);
            Debug.Log("Health Damage: " + healthDamage);
        }
        else
        {
            CurHealth -= amount;

            Debug.Log("Health Damage: " + amount);
        }

        TextManager.Instance.UpdateHealthArmorText();

        if (CurHealth <= 0)
        {
            Debug.Log("Player Dies");
            CurHealth = 0;
            //gameOverScreen.SetActive(true);

            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            GetComponent<FirstPersonAIO>().ControllerPause();
            GetComponent<FirstPersonAIO>().enabled = false;

            transform.GetChild(3).gameObject.SetActive(false);

            Debug.Log("Paused");

            //StartCoroutine(GameManager.Instance.RestartCurrentScene());
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WillMove(startTime));

        curMoveSpeed = defaultMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove)
        {
            if (Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
            {
                current++;
                //if (current >= waypoints.Length) current = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * curMoveSpeed);

            Vector3 targetDirection = waypoints[current].transform.position - transform.position;

            float singleStep = rotSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.5f);

            transform.localRotation = Quaternion.LookRotation(newDirection);
        }

        if ((Vector3.Distance(middle.position, GameManager.Instance.playerGo.transform.position) < radius))
        {
            startMove = true;
        }
        else startMove = false;
    }

    private IEnumerator WillMove(float time)
    {
        yield return new WaitForSeconds(time);
        startMove = true;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }

}
