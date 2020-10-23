using EightDirectionalSpriteSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public int hp = 15;
    public int eggCount;

    public bool isObjective = false;
    public GameObject[] bloodSplashGos;

    private void Start()
    {
        ObjectiveManager.Instance.targetNumber++;
    }

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;


        foreach (GameObject item in bloodSplashGos)
        {
            if (item.tag == "Hit Normal")
            {
                GameObject bloodGo = Instantiate(item, transform.position, Quaternion.identity);
                //bloodGo.transform.parent = hit.transform;
            }
            else
            {
                GameObject bloodGo = Instantiate(item, transform.position, item.transform.rotation);
                //bloodGo.transform.parent = hit.transform;
            }
        }

        if (hp <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Die Egg");
        if (isObjective)
        {

        }
        Debug.Log("Is Objective Egg");
        ObjectiveManager.Instance.currentNumber += eggCount;
        ObjectiveManager.Instance.UpdateTargetNumberObjective();
        Destroy(gameObject);
    }
}
