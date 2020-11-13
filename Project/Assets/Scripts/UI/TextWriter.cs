using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TMP_Text uiText;
    [HideInInspector] public string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters;

    private AudioSource audioSource;

    void Awake()
    {
        uiText = GetComponent<TMP_Text>();
        uiText.alignment = TextAlignmentOptions.TopLeft;
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        uiText.alignment = TextAlignmentOptions.TopLeft;
        //textMesh = GetComponent<TextMesh>();
    }

    public void AddWriter(string _textToWrite, float _timePerCharacter, bool _invisibleCharacters)
    {
        //Debug.Log(uiText);
        if (uiText == null)
        {
            //Debug.Log("uiText is null");
            uiText = GetComponent<TMP_Text>();
        }

        textToWrite = _textToWrite;
        timePerCharacter = _timePerCharacter;
        //invisibleCharacters = _invisibleCharacters;

        characterIndex = 0;
    }

    public IEnumerator Labels2(string label01)
    {
        // Force and update of the mesh to get valid information.
        uiText.ForceMeshUpdate();

        // Get # of Visible Character in text object
        int totalVisibleCharacters = uiText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;

        while (true)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);

            uiText.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {
                uiText.text = label01;
                yield return new WaitForSeconds(1.0f);
            }

            counter += 1;

            yield return new WaitForSeconds(0.01f);
        }

        //Debug.Log("Done revealing the text.");
    }

    // Update is called once per frame
    void Update()
    {
        if (uiText != null)
        {
            //Debug.Log(textToWrite);

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;

                //Debug.Log("Text to Write: " + textToWrite);

                //Debug.Log("characterindex: " + characterIndex);
                string text = null;
                try
                {
                    //text = textToWrite.Substring(0, characterIndex);
                    if (text != null) text = textToWrite.Substring(0, characterIndex);
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    Debug.LogException(ex);
                }

                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }

                audioSource.Play();

                uiText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }
    }
}
