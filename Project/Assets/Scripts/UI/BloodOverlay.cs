using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace EightDirectionalSpriteSystem
{
    public class BloodOverlay : MonoBehaviour
    {
        private RawImage image;

        public float fadeInRate = 2.9f;
        public float fadeOutRate = 0.69f;

        public int[] healthLevels;
        public float[] opacityLevels;

        public Color defaultColor;

        private float health;
        private int index;

        // Start is called before the first frame update
        void Start()
        {
            image = GetComponent<RawImage>();

            //ChangeBloodOverlayOpacity();
            //ChangePassiveBloodOverlayOpacity();
        }

        // Update is called once per frame
        void Update()
        {
            // if (Input.GetKeyDown("space"))
            // {
            //     health = GameManager.Instance.playerGo.GetComponent<PlayerController>().CurHealth;
            //     Debug.Log("Health: " + GameManager.Instance.playerGo.GetComponent<PlayerController>().CurHealth);
            //     Debug.Log("Health: " + health);
            // }
        }

        public void ChangePassiveBloodOverlayOpacity()
        {
            health = GameManager.Instance.playerGo.GetComponent<PlayerController>().CurHealth;

            if (health >= healthLevels[healthLevels.Length - 1])
            {
                //Debug.Log("Health Levels: " + healthLevels[healthLevels.Length - 1]);

                return;
            }

            int closest = healthLevels.Aggregate((x, y) => Mathf.Abs(x - health) < Mathf.Abs(y - health) ? x : y);

            int tempIndex = 0;
            foreach (int number in healthLevels)
            {
                if (closest == number) index = tempIndex;
                tempIndex++;
            }

            StartCoroutine(FadeIn());
        }

        public IEnumerator FadeIn()
        {
            Color curColor = image.color;

            //Debug.Log("Opacity Levels: " + opacityLevels[index]);

            float alpha = opacityLevels[index];

            while (Mathf.Abs(curColor.a - alpha) > 0.01f)
            {
                //Debug.Log("Fade In: " + image.color.a);
                curColor.a = Mathf.Lerp(curColor.a, alpha, fadeInRate * Time.deltaTime);
                image.color = curColor;
                yield return null;
            }
        }

        public IEnumerator PassiveFadeOut()
        {
            health = GameManager.Instance.playerGo.GetComponent<PlayerController>().CurHealth;

            if (health <= healthLevels[healthLevels.Length - 1])
            {

                //Debug.Log("IF Health Level: " + healthLevels[healthLevels.Length - 1]);
                //yield return null;
            }
            else
            {
                //Debug.Log("ELSE Health Level: " + healthLevels[healthLevels.Length - 1]);

                Color curColor = image.color;

                float alpha = opacityLevels[index];

                while (Mathf.Abs(curColor.a - 0) > 0.01f)
                {
                    //Debug.Log("Fade Out: " + image.color.a);
                    curColor.a = Mathf.Lerp(curColor.a, 0, fadeOutRate * Time.deltaTime);
                    image.color = curColor;
                    yield return null;
                }

                Color color = new Vector4(defaultColor.r, defaultColor.g, defaultColor.b, 0);
                image.color = color;
            }

            // if (health <= healthLevels[healthLevels.Length - 1])
            // {
            //     //yield return null;
            // }
            // else
            // {
            //     Debug.Log("Health Level: " + healthLevels[healthLevels.Length - 1]);

            //     int closest = healthLevels.Aggregate((x, y) => Mathf.Abs(x - health) < Mathf.Abs(y - health) ? x : y);

            //     int tempIndex = 0;
            //     int fadeOutIndex = 0;
            //     foreach (int number in healthLevels)
            //     {
            //         if (closest == number) fadeOutIndex = tempIndex + 1;
            //         tempIndex++;
            //     }

            //     if (fadeOutIndex >= opacityLevels.Length) fadeOutIndex = opacityLevels.Length - 1;

            //     Color curColor = image.color;

            //     float alpha = opacityLevels[fadeOutIndex];

            //     while (Mathf.Abs(curColor.a - alpha) > 0.01f)
            //     {
            //         //Debug.Log("Fade Out: " + image.color.a);
            //         curColor.a = Mathf.Lerp(curColor.a, alpha, fadeOutRate * Time.deltaTime);
            //         image.color = curColor;
            //         yield return null;
            //     }

            //     // Color color = new Vector4(defaultColor.r, defaultColor.g, defaultColor.b, 0);
            //     // image.color = color;
            // }
        }

        public void ChangeActiveBloodOverlayOpacity()
        {
            health = GameManager.Instance.playerGo.GetComponent<PlayerController>().CurHealth;

            int closest = healthLevels.Aggregate((x, y) => Mathf.Abs(x - health) < Mathf.Abs(y - health) ? x : y);

            int tempIndex = 0;
            foreach (int number in healthLevels)
            {
                if (closest == number) index = tempIndex;
                tempIndex++;
            }

            StartCoroutine(FadeInAndFadeOut());
        }

        public IEnumerator FadeInAndFadeOut()
        {
            Color curColor = image.color;

            //Debug.Log("Opacity Levels: " + opacityLevels[index]);

            float alpha = opacityLevels[index];

            while (Mathf.Abs(curColor.a - alpha) > 0.01f)
            {
                //Debug.Log("Fade In: " + image.color.a);
                curColor.a = Mathf.Lerp(curColor.a, alpha, fadeInRate * Time.deltaTime);
                image.color = curColor;
                yield return null;
            }
            StartCoroutine(ActiveFadeOut());
        }

        public IEnumerator ActiveFadeOut()
        {
            Color curColor = image.color;

            float alpha = opacityLevels[index];

            while (Mathf.Abs(curColor.a - 0) > 0.01f)
            {
                //Debug.Log("Fade Out: " + image.color.a);
                curColor.a = Mathf.Lerp(curColor.a, 0, fadeOutRate * Time.deltaTime);
                image.color = curColor;
                yield return null;
            }

            Color color = new Vector4(defaultColor.r, defaultColor.g, defaultColor.b, 0);
            image.color = color;
        }
    }
}
