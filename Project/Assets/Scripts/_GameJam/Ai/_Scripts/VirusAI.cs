using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirusAI : MonoBehaviour
{
    Animator anim;
    public GameObject enemy;
    public GameObject cargo;




    // Start is called before the first frame update

    public GameObject GetOpponent()
    {
        return enemy;

    }


    void Start()
    {
        anim = GetComponent<Animator>();

   

    }

    // Update is called once per frame
    void Update()
    {

        anim.SetFloat("distance", Vector3.Distance(transform.position, enemy.transform.position));
    }

  

}
