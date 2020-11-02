using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollider : MonoBehaviour
{
    //public bool specialDialogue = false;
    public enum NumberOfSentences { One, Two, Three, Four, Five };
    public int nametagID = 0;
    public int faceID = 0;
    public NumberOfSentences numberOfSentences = NumberOfSentences.One;

    public string[] sentences;
    public float[] timePerSentence;
    public float additionalTime = 0.01f;

    public bool kaichiDeath = false;
    public GameObject door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (numberOfSentences == NumberOfSentences.One)
            {
                DialogueAssistant.Instance.StartCoroutine
                (DialogueAssistant.Instance.Dialogue1(
                sentences[0],
                timePerSentence[0],
                additionalTime, faceID, nametagID));
            }
            else if (numberOfSentences == NumberOfSentences.Two)
            {
                DialogueAssistant.Instance.StartCoroutine
                (DialogueAssistant.Instance.Dialogue2(
                sentences[0], timePerSentence[0],
                sentences[1], timePerSentence[1],
                additionalTime, faceID, nametagID));
            }
            else if (numberOfSentences == NumberOfSentences.Three)
            {
                DialogueAssistant.Instance.StartCoroutine
                (DialogueAssistant.Instance.Dialogue3(
                sentences[0], timePerSentence[0],
                sentences[1], timePerSentence[1],
                sentences[2], timePerSentence[2],
                additionalTime, faceID, nametagID));
            }
            else if (numberOfSentences == NumberOfSentences.Four)
            {
                DialogueAssistant.Instance.StartCoroutine
                (DialogueAssistant.Instance.Dialogue4(
                sentences[0], timePerSentence[0],
                sentences[1], timePerSentence[1],
                sentences[2], timePerSentence[2],
                sentences[3], timePerSentence[3],
                additionalTime, faceID, nametagID));
            }
            else if (numberOfSentences == NumberOfSentences.Five)
            {
                DialogueAssistant.Instance.StartCoroutine
                (DialogueAssistant.Instance.Dialogue5(
                sentences[0], timePerSentence[0],
                sentences[1], timePerSentence[1],
                sentences[2], timePerSentence[2],
                sentences[3], timePerSentence[3],
                sentences[4], timePerSentence[4],
                additionalTime, faceID, nametagID));

                if (kaichiDeath)
                {
                    Debug.Log("Kaichi Death");
                    door.GetComponent<ImportantDoor>().StartCoroutine(door.GetComponent<ImportantDoor>().PlayAnimation());
                    ObjectiveManager.Instance.TypeObjective("Find a way out", 0.069f, 21f);
                }
            }

            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 5.0f);
        }
    }
}
