using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class IncreaseShotgunAmmo : Perks
{
    [SerializeField] private int addedAmmo;

    public override void Click()
    {
        if (!isUpgraded)
        {
            if (ResourceManager.Instance.curResources >= perkCost)
            {
                ResourceManager.Instance.curResources -= perkCost;

                Debug.Log("Upgraded Armor");

                AmmoInventory.Instance.maxShotgunCapacity += addedAmmo;
                AmmoInventory.Instance.curShotgunAmmo += addedAmmo;

                isUpgraded = true;

                UpgradesBoxManager.Instance.UpdateButton(false, isUpgraded);
            }
            else Debug.Log("Not enough resources for upgrade");
        }
    }
}