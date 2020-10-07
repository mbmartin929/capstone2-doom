using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public bool useMidi = false;

    public AudioClip[] backgroundMusicMidi;
    public AudioClip[] backgroundMusicNormal;

    [SerializeField] AudioSource bgmAudioSource;

    void Awake()
    {
        if (Instance == this) Debug.Log("MusicManager Singleton Initialized");
    }

    public void PlayBGM()
    {
        if (useMidi) bgmAudioSource.clip = backgroundMusicMidi[0];
        else bgmAudioSource.clip = backgroundMusicNormal[0];

        bgmAudioSource.Play();
    }


    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}
