using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class DialogueAssistant : MonoBehaviour
{
    // Instantiates Singleton
    public static DialogueAssistant Instance { set; get; }

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
        dialogueAnim.gameObject.SetActive(false);

        StartCoroutine(AnotherIntroDialogue());
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    public IEnumerator NameTag()
    {
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.18f);
        nameTag.AddWriter("Commander", defaultTypeTime + 0.01f, true);
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

        BGM.Instance.StartBGM();

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

    public IEnumerator SwitchPistol()
    {
        if (switchPistolTutorial == 0)
        {
            StartTransition();
            yield return new WaitForSeconds(2.0f);

            textWriter.AddWriter("Soldier!", defaultTypeTime, true);
            yield return new WaitForSeconds(1.42f);
            textWriter.AddWriter("You picked up a NEW WEAPON!", defaultTypeTime, true);
            yield return new WaitForSeconds(3.69f);
            textWriter.AddWriter("Switch to your PISTOL!", defaultTypeTime, true);
            yield return new WaitForSeconds(4.0f);

            switchPistolTutorial++;

            StartCoroutine(EndTransition());

            StartCoroutine(Surrounded1());
        }
    }

    public IEnumerator SwitchShotgun()
    {
        if (switchShotgunTutorial == 0)
        {
            StartTransition();
            yield return new WaitForSeconds(2.0f);

            textWriter.AddWriter("You picked up a NEW WEAPON!", defaultTypeTime, true);
            yield return new WaitForSeconds(3.09f);
            textWriter.AddWriter("Switch to your SHOTGUN!", defaultTypeTime, true);
            yield return new WaitForSeconds(0.29f);
            textWriter.AddWriter("", defaultTypeTime, true);
            yield return new WaitForSeconds(0.69f);

            textWriter.AddWriter("Multiple MONSTERS converging on you! ", defaultTypeTime + 0.00f, true);
            yield return new WaitForSeconds(2.29f);
            textWriter.AddWriter("Make'em eat lead!", defaultTypeTime + 0.00f, true);

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
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You are SURROUNDED", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.0f);
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
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("Multiple MONSTERS converging on you! ", defaultTypeTime + 0.00f, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter("Make'em eat lead!", defaultTypeTime + 0.00f, true);

        yield return new WaitForSeconds(2.9f);

        StartCoroutine(EndTransition());
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


    public IEnumerator NeedKey()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You need a KEY before continuing", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter("Find it!", defaultTypeTime + 0.069f, true);
        yield return new WaitForSeconds(2.9f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator FoundKey()
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You found the KEY!", defaultTypeTime + 0.042f, true);
        yield return new WaitForSeconds(2.9f);
        textWriter.AddWriter("Now get to the EXIT!", defaultTypeTime + 0.042f, true);
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

    public IEnumerator Dialogue1(string sentence1, float additionalTime)
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue2(string sentence1, string sentence2, float additionalTime)
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue3(string sentence1, string sentence2, string sentence3, float additionalTime)
    {
        StartTransition();
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter(sentence3, defaultTypeTime + additionalTime, true);

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

    private IEnumerator EndTransition()
    {
        dialogueAnim.SetTrigger("Exit");
        yield return new WaitForSeconds(0.8f);
        PlaySound(1);
        yield return new WaitForSeconds(0.8f);
        dialogueAnim.gameObject.SetActive(false);
    }
}
