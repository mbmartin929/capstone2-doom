using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirusAI : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance",
        Vector3.Distance(transform.position,
        GameManager.Instance.playerGo.transform.position));
    }
}
