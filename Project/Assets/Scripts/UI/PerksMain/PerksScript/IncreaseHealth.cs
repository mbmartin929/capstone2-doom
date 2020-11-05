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
                Debug.Log("Upgraded");
                //Debug.Log("Before: " + playerMovement.walkSpeed);
                playerMovement.walkSpeed += addedSpeed;
                playerMovement.sprintSpeed += addedSpeed;
                //Debug.Log("After: " + playerMovement.walkSpeed);

                isUpgraded = true;

                UpgradesBoxManager.Instance.UpdateButton(false, isUpgraded);
            }
            else Debug.Log("Not enough resources for upgrade");
        }
    }
}

