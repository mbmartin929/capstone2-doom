using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndTrigger : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = transform.GetChild(0).GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.targetCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.playerGo.transform.GetChild(3).gameObject.SetActive(false);
            // videoPlayer.Play();

            EndGameScreen.Instance.badEnd = false;
            EndGameScreen.Instance.StartEndLevelScreen();
        }
    }
}
