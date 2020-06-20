using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooksAtPlayer : MonoBehaviour
{
    public enum LookMode { LookAway, LookTowards };
    public LookMode _LookMode;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 targetRotation = new Vector3(target.transform.position.x,
                                     transform.position.y,
                                     target.transform.position.z);

        if (_LookMode == LookMode.LookTowards) transform.LookAt(targetRotation);
        else if (_LookMode == LookMode.LookAway) LookAwayFrom(targetRotation);
    }

    private void LookAwayFrom(Vector3 point)
    {
        point = 2.0f * transform.position - point;
        transform.LookAt(point);
    }
}
