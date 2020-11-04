using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStatic : MonoBehaviour
{
    private void Awake()
    {
        gameObject.isStatic = false;

        foreach (Transform item in transform)
        {
            item.gameObject.isStatic = false;
        }
    }
}
