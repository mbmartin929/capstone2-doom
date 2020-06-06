using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun[] loadout;
    public Transform weaponParent;

    private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
    }

    void Equip(int index)
    {
        if (currentWeapon != null) Destroy(currentWeapon);

        GameObject newWeapon = Instantiate(loadout[index].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;
    }
}
