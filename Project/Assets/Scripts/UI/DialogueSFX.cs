using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSFX : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }
}
