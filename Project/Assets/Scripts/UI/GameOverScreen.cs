﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] allEnemies;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        //gameObject.SetActive(false);
        //StartGameOver();
        //Debug.Log("GameOverScreen");
    }

    public void StartGameOver()
    {
        MusicManager.Instance.FadeOutActiveMusicCaller();
        MusicManager.Instance.FadeOutAmbientMusicCaller();

        StartCoroutine(LateStartMusic());
    }

    private IEnumerator LateStartMusic()
    {
        yield return new WaitForSeconds(0.69f);
        MusicManager.Instance.FadeInActiveMusicCaller(3, true, 2);
    }

    public void Restart()
    {
        Debug.Log("Restart");

        gameObject.SetActive(false);
        StartCoroutine(GameManager.Instance.RestartCurrentScene(0.0f));
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
