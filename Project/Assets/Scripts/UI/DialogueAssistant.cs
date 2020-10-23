﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueAssistant : MonoBehaviour
{
    // Instantiates Singleton
    public static DialogueAssistant Instance { set; get; }

    [SerializeField] private Texture2D[] faces;
    [SerializeField] private RawImage face;

    [SerializeField] private TextWriter textWriter;
    [SerializeField] private TextWriter nameTag;
    [SerializeField] private Animator dialogueAnim;
    private TextMeshPro messageText;

    public float defaultTypeTime = 0.029f;

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    [SerializeField] private int lowHealthTutorial = 0;
    [SerializeField] private int lowArmorTutorial = 0;
    [SerializeField] private int breakWallsTutorial = 0;
    [SerializeField] private int switchPistolTutorial = 0;
    [SerializeField] private int switchShotgunTutorial = 0;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        if (Instance == this) Debug.Log("DialogueAssistant Singleton Initialized");

        //messageText = transform.Find("message").Find("messageText").GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //dialogueAnim.gameObject.SetActive(false);

        if (GameManager.Instance.level == 1) StartCoroutine(IntroDialogueLvl1());
        else if (GameManager.Instance.level == 2) StartCoroutine(IntroDialogueLvl2());
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    public IEnumerator CommanderNameTag()
    {
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.18f);
        nameTag.AddWriter("Commander", defaultTypeTime + 0.01f, true);
    }

    public IEnumerator PinkyNameTag()
    {
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.18f);
        nameTag.AddWriter("Kaichi", defaultTypeTime + 0.01f, true);
    }

    public IEnumerator IntroDialogueLvl1()
    {
        face.texture = faces[0];
        yield return new WaitForSeconds(6.0f);
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Soldier! You are deep in enemy territory.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.75f);
        textWriter.AddWriter("You have one objective when you encounter monsters.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.42f);
        face.texture = faces[2];
        textWriter.AddWriter("KILL.", defaultTypeTime + 0.21f, true);
        yield return new WaitForSeconds(3.42f);

        //MusicManager.Instance.FadeInActiveMusicCaller(0);
        if (GameManager.Instance.introEnabled) FirstPersonAIO.Instance.StartCoroutine(FirstPersonAIO.Instance.CanMoveAfterSeconds(2.0f));

        StartCoroutine(EndTransition());
        MusicManager.Instance.FadeInAmbientMusicCaller(0, true);
        //FirstPersonAIO.Instance.playerCanMove = true;
    }

    public IEnumerator IntroDialogueLvl2()
    {
        face.texture = faces[4];
        yield return new WaitForSeconds(2.9f);
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Hey.", defaultTypeTime + 0.018f, true);
        yield return new WaitForSeconds(2.1f);
        textWriter.AddWriter("We've got our orders.", defaultTypeTime + 0.014f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("EXTERMINATE  All  Alien  EGGS", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.42f);
        textWriter.AddWriter("Split up and we'll cover more ground.", defaultTypeTime + 0.014f, true);
        yield return new WaitForSeconds(3.29f);

        if (GameManager.Instance.introEnabled) FirstPersonAIO.Instance.StartCoroutine(FirstPersonAIO.Instance.CanMoveAfterSeconds(2.9f));

        StartCoroutine(EndTransition());
        //MusicManager.Instance.FadeInAmbientMusicCaller(0, true);
    }

    public IEnumerator SwitchPistol()
    {
        if (switchPistolTutorial == 0)
        {
            StartTransition();
            yield return new WaitForSeconds(2.0f);

            face.texture = faces[0];
            textWriter.AddWriter("Soldier!", defaultTypeTime, true);
            yield return new WaitForSeconds(1.42f);
            textWriter.AddWriter("You picked up a NEW WEAPON!", defaultTypeTime, true);
            yield return new WaitForSeconds(3.69f);
            textWriter.AddWriter("Switch to your PISTOL!", defaultTypeTime, true);
            yield return new WaitForSeconds(4.0f);

            switchPistolTutorial++;

            StartCoroutine(EndTransition());

            StartCoroutine(Surrounded1());

            yield return new WaitForSeconds(1.0f);

            ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Survive!", 0.069f, 0f));
        }
    }

    public IEnumerator SwitchShotgun()
    {
        if (switchShotgunTutorial == 0)
        {
            MusicManager.Instance.FadeOutAmbientMusicCaller();
            MusicManager.Instance.FadeInActiveMusicCaller(4, false, 0);

            StartTransition();
            yield return new WaitForSeconds(2.0f);

            face.texture = faces[0];
            textWriter.AddWriter("You picked up a NEW WEAPON!", defaultTypeTime, true);
            yield return new WaitForSeconds(3.09f);
            textWriter.AddWriter("Switch to your SHOTGUN!", defaultTypeTime, true);
            yield return new WaitForSeconds(0.29f);
            // textWriter.AddWriter("", defaultTypeTime, true);
            // yield return new WaitForSeconds(0.29f);

            // textWriter.AddWriter("Multiple MONSTERS converging on you! ", defaultTypeTime + 0.00f, true);
            // yield return new WaitForSeconds(2.29f);
            // textWriter.AddWriter("Make'em eat lead!", defaultTypeTime + 0.00f, true);

            yield return new WaitForSeconds(3.29f);

            switchShotgunTutorial++;

            StartCoroutine(EndTransition());

            //StartCoroutine(Surrounded2());
        }
    }

    public IEnumerator Surrounded1()
    {
        yield return new WaitForSeconds(4.2f);

        StartTransition();
        face.texture = faces[2];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You are SURROUNDED", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.0f);
        face.texture = faces[1];
        textWriter.AddWriter("SHOOT!", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("KILL", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("MASSACRE", defaultTypeTime + 0.00f, true);

        yield return new WaitForSeconds(2.9f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator Surrounded2()
    {
        StartTransition();
        face.texture = faces[0];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Multiple MONSTERS converging on you! ", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.42f);
        face.texture = faces[1];
        textWriter.AddWriter("Make'em eat lead!", defaultTypeTime + 0.00f, true);

        yield return new WaitForSeconds(2.9f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator FinishArena()
    {
        StartTransition();
        face.texture = faces[0];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Good job Soldier!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        face.texture = faces[1];
        textWriter.AddWriter("You got them all!", defaultTypeTime + 0.069f, true);
        yield return new WaitForSeconds(2.9f);

        StartCoroutine(EndTransition());
    }


    public IEnumerator NeedKey()
    {
        StartTransition();
        face.texture = faces[0];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You need a KEY before continuing", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter("Find it!", defaultTypeTime + 0.069f, true);
        yield return new WaitForSeconds(2.9f);

        ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Find The Key", 0.042f, 0f));

        StartCoroutine(EndTransition());
    }

    public IEnumerator FoundKey()
    {
        StartTransition();
        face.texture = faces[0];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You found the KEY!", defaultTypeTime + 0.042f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Now get to the EXIT!", defaultTypeTime + 0.042f, true);
        yield return new WaitForSeconds(2.9f);

        ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Go to the Exit", 0.042f, 0f));

        StartCoroutine(EndTransition());
    }

    public IEnumerator KillDialogue1()
    {
        StartTransition();
        face.texture = faces[2];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("YEAH! That's it!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        face.texture = faces[1];
        textWriter.AddWriter("Shoot them till they DIE!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator KillDialogue2()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);
        face.texture = faces[1];
        textWriter.AddWriter("Shoot to KILL!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator Dialogue1(string sentence1, float additionalTime, int faceID)
    {
        StartTransition();
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue2(string sentence1, string sentence2, float additionalTime, int faceID)
    {
        StartTransition();
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue3(string sentence1, string sentence2, string sentence3, float additionalTime, int faceID)
    {
        StartTransition();
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter(sentence3, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue4(string sentence1, string sentence2, string sentence3, string sentence4, float additionalTime, int faceID)
    {
        StartTransition();
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.69f);
        textWriter.AddWriter(sentence3, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter(sentence4, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    private void StartTransition()
    {
        Debug.Log("Start Transition");

        audioSource.pitch = 0.75f;
        audioSource.volume = 0.9f;
        PlaySound(0);
        audioSource.pitch = 1.0f;
        audioSource.volume = 1.0f;

        //face = faces[0];

        dialogueAnim.gameObject.SetActive(true);
        dialogueAnim.SetTrigger("Start");
        StartCoroutine(CommanderNameTag());
        textWriter.AddWriter(" ", defaultTypeTime, true);
    }

    private void StartTransitionSoldierGirl()
    {
        audioSource.pitch = 0.75f;
        audioSource.volume = 0.9f;
        PlaySound(0);
        audioSource.pitch = 1.0f;
        audioSource.volume = 1.0f;

        //face = faces[1];

        dialogueAnim.gameObject.SetActive(true);
        dialogueAnim.SetTrigger("Start");
        StartCoroutine(CommanderNameTag());
        textWriter.AddWriter(" ", defaultTypeTime, true);
    }

    private IEnumerator EndTransition()
    {
        dialogueAnim.SetTrigger("Exit");
        yield return new WaitForSeconds(0.8f);
        PlaySound(1);
        yield return new WaitForSeconds(0.7f);
        face.texture = faces[0];
        dialogueAnim.gameObject.SetActive(false);
    }
}
