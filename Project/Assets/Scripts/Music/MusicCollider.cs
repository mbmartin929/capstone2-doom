using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCollider : MonoBehaviour
{
    public int musicID = 0;
    public bool loop = false;
    public int nextMusicID = 0;

    public float waitTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance.FadeOutAmbientMusicCaller();
            StartCoroutine(WaitFadeIn());
        }
    }

    private IEnumerator WaitFadeIn()
    {
        yield return new WaitForSeconds(waitTime);
        MusicManager.Instance.FadeInActiveMusicCaller(musicID, loop, nextMusicID);
        Destroy(gameObject);
    }
}