using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueAssistant : MonoBehaviour
{
    // Instantiates Singleton
    public static DialogueAssistant Instance { set; get; }

    public Texture[] faces;
    public UnityEngine.UI.RawImage dialogueFaceHolder;

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

    [SerializeField] private int level = 1;

    public GameObject blackOverlay;

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
        dialogueAnim.gameObject.SetActive(false);

        if (level == 1) StartCoroutine(GameJamIntroDialogue());
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha4))
        // {
        //     StartCoroutine(BlackOverlayEnding(2.9f));
        // }
    }

    private IEnumerator BlackOverlayEnding(float time)
    {
        blackOverlay.SetActive(true);
        yield return new WaitForSeconds(time);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // roll credits here or go to main menu
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    public IEnumerator NameTag()
    {
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.0f);
        nameTag.AddWriter("E-MAIL SENT", defaultTypeTime + 0.0f, true);
    }

    public IEnumerator IntroNameTag()
    {
        dialogueFaceHolder.texture = faces[0];
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.0f);
        nameTag.AddWriter("Nord", defaultTypeTime + 0.0f, true);
    }

    public IEnumerator FirstEmail()
    {
        StartTransition();
        yield return new WaitForSeconds(0.0f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("Nice! It's uploaded.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Let's see what the email says.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.18f);

        dialogueFaceHolder.texture = faces[1];
        textWriter.AddWriter("My beloved sister Ashley.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.69f);
        textWriter.AddWriter("I hope you are doing fine in these trying times", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.69f);
        textWriter.AddWriter("Mom and Dad misses you", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.69f);
        textWriter.AddWriter("I'm excited for your visit. See you then!", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.20f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("Yeah these are trying times", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("Since it's quarantine.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("People can't meet up like usual.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("I'm glad I helped send that email.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.42f);

        StartCoroutine(EndTransition());

        StartCoroutine(AfterEmailDialogue());
    }

    public IEnumerator AfterEmailDialogue()
    {
        yield return new WaitForSeconds(4.2f);

        StartTransition();
        yield return new WaitForSeconds(2.0f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("I won't let these viruses infect the emails!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.18f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator SecondEmail()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("Okay, uploading is done.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Let's see what it says.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.18f);

        dialogueFaceHolder.texture = faces[2];
        textWriter.AddWriter("Hey lil bro!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("I heard that your company laid you off.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.20f);
        textWriter.AddWriter("If you need any help don't be afraid to ask!", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.42f);
        textWriter.AddWriter("I'm always here for ya lil bro.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.2f);
        textWriter.AddWriter("Lova ya!", defaultTypeTime + 0.029f, true);

        yield return new WaitForSeconds(4.20f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("I'm glad I helped them connect to each other.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.42f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator ThirdEmail()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("Connection done.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Let's see here...", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.18f);

        dialogueFaceHolder.texture = faces[3];
        textWriter.AddWriter("Hey babe.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.09f);
        textWriter.AddWriter("We haven't seen each other for a while", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.69f);
        textWriter.AddWriter("This long distance relationship is difficult.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.42f);
        textWriter.AddWriter("I'm hanging on cuz you're worth it babe.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.2f);
        textWriter.AddWriter("Miss ya!", defaultTypeTime + 0.021f, true);

        yield return new WaitForSeconds(4.20f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("Yeah, long distance relationships are hard.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("A lot of people today", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("Are having a hard time connecting with loved ones.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Since they are far away from each other.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Connecting with them online is the only way.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator FourthEmail()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("Last Email...", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);

        dialogueFaceHolder.texture = faces[4];
        textWriter.AddWriter("Hi Mom!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.09f);
        textWriter.AddWriter("Me and Dad misses you!", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.69f);
        textWriter.AddWriter("It's been a long time since we last spoke...", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.42f);
        textWriter.AddWriter("I know I can't see you..", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("But I know you're here with me...", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.69f);
        textWriter.AddWriter("I'll always remember you even when you're not here.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(4.2f);
        textWriter.AddWriter("I love you Mom!", defaultTypeTime + 0.00f, true);

        yield return new WaitForSeconds(4.20f);

        dialogueFaceHolder.texture = faces[0];
        textWriter.AddWriter("...", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("People who lost their loved ones", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("Still need to have an outlet to grieve.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("I'm glad I can connect these people with each other.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.29f);
        textWriter.AddWriter("Just another day for Nord.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());

        StartCoroutine(BlackOverlayEnding(6.9f));
    }

    public IEnumerator GameJamIntroDialogue()
    {
        yield return new WaitForSeconds(2.9f);
        IntroStartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("It's my duty as a VPN.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.69f);
        textWriter.AddWriter("To protect all these emails from viruses.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.42f);
        textWriter.AddWriter("And successfuly upload them to the receiver.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.69f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator GameJamDialogue1()
    {
        IntroStartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("I have to stick close to the payload.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.69f);
        textWriter.AddWriter("It can only move when I'm near.", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(3.42f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator AnotherIntroDialogue()
    {
        yield return new WaitForSeconds(6.0f);
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Soldier! You are deep in enemy territory.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.75f);
        textWriter.AddWriter("You have one objective when you encounter monsters.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.42f);
        textWriter.AddWriter("KILL.", defaultTypeTime + 0.21f, true);
        yield return new WaitForSeconds(3.42f);

        //MusicManager.Instance.FadeInActiveMusicCaller(0);
        MusicManager.Instance.FadeInAmbientMusicCaller(0, true);
        //FirstPersonAIO.Instance.playerCanMove = true;
        if (GameManager.Instance.introEnabled) FirstPersonAIO.Instance.StartCoroutine(FirstPersonAIO.Instance.CanMoveAfterSeconds(2.0f));

        StartCoroutine(EndTransition());
    }

    public IEnumerator IntroDialogue()
    {
        yield return new WaitForSeconds(2.9f);
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Soldier! You are deep in enemy territory", defaultTypeTime, true);
        yield return new WaitForSeconds(4.9f);
        textWriter.AddWriter("Kill all MONSTERS you encounter!", defaultTypeTime, true);
        yield return new WaitForSeconds(4.9f);
        textWriter.AddWriter("Let's get rid of the monsters plaguing this planet!", defaultTypeTime, true);
        yield return new WaitForSeconds(6.21f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator SwitchShotgun()
    {
        if (switchShotgunTutorial == 0)
        {
            IntroStartTransition();
            yield return new WaitForSeconds(2.0f);

            textWriter.AddWriter("Nice a new weapon!", defaultTypeTime, true);
            yield return new WaitForSeconds(2.29f);
            textWriter.AddWriter("Let me just switch to it.", defaultTypeTime, true);
            yield return new WaitForSeconds(2.69f);

            switchShotgunTutorial++;

            StartCoroutine(EndTransition());
        }
    }

    public IEnumerator FinishArena()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Good job Soldier!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("You got them all!", defaultTypeTime + 0.069f, true);
        yield return new WaitForSeconds(2.9f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator KillDialogue1()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("YEAH! That's it!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("Shoot them till they DIE!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator KillDialogue2()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Shoot to KILL!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    private void StartTransition()
    {
        audioSource.pitch = 0.75f;
        audioSource.volume = 0.9f;
        PlaySound(0);
        audioSource.pitch = 1.0f;
        audioSource.volume = 1.0f;

        dialogueAnim.gameObject.SetActive(true);
        dialogueAnim.SetTrigger("Start");
        StartCoroutine(NameTag());
        textWriter.AddWriter(" ", defaultTypeTime, true);
    }

    private void IntroStartTransition()
    {
        audioSource.pitch = 0.75f;
        audioSource.volume = 0.9f;
        PlaySound(0);
        audioSource.pitch = 1.0f;
        audioSource.volume = 1.0f;

        dialogueAnim.gameObject.SetActive(true);
        dialogueAnim.SetTrigger("Start");
        StartCoroutine(IntroNameTag());
        textWriter.AddWriter(" ", defaultTypeTime, true);
    }

    private IEnumerator EndTransition()
    {
        dialogueAnim.SetTrigger("Exit");
        yield return new WaitForSeconds(0.8f);
        PlaySound(1);
        yield return new WaitForSeconds(0.42f);
        dialogueAnim.gameObject.SetActive(false);
    }
}
