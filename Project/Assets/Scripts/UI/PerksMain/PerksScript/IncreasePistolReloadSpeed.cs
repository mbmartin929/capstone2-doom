using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class IncreaseReloadPistolSpeed : Perks
{
    [SerializeField] private float addedSpeed;
    [SerializeField] private AnimationSpeed animationSpeed;

    public override void Click()
    {
        if (!isUpgraded)
        {
            if (ResourceManager.Instance.curResources >= perkCost)
            {
                if (animationSpeed == null) { Debug.Log("Animation Speed is null"); return; }

                Debug.Log("Upgraded Armor");

                animationSpeed.ChangeSpeed(addedSpeed);

                isUpgraded = true;

                UpgradesBoxManager.Instance.UpdateButton(false, isUpgraded);
            }
            else Debug.Log("Not enough resources for upgrade");
        }
    }
}