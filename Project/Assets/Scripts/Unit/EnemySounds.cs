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

        private AudioSource audioSource;

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
    }
}