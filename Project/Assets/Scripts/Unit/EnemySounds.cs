﻿using System.Collections;
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

        public float painSound = 29.0f;

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

            //float defaultVolume = audioSource.volume;
            //audioSource.volume = 1.0f;

            //audioSource.clip = pain[random];
            //audioSource.Play();

            //audioSource.PlayOneShot(pain[random], 1f);

            //audioSource.volume = defaultVolume;

            // emptyObject = new GameObject();

            // GameObject PainSFX = Instantiate(emptyObject, transform.position, transform.rotation);
            // Destroy(emptyObject, 0.0f);

            // PainSFX.AddComponent<AudioSource>();
            // PainSFX.GetComponent<AudioSource>().priority = 42;
            // PainSFX.GetComponent<AudioSource>().reverbZoneMix = 0f;
            // PainSFX.GetComponent<AudioSource>().spatialBlend = 0.0f;
            // PainSFX.GetComponent<AudioSource>().volume = 1;
            // PainSFX.GetComponent<AudioSource>().clip = pain[random];
            // PainSFX.GetComponent<AudioSource>().Play();
            // Destroy(PainSFX, 2.0f);

            AudioClip clip = pain[random];

            AudioSource.PlayClipAtPoint(clip, transform.position, painSound);

            Debug.Log("Play Pain Sound");
        }

        public void BloodSplatterSound()
        {
            int random = Random.Range(0, splatter.Length);
            emptyObject = new GameObject();

            GameObject BloodSplatterSFX = Instantiate(emptyObject, transform.position, transform.rotation);
            Destroy(emptyObject, 0.0f);

            BloodSplatterSFX.AddComponent<AudioSource>();
            BloodSplatterSFX.GetComponent<AudioSource>().reverbZoneMix = 1.1f;
            BloodSplatterSFX.GetComponent<AudioSource>().spatialBlend = Random.Range(0.95f, 1.0f);
            BloodSplatterSFX.GetComponent<AudioSource>().volume = Random.Range(0.42f, 0.8f);
            BloodSplatterSFX.GetComponent<AudioSource>().clip = splatter[random];
            BloodSplatterSFX.GetComponent<AudioSource>().Play();
            Destroy(BloodSplatterSFX, 2.0f);

            // audioSource.clip = splatter[random];
            // audioSource.Play();

            Debug.Log("Play Blood Splatter Sound");
        }
    }
}