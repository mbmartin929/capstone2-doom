using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ActorAvatarManager : MonoBehaviour
{
    // Instantiates Singleton
    public static ActorAvatarManager Instance { set; get; }

    public Material[] slimeAvatars;
    public Material[] wormAvatars;

    void Awake()
    {
        // Sets Singleton
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
