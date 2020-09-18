using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class WeaponIdleAnim : MonoBehaviour
    {

        private WeaponSwitching weaponSwitching;

        private GameObject weaponGo;

        void Awake()
        {
            weaponSwitching = GetComponent<WeaponSwitching>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
                {
                    weaponGo = gameObject.transform.GetChild(i).gameObject;
                    if (weaponGo.name == "Pistol")
                    {

                    }
                    //weaponGo.GetComponent<WeaponController>().;
                }
            }
        }
    }
}
