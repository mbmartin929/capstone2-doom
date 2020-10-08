using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startBlackOverlay;
    public GameObject endBlackOverlay;
    public GameObject awakeBlackOverlay;

    public VideoPlayer launchVideo;
    public VideoPlayer VideoPlayer;
    public float time;

    private AudioSource audioSource;

    private bool isLaunchDone = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMusic());
        VideoPlayer.loopPointReached += LoadScene;

        StartCoroutine(AwakeBlackOverlay());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            launchVideo.gameObject.SetActive(false);
            audioSource.Play();
        }
    }

    private IEnumerator AwakeBlackOverlay()
    {
        yield return new WaitForSeconds(1.0f);
        awakeBlackOverlay.SetActive(false);
    }


    private IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds((float)launchVideo.length + 0.29f);
        startBlackOverlay.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(0.69f);
        launchVideo.gameObject.SetActive(false);
        startBlackOverlay.SetActive(false);
    }

    public void StartButton()
    {
        Debug.Log("Start!");
        audioSource.Stop();
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        endBlackOverlay.SetActive(true);
        yield return new WaitForSeconds(time);
        VideoPlayer.Play();
    }

    private void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
