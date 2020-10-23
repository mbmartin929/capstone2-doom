using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollider : MonoBehaviour
{
    //public bool specialDialogue = false;
    public enum NumberOfSentences { One, Two, Three, Four };
    public int faceID = 0;
    public NumberOfSentences numberOfSentences = NumberOfSentences.One;

    public string[] sentences;
    public float additionalTime = 0.01f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (numberOfSentences == NumberOfSentences.One)
            {
                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.Dialogue1(sentences[0], additionalTime, faceID));
            }
            else if (numberOfSentences == NumberOfSentences.Two)
            {
                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.Dialogue2(sentences[0], sentences[1], additionalTime, faceID));
            }
            else if (numberOfSentences == NumberOfSentences.Three)
            {
                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.Dialogue3(sentences[0], sentences[1], sentences[2], additionalTime, faceID));
            }
            else if (numberOfSentences == NumberOfSentences.Four)
            {
                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.Dialogue4(sentences[0], sentences[1], sentences[2], sentences[3], additionalTime, faceID));
            }

            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 5.0f);
        }
    }
}
