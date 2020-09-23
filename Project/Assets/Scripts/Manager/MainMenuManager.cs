using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public VideoPlayer VideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButton()
    {
        Debug.Log("Start!");

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
