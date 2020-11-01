using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleConstrain : MonoBehaviour
{
    public float targetAlpha;

    private bool isNear = false;

    public float duration = 1.69f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //circleCenter = new Vector3(parent.position.x, parent.position.y, parent.position.z);
        //circleCenter = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        // float step = speed * Time.deltaTime;
        // transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.playerGo.transform.position, step);

        // Vector3 offset = transform.position - circleCenter;
        // offset = offset.normalized * radius;
        // transform.position = offset;

        if (isNear)
        {
            float elapsedTime = 0;
            float startValue = spriteRenderer.color.a;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetAlpha, elapsedTime / duration);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            }
        }
        else if (!isNear)
        {
            float elapsedTime = 0;
            float startValue = spriteRenderer.color.a;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime / duration);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isNear = false;
    }
}
