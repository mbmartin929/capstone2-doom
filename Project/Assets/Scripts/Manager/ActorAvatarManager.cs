using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ActorAvatarManager : MonoBehaviour
{
    // Instantiates Singleton
    public static ActorAvatarManager Instance { set; get; }

    public Material[] slimeAvatars;
    public Material[] redSlimeAvatars;
    public Material[] wormAvatars;
    public Material[] spiderAvatars;
    public Material[] snailAvatars;
    public Material[] bbWormAvatars;

    void Awake()
    {
        // Sets Singleton
        Instance = this;
    }
}
