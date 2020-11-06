using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class IncreaseSpeed : Perks
{
    [SerializeField] private float addedSpeed;

    public override void Click()
    {
        if (!isUpgraded)
        {
            if (ResourceManager.Instance.curResources >= perkCost)
            {
                ResourceManager.Instance.curResources -= perkCost;

                Debug.Log("Upgraded Speed");

                playerMovement.walkSpeed += addedSpeed;
                playerMovement.sprintSpeed += addedSpeed;

                isUpgraded = true;

                UpgradesBoxManager.Instance.UpdateButton(false, isUpgraded);
            }
            else Debug.Log("Not enough resources for upgrade");
        }
    }
}

