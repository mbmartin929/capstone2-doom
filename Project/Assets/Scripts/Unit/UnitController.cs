﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class UnitController : MonoBehaviour
    {
        // References Singleton
        protected GameManager gameManager;

        #region Variables
        [Header("Game Objects")]
        public GameObject projectileGo;

        [Header("Stats")]
        public int maxHealth;
        private int curHealth;
        public int maxArmor;
        private int curArmor;


        [Header("TEST GOLD")]
        private int curGold;


        #endregion

        void Awake()
        {

        }

        void Start()
        {
            curHealth = maxHealth;
            curArmor = maxArmor;
        }

        public int CurHealth
        {
            get { return curHealth; }
            set
            {
                curHealth = value;
                //if (curHealth < 0) curHealth = 0;
                if (curHealth > maxHealth) curHealth = maxHealth;
            }
        }

        public int CurArmor
        {
            get { return curArmor; }
            set
            {
                curArmor = value;
                if (curArmor < 0) curArmor = 0;
                if (curArmor > maxArmor) curArmor = maxArmor;
            }
        }

        public int CurGold
        {
            get { return curGold; }
            set
            {
                curGold = value;
                if (curGold < 0) curGold = 0;
                PerkTree.Instance.UpdateGoldText();
            }
        }

        public bool IsDead()
        {
            if (CurHealth <= 0) return true;
            else return false;
        }

        public void getPerk(int amount)
        {
            curGold -= amount;
        }
        public void RestoreHealth(int amount)
        {
            curHealth += amount;
        }

        public void RestoreArmor(int amount)
        {
            curArmor += amount;
        }
    }
}