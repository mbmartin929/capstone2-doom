using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class EnemySounds : MonoBehaviour
    {
        public float minPatrolSoundTime = 1.5f;
        public float maxPatrolSoundTime = 5.0f;

        public AudioClip[] idle;
        public AudioClip[] chase;
        public AudioClip[] fire;
        public AudioClip[] pain;
        public AudioClip[] death;
        public AudioClip[] splatter;

        private AudioSource audioSource;
        private GameObject emptyObject;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SlimeChaseSound()
        {
            audioSource.clip = chase[0];
            audioSource.Play();
        }

        public void SlimeChaseSoudOneShot()
        {
            audioSource.PlayOneShot(chase[0]);
        }

        public void PainSound()
        {
            int random = Random.Range(0, pain.Length);
            audioSource.clip = pain[random];
            audioSource.Play();

            Debug.Log("Play Pain Sound");
        }

        public void BloodSplatterSound()
        {
            int random = Random.Range(0, splatter.Length);
            emptyObject = new GameObject();

            GameObject BloodSplatterSFX = Instantiate(emptyObject, transform.position, transform.rotation);
            BloodSplatterSFX.AddComponent<AudioSource>();
            BloodSplatterSFX.GetComponent<AudioSource>().clip = splatter[random];
            BloodSplatterSFX.GetComponent<AudioSource>().Play();

            // audioSource.clip = splatter[random];
            // audioSource.Play();

            Debug.Log("Play Blood Splatter Sound");
        }
    }
}