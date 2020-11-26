using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CheckVideo : MonoBehaviour
{
    private VideoClip videoClip;

    private void Awake()
    {
        videoClip = GetComponent<VideoPlayer>().clip;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToCredits());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds((float)videoClip.length + 0.420f);

        SceneManager.LoadScene("EndMainMenu");
    }
}
