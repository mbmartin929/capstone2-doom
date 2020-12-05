using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [Header("Intro Dialogue")]
    public AudioClip[] introLvl1;
    public AudioClip[] introLvl2;
    public AudioClip[] introLvl3;

    [Header("Level 1 Dialogue")]
    public AudioClip[] switchPistol;
    public AudioClip[] switchShotgun;

    public AudioClip[] surrounded1;
    public AudioClip[] finishArena;
    public AudioClip[] needKey;
    public AudioClip[] foundKey;


    // Instantiates Singleton
    public static VoiceManager Instance { set; get; }

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        if (Instance == this) Debug.Log("VoiceManager Singleton Initialized");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayDialogueClip(AudioClip clip)
    {
        if (clip == null) { Debug.Log("No AudioClip in place"); return; }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
