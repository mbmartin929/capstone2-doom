using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpController : MonoBehaviour
{
    public Image overlayImage;
    public GameObject player;
    protected UnitController unit;
    public enum AmmoType
    {
        Pistol, Shotgun
    }
    public AmmoType ammoType;

    public string itemName;
    public int recoverAmount;
    public GameObject weapon;

    private void Start()
    {
        unit = player.GetComponent<PlayerController>();
        overlayImage.SetEnabled(false);
    }
}
