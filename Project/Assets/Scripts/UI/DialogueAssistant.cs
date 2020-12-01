using System.Collections;
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

    public bool playBadEnd = false;

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
        else if (GameManager.Instance.level == 3) StartCoroutine(IntroDialogueLvl3());
    }

    private void Update()
    {
        // if (playBadEnd)
        // {
        //     StartCoroutine(BadEndDialogue1());
        //     //playBadEnd = false;
        // }
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

    public IEnumerator UnknownNameTag()
    {
        nameTag.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.18f);
        nameTag.AddWriter("Unknown", defaultTypeTime + 0.01f, true);
    }

    public IEnumerator IntroDialogueLvl1()
    {
        face.texture = faces[0];
        yield return new WaitForSeconds(6.0f);
        StartTransition(0);
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
        else TimeManager.Instance.StartTimer();

        StartCoroutine(EndTransition());
        MusicManager.Instance.FadeInAmbientMusicCaller(0, true);
        FirstPersonAIO.Instance.playerCanMove = true;
    }

    public IEnumerator IntroDialogueLvl2()
    {
        face.texture = faces[4];
        yield return new WaitForSeconds(2.9f);
        StartTransition(1);
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
        else TimeManager.Instance.StartTimer();

        StartCoroutine(EndTransition());
        MusicManager.Instance.FadeInAmbientMusicCaller(6, true);
        FirstPersonAIO.Instance.playerCanMove = true;
    }

    public IEnumerator IntroDialogueLvl3()
    {
        face.texture = faces[6];
        yield return new WaitForSeconds(2.9f);
        StartTransition(2);
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter("You will die here killer.", defaultTypeTime + 0.018f, true);
        yield return new WaitForSeconds(2.42f);
        textWriter.AddWriter("We will do everything to protect our planet.", defaultTypeTime + 0.014f, true);
        yield return new WaitForSeconds(3.0f);
        textWriter.AddWriter("To protect our species.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(3.42f);

        if (GameManager.Instance.introEnabled) FirstPersonAIO.Instance.StartCoroutine(FirstPersonAIO.Instance.CanMoveAfterSeconds(2.9f));
        else TimeManager.Instance.StartTimer();

        StartCoroutine(EndTransition());
        MusicManager.Instance.FadeInAmbientMusicCaller(6, true);
        FirstPersonAIO.Instance.playerCanMove = true;
    }

    public IEnumerator SwitchPistol()
    {
        if (switchPistolTutorial == 0)
        {
            StartTransition(0);
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

            yield return new WaitForSeconds(0.69f);

            ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Survive!", 0.069f, 0f));
        }
    }

    public IEnumerator SwitchShotgun()
    {
        if (switchShotgunTutorial == 0 && GameManager.Instance.level == 1)
        {
            MusicManager.Instance.FadeOutAmbientMusicCaller();
            MusicManager.Instance.FadeInActiveMusicCaller(4, false, 0);

            StartTransition(0);
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

        StartTransition(0);
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
        StartTransition(0);
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
        StartTransition(0);
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
        StartTransition(0);
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
        StartTransition(0);
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
        StartTransition(0);
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
        StartTransition(0);
        yield return new WaitForSeconds(2.0f);
        face.texture = faces[1];
        textWriter.AddWriter("Shoot to KILL!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }

    public IEnumerator Dialogue1(string sentence1, float time1, float additionalTime, int faceID, int nametagID)
    {
        StartTransition(nametagID);
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time1);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue2(string sentence1, float time1, string sentence2, float time2, float additionalTime, int faceID, int nametagID)
    {
        StartTransition(nametagID);
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);

        yield return new WaitForSeconds(4.2f);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue3(string sentence1, float time1, string sentence2, float time2, string sentence3, float time3, float additionalTime, int faceID, int nametagID)
    {
        StartTransition(nametagID);
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time1);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time2);
        textWriter.AddWriter(sentence3, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time3);

        StartCoroutine(EndTransition());
    }
    public IEnumerator Dialogue4(string sentence1, float time1, string sentence2, float time2, string sentence3, float time3, string sentence4, float time4, float additionalTime, int faceID, int nametagID)
    {
        StartTransition(nametagID);
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time1);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time2);
        textWriter.AddWriter(sentence3, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time3);
        textWriter.AddWriter(sentence4, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time4);

        StartCoroutine(EndTransition());
    }

    public IEnumerator Dialogue5(string sentence1, float time1, string sentence2, float time2, string sentence3, float time3, string sentence4, float time4, string sentence5, float time5, float additionalTime, int faceID, int nametagID)
    {
        StartTransition(nametagID);
        face.texture = faces[faceID];
        yield return new WaitForSeconds(2.0f);

        textWriter.AddWriter(sentence1, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time1);
        textWriter.AddWriter(sentence2, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time2);
        textWriter.AddWriter(sentence3, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time3);
        textWriter.AddWriter(sentence4, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time4);
        textWriter.AddWriter(sentence5, defaultTypeTime + additionalTime, true);
        yield return new WaitForSeconds(time5);

        StartCoroutine(EndTransition());
    }

    public IEnumerator EndDialogue1()
    {
        ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Leave or Kill", 0.018f, 0.69f));

        StartTransition(0);
        face.texture = faces[0];
        yield return new WaitForSeconds(1.69f);

        textWriter.AddWriter("There it is.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.0f);
        textWriter.AddWriter("Quickly! Kill the eggs!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.29f);
        textWriter.AddWriter("We can end the fight with this!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);

        face.texture = faces[6];
        nameTag.AddWriter("Unknown", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("No! Please!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("Please spare us.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("You can have this planet.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("We'll leave you humans. Just leave us alone!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);

        face.texture = faces[0];
        nameTag.AddWriter("Commander", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("Hurry up!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("What are you waiting for?", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.929f);
        textWriter.AddWriter("KILL THEM!!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.42f);

        face.texture = faces[6];
        nameTag.AddWriter("Unknown", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("Just leave us alone..", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("You only need the planet right?", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("You only want the resources..", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("You can have it!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.42f);
        textWriter.AddWriter("You don't need to kill us anymore..", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("We surrender..", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);

        face.texture = faces[2];
        nameTag.AddWriter("Commander", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("KILL!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(0.929f);
        textWriter.AddWriter("KILL!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(0.929f);
        textWriter.AddWriter("KILL!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);

        face.texture = faces[6];
        nameTag.AddWriter("Unknown", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("These are unborn babies!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("Just spare us..", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.929f);
        textWriter.AddWriter("Just leave, and we'll leave this planet.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.9f);


        StartCoroutine(EndTransition());
    }

    public IEnumerator BadEndDialogue1()
    {
        ObjectiveManager.Instance.StartCoroutine(ObjectiveManager.Instance.TypeObjective("Kill Everything", 0.018f, 0.69f));

        StartTransition(2);
        face.texture = faces[6];
        yield return new WaitForSeconds(1.69f);

        textWriter.AddWriter("NOOOOOOOOO!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.0f);
        textWriter.AddWriter("MY CHILDREN!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);

        face.texture = faces[0];
        nameTag.AddWriter("Commander", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("HAHAHAHAHAHA!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.0f);
        textWriter.AddWriter("GOOD JOB!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.29f);

        face.texture = faces[6];
        nameTag.AddWriter("Unknown", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("YOU WILL NOT LEAVE THIS PLACE!", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.69f);
        textWriter.AddWriter("YOU'RE GOING TO DIE HERE LIKE MY CHILDREN", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.69f);

        face.texture = faces[0];
        nameTag.AddWriter("Commander", defaultTypeTime + 0.01f, true);

        textWriter.AddWriter("It seems you're stuck there'", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.42f);
        textWriter.AddWriter("We'll not wait for you", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(1.29f);
        textWriter.AddWriter("We're going to bomb that place to kingdom come!'", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.29f);
        textWriter.AddWriter("Too many monsters converging in your area, can't risk it.", defaultTypeTime + 0.01f, true);
        yield return new WaitForSeconds(2.69f);

        StartCoroutine(EndTransition());
    }

    private void StartTransition(int nametagID)
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
        if (nametagID == 0) StartCoroutine(CommanderNameTag());
        else if (nametagID == 1) StartCoroutine(PinkyNameTag());
        else if (nametagID == 2) StartCoroutine(UnknownNameTag());
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
        yield return new WaitForSeconds(0.79f);
        PlaySound(1);
        yield return new WaitForSeconds(0.5f);
        face.texture = faces[0];
        dialogueAnim.gameObject.SetActive(false);
    }
}
