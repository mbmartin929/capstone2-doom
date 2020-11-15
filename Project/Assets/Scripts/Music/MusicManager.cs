using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public float maxVolume = 1.0f;
    public float speed;
    public bool useMidi = false;

    public AudioClip[] musicTrackMidi;
    public AudioClip[] musicTrackNormal;

    private static bool keepFadingInActive;
    private static bool keepFadingOutActive;

    private static bool keepFadingInAmbient;
    private static bool keepFadingOutAmbient;

    [Header("Audio Sources")]

    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource activeAudioSource;
    [SerializeField] AudioSource ambientAudioSource;

    void Awake()
    {
        Instance = this;

        if (Instance == this) Debug.Log("MusicManager Singleton Initialized");
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void FadeInActiveMusicCaller(int track, bool loop, int nextTrack)
    {
        StartCoroutine(FadeInActiveMusic(track, loop, nextTrack));
    }

    public void FadeOutActiveMusicCaller()
    {
        StartCoroutine(FadeOutActiveMusic(0.18f));
    }

    public void FadeInAmbientMusicCaller(int track, bool loop)
    {
        StartCoroutine(FadeInAmbientMusic(track, loop));
    }

    public void FadeOutAmbientMusicCaller()
    {
        StartCoroutine(FadeOutAmbientMusic(0.18f));
    }

    private IEnumerator FadeInActiveMusic(int track, bool loop, int nextTrack)
    {
        keepFadingInActive = true;
        keepFadingOutActive = false;

        if (useMidi) activeAudioSource.clip = musicTrackMidi[track];
        else activeAudioSource.clip = musicTrackNormal[track];

        if (loop) activeAudioSource.loop = true;
        else activeAudioSource.loop = false;

        activeAudioSource.Play();

        float audioVolume = activeAudioSource.volume;

        while (activeAudioSource.volume < maxVolume && keepFadingInActive)
        {
            audioVolume += speed;
            activeAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }

        if (!loop)
        {
            Debug.Log("Will play another song after");
            yield return new WaitForSeconds(musicTrackNormal[track].length + 1.0f);

            ambientAudioSource.loop = true;

            FadeInAmbientMusicCaller(nextTrack, true);
        }
    }

    private IEnumerator FadeOutActiveMusic(float _speed)
    {
        keepFadingInActive = false;
        keepFadingOutActive = true;

        float audioVolume = activeAudioSource.volume;

        while (activeAudioSource.volume >= 0 && keepFadingOutActive)
        {
            audioVolume -= _speed;
            activeAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FadeInAmbientMusic(int track, bool loop)
    {
        Debug.Log("Fade In Ambient Music");

        keepFadingInAmbient = true;
        keepFadingOutAmbient = false;

        if (useMidi) ambientAudioSource.clip = musicTrackMidi[track];
        else ambientAudioSource.clip = musicTrackNormal[track];

        if (loop) ambientAudioSource.loop = true;
        else ambientAudioSource.loop = false;

        ambientAudioSource.Play();

        float audioVolume = ambientAudioSource.volume;

        while (ambientAudioSource.volume < maxVolume && keepFadingInAmbient)
        {
            audioVolume += speed;
            ambientAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FadeOutAmbientMusic(float _speed)
    {
        keepFadingInAmbient = false;
        keepFadingOutAmbient = true;

        float audioVolume = ambientAudioSource.volume;

        while (ambientAudioSource.volume >= 0 && keepFadingOutAmbient)
        {
            audioVolume -= _speed;
            ambientAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlayOnceActiveMusic(int track, float time)
    {
        FadeOutActiveMusicCaller();

        if (useMidi) ambientAudioSource.clip = musicTrackMidi[track];
        else ambientAudioSource.clip = musicTrackNormal[track];

        yield return new WaitForSeconds(musicTrackNormal[track].length + 1.0f);

        ambientAudioSource.loop = true;

        ambientAudioSource.Play();
    }
}
