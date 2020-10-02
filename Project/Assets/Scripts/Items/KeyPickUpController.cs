using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class KeyPickUpController : PickUpController
{
    public int keyValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Picked up Key");

            //StartCoroutine(DialogueAssistant.Instance.FoundKey());
            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FoundKey());

            other.GetComponent<PlayerController>().keyAmount += keyValue;

            PickUpOverlayManager.Instance.KeyOverlay();

            Destroy(gameObject, 0f);
        }
    }
}
