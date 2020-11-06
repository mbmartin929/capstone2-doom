using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class IncreaseArmor : Perks
{
    [SerializeField] private int addedArmor;

    public override void Click()
    {
        if (!isUpgraded)
        {
            if (ResourceManager.Instance.curResources >= perkCost)
            {
                ResourceManager.Instance.curResources -= perkCost;

                Debug.Log("Upgraded Armor");

                playerController.maxArmor += addedArmor;
                playerController.CurArmor += addedArmor;

                isUpgraded = true;

                UpgradesBoxManager.Instance.UpdateButton(false, isUpgraded);
            }
            else Debug.Log("Not enough resources for upgrade");
        }
    }
}