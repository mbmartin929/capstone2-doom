using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] allEnemies;

    void Awake()
    {
        //gameObject.SetActive(false);
    }

    void Start()
    {
        //gameObject.SetActive(false);
        Pause();
        Debug.Log("GameOverScreen");
    }

    public void Pause()
    {
        // allEnemies = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // foreach (var enemy in allEnemies)
        // {
        //     if (enemy.activeInHierarchy)
        //     {
        //         EnemyController _enemy = enemy.transform.GetChild(0).GetComponent<EnemyController>();

        //         Debug.Log("Name: " + _enemy.transform.parent.gameObject.name);

        //         _enemy.enabled = false;
        //     }
        // }
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
