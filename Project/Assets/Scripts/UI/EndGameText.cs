using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class EndGameText : MonoBehaviour
{
    public enum EndGameTextType { Secrets, Kills, Brutalized, HealthFound, ArmorFound, AmmoFound, PickupsFound };
    [SerializeField] private EndGameTextType textType;

    private string textToWrite;
    private int characterIndex;
    private float timer;
    public float timePerCharacter;
    private bool invisibleCharacters;


    private TMP_Text text;

    private AudioSource audioSource;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        textToWrite = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        text.alignment = TextAlignmentOptions.TopLeft;
        characterIndex = 0;

        if (textType == EndGameTextType.Secrets) StartCoroutine(Secrets());
        else if (textType == EndGameTextType.Kills) StartCoroutine(Kills());
        else if (textType == EndGameTextType.Brutalized) StartCoroutine(Brutalized());
        else if (textType == EndGameTextType.PickupsFound) StartCoroutine(PickupsFound());
        else if (textType == EndGameTextType.HealthFound) StartCoroutine(HealthFound());
        //else if (textType == EndGameTextType.ArmorFound) StartCoroutine(ArmorFound());
        //else if (textType == EndGameTextType.AmmoFound) StartCoroutine(AmmoFound());
    }

    private string GetPercentage(int currentValue, int maxValue)
    {
        float value = (((float)currentValue) / ((float)maxValue)) * 100f;
        int newValue = Mathf.RoundToInt(value);

        string toReturn = " [" + Mathf.RoundToInt(value).ToString("F0") + "%]";
        return toReturn;
    }

    public IEnumerator Secrets()
    {
        string toWrite = "Secrets: " + EndGameScreen.Instance.totalSecretsFound + " / " + EndGameScreen.Instance.totalSecrets + GetPercentage(EndGameScreen.Instance.totalSecretsFound, EndGameScreen.Instance.totalSecrets);
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public IEnumerator Kills()
    {
        string toWrite = "Kills: " + EndGameScreen.Instance.killedEnemies + " / " + EndGameScreen.Instance.totalEnemies + GetPercentage(EndGameScreen.Instance.killedEnemies, EndGameScreen.Instance.totalEnemies);
        AddWriter("", 0.0f, true);
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public IEnumerator Brutalized()
    {
        string toWrite = "Brutalized: " + EndGameScreen.Instance.enemiesGibbed + " / " + EndGameScreen.Instance.totalEnemies + GetPercentage(EndGameScreen.Instance.enemiesGibbed, EndGameScreen.Instance.totalEnemies);
        AddWriter("", 0.0f, true);
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public IEnumerator PickupsFound()
    {
        int pickupsFound = EndGameScreen.Instance.healthFound + EndGameScreen.Instance.armorFound + EndGameScreen.Instance.ammoFound;
        int totalPickups = EndGameScreen.Instance.totalHealth + EndGameScreen.Instance.totalArmor + EndGameScreen.Instance.totalAmmo;

        string toWrite = "Pick Ups Found: " + pickupsFound + " / " + totalPickups + GetPercentage(pickupsFound, totalPickups);
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public IEnumerator HealthFound()
    {
        string toWrite = "Health Found: " + EndGameScreen.Instance.healthFound + " / " + EndGameScreen.Instance.totalHealth;
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public IEnumerator ArmorFound()
    {
        string toWrite = "Armor Found: " + EndGameScreen.Instance.armorFound + " / " + EndGameScreen.Instance.totalArmor;
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public IEnumerator AmmoFound()
    {
        string toWrite = "Ammo Found: " + EndGameScreen.Instance.ammoFound + " / " + EndGameScreen.Instance.totalAmmo;
        AddWriter("", 0.0f, true);
        yield return new WaitForSeconds(0.29f);
        AddWriter(toWrite, 0.0f, true);
        yield return new WaitForSeconds(0.29f);
    }

    public void AddWriter(string _textToWrite, float _timePerCharacter, bool _invisibleCharacters)
    {
        //Debug.Log(uiText);
        if (text == null)
        {
            //Debug.Log("uiText is null");
            text = GetComponent<TMP_Text>();
        }

        textToWrite = _textToWrite;
        timePerCharacter = _timePerCharacter;
        //invisibleCharacters = _invisibleCharacters;

        characterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string _text = textToWrite.Substring(0, characterIndex);

                if (invisibleCharacters)
                {
                    _text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }

                audioSource.Play();

                text.text = _text;

                if (characterIndex >= textToWrite.Length)
                {
                    text = null;
                    return;
                }
            }
        }
    }
}
