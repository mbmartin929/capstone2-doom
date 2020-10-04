using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    void Awake()
    {
        //gameObject.SetActive(false);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restart");

        //StartCoroutine(GameManager.Instance.RestartCurrentScene());
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
