using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.0f);
        nameTag.AddWriter("Nord", defaultTypeTime + 0.0f, true);
    }

    public IEnumerator FirstEmail()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

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
        yield return new WaitForSeconds(6.9f);

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

        dialogueFaceHolder.texture = faces[1];
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
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("I'm glad I helped send that email.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.42f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator GameJamIntroDialogue()
    {
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
        yield return new WaitForSeconds(0.7f);
        dialogueAnim.gameObject.SetActive(false);
    }
}
