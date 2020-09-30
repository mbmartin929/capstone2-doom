using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EightDirectionalSpriteSystem;

namespace PSX
{
    public class PlayerDeath : MonoBehaviour
    {
        public float fadeInTime;
        public float fadeOutTime;
        public float waitTime = 2.0f;

        public float targetHeightPixelation;
        public float defaultHeightPixelation;

        public float targetWidthPixelation;
        public float defaultWidthPixelation;

        [SerializeField] private VolumeProfile volumeProfile;
        private PixelationController pixelationController;
        private Pixelation pixelation;
        private Dithering dithering;
        private Fog fog;

        private bool dead = false;

        // Start is called before the first frame update
        void Start()
        {
            volumeProfile.TryGet<Pixelation>(out pixelation);
            pixelationController = GetComponent<PixelationController>();

            // defaultHeightPixelation = pixelationController.heightPixelation;
            // defaultWidthPixelation = pixelationController.widthPixelation;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return)) dead = true;

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // pixelationController.heightPixelation = Mathf.Lerp(targetHeightPixelation, defaultHeightPixelation, fadeOutTime * Time.deltaTime);
                // pixelationController.widthPixelation = Mathf.Lerp(targetWidthPixelation, defaultWidthPixelation, fadeOutTime * Time.deltaTime);

                pixelation.heightPixelation.value = Mathf.Lerp(defaultHeightPixelation, targetHeightPixelation, fadeInTime * Time.deltaTime);
            }

            if (dead)
            {
                pixelationController.heightPixelation = Mathf.Lerp(defaultHeightPixelation, targetHeightPixelation, fadeInTime * Time.deltaTime);
                pixelationController.widthPixelation = Mathf.Lerp(defaultWidthPixelation, targetWidthPixelation, fadeInTime * Time.deltaTime);

                //pixelation.heightPixelation.value = Mathf.Lerp(defaultHeightPixelation, targetHeightPixelation, fadeInTime * Time.deltaTime);
            }
        }

        private IEnumerator FadeDeathLerp()
        {
            //pixelation.heightPixelation.value = Mathf.Lerp(defaultHeightPixelation, targetHeightPixelation, fadeInTime);


            yield return new WaitForSeconds(waitTime);

            //pixelation.heightPixelation.value = Mathf.Lerp(targetHeightPixelation, defaultHeightPixelation, fadeOutTime);
        }
    }
}
