using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EightDirectionalSpriteSystem
{
    public class FaceStatusBar : MonoBehaviour
    {
        public Transform enemy;
        public GameObject player;
        public PlayerController unit;
        Texture2D[] faces;
        Image randomImage;
        bool doEvilGrin;
        int pick;
        float timer;
        int i;


        public float muchPain;
        float previousHealth;
        [Header("HP 100 - 80")]
        public Texture2D HP80100FW;
        public Texture2D HP80100L;
        public Texture2D HP80100R;
        [Header("HP 79 - 60")]
        public Texture2D HP60100FW;
        public Texture2D HP60100L;
        public Texture2D HP60100R;
        [Header("HP 59 - 40")]
        public Texture2D HP40100FW;
        public Texture2D HP40100L;
        public Texture2D HP40100R;
        [Header("HP 39 - 20")]
        public Texture2D HP20100FW;
        public Texture2D HP20100L;
        public Texture2D HP20100R;
        [Header("HP 19 - 1")]
        public Texture2D HP1100FW;
        public Texture2D HP1100L;
        public Texture2D HP1100R;
        [Header("Picked up Weapon")]
        public Texture2D HP80100FWeaponPick;
        public Texture2D HP60100FWeaponPick;
        public Texture2D HP40100FWeaponPick;
        public Texture2D HP20100FWeaponPick;
        public Texture2D HP1100FWeaponPick;


        [Header("HP <= 0")]
        public Texture2D HPDead;



        //Randomize face 
        //Face follow Enemy when hit (indicator where the enemy is)
        //if 20% hp is deducted ouchFace
        //DO Evil grin when picked up a weapon
        // Start is called before the first frame update
        void Start()
        {
            unit = player.GetComponent<PlayerController>();
            faces = new Texture2D[16];

            faces[0] = HP80100FW;
            faces[1] = HP80100L;
            faces[2] = HP80100R;

            faces[3] = HP60100FW;
            faces[4] = HP60100L;
            faces[5] = HP60100R;

            faces[6] = HP40100FW;
            faces[7] = HP40100L;
            faces[8] = HP40100R;

            faces[9] = HP20100FW;
            faces[10] = HP20100L;
            faces[11] = HP20100R;

            faces[12] = HP1100FW;
            faces[13] = HP1100L;
            faces[14] = HP1100R;

            faces[15] = HPDead;
        }

        // Update is called once per frame
        void Update()
        {
            ChangeFace();
        }

        void ChangeFace()
        {
            timer -= Time.deltaTime;
            if (unit.CurHealth >= 80 && unit.CurHealth <= 100)
            {
                if (Input.GetMouseButton(0)) //straight face
                {
                    Debug.Log("FWD Face 100");

                    timer = 1f;
                    pick = 0;
                }
                if (unit.isDamaged == true)
                {
                    timer = 0.5f;

                    //pick = damaged face;
                    //Calculate where the enemy is
                }
                if (timer < 0 & !Input.GetMouseButton(0))
                {
                    Debug.Log("Left Click");

                    timer = 1f;
                    i = pick;
                    while (i == pick)
                    {
                        pick = Random.Range(0, 3);
                    }
                }
            }
            if (unit.CurHealth >= 60 && unit.CurHealth <= 79)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 3;
                }
                if (unit.isDamaged == true)
                {
                    timer = 0.5f;
                    //pick = damaged face;
                }
                if (timer < 0 & !Input.GetMouseButton(0))
                {
                    timer = 1f;
                    i = pick;
                    while (i == pick)
                    {
                        pick = Random.Range(3, 6);

                    }

                }
            }
            if (unit.CurHealth >= 40 && unit.CurHealth <= 59)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 6;
                }
                if (unit.isDamaged == true)
                {
                    timer = 0.5f;
                    //pick = damaged face;
                }
                if (timer < 0 & !Input.GetMouseButton(0))
                {
                    timer = 1f;
                    i = pick;
                    while (i == pick)
                    {
                        pick = Random.Range(6, 9);

                    }

                }
            }
            if (unit.CurHealth >= 20 && unit.CurHealth <= 39)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 9f;
                    pick = 0;
                }
                if (unit.isDamaged == true)
                {
                    timer = 0.5f;
                    //pick = damaged face;
                }
                if (timer < 0 & !Input.GetMouseButton(0))
                {
                    timer = 1f;
                    i = pick;
                    while (i == pick)
                    {
                        pick = Random.Range(9, 12);

                    }

                }
            }
            if (unit.CurHealth >= 1 && unit.CurHealth <= 19)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 12;
                }
                if (unit.isDamaged == true)
                {
                    timer = 0.5f;
                    //pick = damaged face;
                }
                if (timer < 0 & !Input.GetMouseButton(0))
                {
                    timer = 1f;
                    i = pick;
                    while (i == pick)
                    {
                        pick = Random.Range(12, 5);

                    }

                }
            }
            if (unit.CurHealth <= 0)
            {
                pick = 15;
            }
        }

        void calcWhereEnemy(Texture2D charFace)
        {
            Vector3 targetDir = enemy.position - transform.position;
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            //float angle = Vector3.Angle(targetDir, forward);
            if (Vector3.Dot(forward, targetDir) > 0)
            {
                charFace = faces[pick];
                //turn right face
            }
            else if (Vector3.Dot(forward, targetDir) < 0)
            {
                charFace = faces[pick];
                //turn left face
            }
        }

        private void OnGUI()
        {
            //GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), faces[pick]);
        }
    }
}