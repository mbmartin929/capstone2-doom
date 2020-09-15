using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class IncreaseSpeed : PerksPlayer
    {
        [SerializeField] private int addedSpeed;

        [SerializeField] private UnitController player;

        [SerializeField] private PlayerMovement playerMovement;

        public void Start()
        {

        }
        // Start is called before the first frame update
        public override bool Click()
        {
            if (base.Click())
            {
                playerMovement.movementSpeed += addedSpeed;
                Debug.Log("INCREASE SPEED");
                return true;
            }
            return false;
        }
    }
}
