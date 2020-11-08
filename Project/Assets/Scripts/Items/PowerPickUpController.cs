using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class PowerPickUpController : PickUpController
{
    public enum PowerUpType { PunchPower, WeaponPower, SpeedPower, InvulnerablePower }
    public PowerUpType powerUpType;
    public int powerUpAmount = 420;
    public float powerUpDuration = 42f;

    public int numberOfPowerUp = 0;

    void Start()
    {
        PickUpController PowerUp = new PickUpController();
        playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = ambientSound;
        audioSource.Play();

        PowerUp.itemName = "Power Up";

        EndGameScreen.Instance.totalPowerUp += numberOfPowerUp;
    }

    void LateUpdate()
    {
        if (CheckCloseToTag("Player", distanceToPickUp))
        {
            if (fraction < 1)
            {
                fraction += lerpSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, target, fraction);
            }
        }
    }

    protected void PowerUpPickUp(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // if (itemName == "Armor")
            // {
            //     playerController.RecoverArmor(recoverAmount);
            //     PickUpOverlayManager.Instance.ShieldOverlay();
            //     Debug.Log("Armor PICKED! " + recoverAmount);
            // }
            // else if (itemName == "Health")
            // {
            //     playerController.RecoverHealth(recoverAmount);

            //     Debug.Log("Health PICKED! " + recoverAmount);
            // }


            if (powerUpType == PowerUpType.PunchPower)
            {
                playerController.StartCoroutine(StartPunchPowerUp());
                PickUpOverlayManager.Instance.HealthOverlay();
            }
            else if (powerUpType == PowerUpType.WeaponPower)
            {

            }
            else if (powerUpType == PowerUpType.SpeedPower)
            {
                playerController.StartCoroutine(StartSpeedPowerUp());
                PickUpOverlayManager.Instance.HealthOverlay();
            }
            else if (powerUpType == PowerUpType.InvulnerablePower)
            {
                playerController.StartCoroutine(StartInvulnerablePowerUp());
                PickUpOverlayManager.Instance.HealthOverlay();
            }

            GameObject _pickUpSFX = new GameObject();
            _pickUpSFX.transform.position = transform.position;
            _pickUpSFX.name = "PickUp SFX";

            _pickUpSFX.AddComponent<AudioSource>();
            _pickUpSFX.GetComponent<AudioSource>().priority = 6;
            _pickUpSFX.GetComponent<AudioSource>().spatialBlend = 0.0f;
            _pickUpSFX.GetComponent<AudioSource>().volume = 0.42f;
            _pickUpSFX.GetComponent<AudioSource>().clip = pickUpSound;
            _pickUpSFX.GetComponent<AudioSource>().Play();

            Destroy(_pickUpSFX, 2.0f);

            Destroy(this.gameObject, 0.01f);
        }
    }

    IEnumerator StartInvulnerablePowerUp()
    {
        Debug.Log("Started Invulnerable Power Up");

        playerController.powerUpInvulnerable = true;

        yield return new WaitForSeconds(0);

        Debug.Log("Stopped Invulnerable Power Up");

        playerController.powerUpInvulnerable = false;
    }

    IEnumerator StartSpeedPowerUp()
    {
        Debug.Log("Started Speed Power Up");

        FirstPersonAIO playerMovement = FirstPersonAIO.Instance;
        playerMovement.walkSpeed += powerUpAmount;
        playerMovement.sprintSpeed += powerUpAmount;

        yield return new WaitForSeconds(0);

        Debug.Log("Stopped Speed Power Up");
        playerMovement.walkSpeed += powerUpAmount;
        playerMovement.sprintSpeed += powerUpAmount;
    }

    IEnumerator StartPunchPowerUp()
    {
        Debug.Log("Started Punch Power Up");
        PunchController punchController = FindWeapon("Fists").GetComponent<PunchController>();
        punchController.damage += powerUpAmount;

        yield return new WaitForSeconds(powerUpDuration);

        Debug.Log("Stopped Punch Power Up");
        punchController.damage -= powerUpAmount;

        // float timePassed = 0;
        // while (timePassed < powerUpDuration)
        // {
        //     PunchController punchController = FindWeapon("Fists").GetComponent<PunchController>();
        //     punchController.damage = powerUpAmount;

        //     timePassed += Time.deltaTime;

        //     yield return null;
        // }
    }
    IEnumerator StopPunchPowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration + 0.29f);

        Debug.Log("Stopped Punch Power Up");

        //PunchController punchController = FindWeapon("Fists").GetComponent<PunchController>();
        //punchController.damage = powerUpAmount;
    }

    private GameObject FindWeapon(string name)
    {
        GameObject objectToReturn = null;
        foreach (Transform child in AmmoInventory.Instance.gameObject.transform)
        {
            if (child.gameObject.name == name) { objectToReturn = child.gameObject; return objectToReturn; }
            // else if (name == "Pistol") objectToReturn = child.gameObject;
            // else if (name == "Shotgun") objectToReturn = child.gameObject;
            // else if (name == "Launcher") objectToReturn = child.gameObject;
            else Debug.Log("Unnamed Weapon");
        }

        return objectToReturn;
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerUpPickUp(other);

        if (other.CompareTag("Player") && numberOfPowerUp >= 1)
        {
            EndGameScreen.Instance.powerUpFound++;
        }
    }
}
