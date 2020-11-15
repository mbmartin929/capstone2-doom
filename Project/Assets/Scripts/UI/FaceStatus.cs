using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

namespace EightDirectionalSpriteSystem
{
    public class FaceStatus : MonoBehaviour
    {
        // HP = HitPoints, SHT = While Shooting, DMG = While taking damage
        // FW = Forward, L = Left, R = Right
        // Numbers are the HP ranges for the different faces

        public float lookFaceTimer = 0.2f;

        public Texture2D HP80100FW;
        public Texture2D HP80100L;
        public Texture2D HP80100R;

        public Texture2D HP6079FW;
        public Texture2D HP6079L;
        public Texture2D HP60790R;

        public Texture2D HP4059FW;
        public Texture2D HP4059L;
        public Texture2D HP4059R;

        public Texture2D HP2039FW;
        public Texture2D HP2039L;
        public Texture2D HP2039R;

        public Texture2D HP119FW;
        public Texture2D HP119L;
        public Texture2D HP119R;

        public Texture2D HP0;

        public Texture2D SHT80100;
        public Texture2D SHT6079;
        public Texture2D SHT4059;
        public Texture2D SHT2039;
        public Texture2D SHT119;

        public Texture2D DMG80100;
        public Texture2D DMG6079;
        public Texture2D DMG4059;
        public Texture2D DMG2039;
        public Texture2D DMG119;

        int pick;

        Texture2D[] imgs;
        int i;
        float timer;

        private PlayerController playerController;
        //private SpriteRenderer spriteRenderer;
        //private Image image;
        public RawImage rawImage;

        void Start()
        {
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();

            Debug.Log("Face Status found playerController");

            //spriteRenderer = GetComponent<SpriteRenderer>();
            //rawImage = GetComponent<RawImage>();
            //image = GetComponent<Image>();

            //Debug.Log(rawImage.name);

            // have every image in an array for easy access
            imgs = new Texture2D[26];

            imgs[0] = HP80100FW;
            imgs[1] = HP80100L;
            imgs[2] = HP80100R;

            imgs[3] = HP6079FW;
            imgs[4] = HP6079L;
            imgs[5] = HP60790R;

            imgs[6] = HP4059FW;
            imgs[7] = HP4059L;
            imgs[8] = HP4059R;

            imgs[9] = HP2039FW;
            imgs[10] = HP2039L;
            imgs[11] = HP2039R;

            imgs[12] = HP119FW;
            imgs[13] = HP119L;
            imgs[14] = HP119R;

            imgs[15] = HP0;

            imgs[16] = SHT80100;
            imgs[17] = SHT6079;
            imgs[18] = SHT4059;
            imgs[19] = SHT2039;
            imgs[20] = SHT119;

            imgs[21] = DMG80100;
            imgs[22] = DMG6079;
            imgs[23] = DMG4059;
            imgs[24] = DMG2039;
            imgs[25] = DMG119;
        }

        void Update()
        {
            Faces();
            rawImage.texture = imgs[pick];
        }

        public void LookFace(float faceTimer, int frontFace, int leftFace, int rightFace)
        {
            timer = faceTimer;
            if (Input.GetAxis("Mouse X") < 0)
            {
                //Code for action on mouse moving left
                pick = leftFace;
            }
            else if (Input.GetAxis("Mouse X") > 0)
            {
                //Code for action on mouse moving right
                pick = rightFace;
            }
            else pick = frontFace;
        }

        public void Faces()
        {
            timer -= Time.deltaTime;

            // modify the timers to your liking
            if (playerController.CurHealth >= 80 & playerController.CurHealth <= 100)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 16;
                }

                if (playerController.damaged == true)   // add a bool for when damaged in your health script!
                {
                    timer = 0.5f;
                    pick = 21;
                }

                if (timer < 0 & !Input.GetMouseButton(0)) LookFace(lookFaceTimer, 0, 1, 2);
            }
            if (playerController.CurHealth >= 60 & playerController.CurHealth <= 79)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 17;
                }

                if (playerController.damaged == true)
                {
                    timer = 0.5f;
                    pick = 22;
                }

                if (timer < 0 & !Input.GetMouseButton(0)) LookFace(lookFaceTimer, 3, 4, 5);

            }
            if (playerController.CurHealth >= 40 & playerController.CurHealth <= 59)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 18;
                }

                if (playerController.damaged == true)
                {
                    timer = 0.5f;
                    pick = 23;
                }

                if (timer < 0 & !Input.GetMouseButton(0)) LookFace(lookFaceTimer, 6, 7, 8);

            }
            if (playerController.CurHealth >= 20 & playerController.CurHealth <= 39)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 19;
                }

                if (playerController.damaged == true)
                {
                    timer = 0.5f;
                    pick = 24;
                }

                if (timer < 0 & !Input.GetMouseButton(0)) LookFace(lookFaceTimer, 9, 10, 11);

            }
            if (playerController.CurHealth >= 1 & playerController.CurHealth <= 19)
            {
                if (Input.GetMouseButton(0))
                {
                    timer = 1f;
                    pick = 20;
                }

                if (playerController.damaged == true)
                {
                    timer = 0.5f;
                    pick = 25;
                }

                if (timer < 0 & !Input.GetMouseButton(0)) LookFace(lookFaceTimer, 12, 13, 14);

            }
            if (playerController.CurHealth <= 0)
            {
                pick = 15;
            }
        }
    }
}