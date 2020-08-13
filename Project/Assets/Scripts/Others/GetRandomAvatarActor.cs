using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class GetRandomAvatarActor : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

            GameObject avatar = transform.GetChild(0).gameObject;
            Material chosenMaterial;
            Material[] materials = ActorAvatarManager.Instance.slimeAvatars;

            // foreach (var material in ActorAvatarManager.Instance.slimeAvatars)
            // {
            //     if (material != null)
            //     {
            //         chosenMaterial = material;
            //         material.;
            //     }
            // }

            if (Application.isEditor)
            {
                // for (int i = 0; i < ActorAvatarManager.Instance.slimeAvatars.Length; i++)
                // {
                //     if (ActorAvatarManager.Instance.slimeAvatars[i] != null)
                //     {
                //         chosenMaterial = ActorAvatarManager.Instance.slimeAvatars[i];
                //         ActorAvatarManager.Instance.slimeAvatars[i] = null;
                //         avatar.GetComponent<SpriteRenderer>().material = chosenMaterial;
                //         return;
                //     }
                // }
            }
            else
            {

            }

            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Slime)
            {
                for (int i = 0; i < ActorAvatarManager.Instance.slimeAvatars.Length; i++)
                {
                    if (ActorAvatarManager.Instance.slimeAvatars[i] != null)
                    {
                        chosenMaterial = ActorAvatarManager.Instance.slimeAvatars[i];
                        ActorAvatarManager.Instance.slimeAvatars[i] = null;
                        avatar.GetComponent<SpriteRenderer>().material = chosenMaterial;
                        return;
                    }
                }
            }

            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Worm)
            {
                for (int i = 0; i < ActorAvatarManager.Instance.wormAvatars.Length; i++)
                {
                    if (ActorAvatarManager.Instance.wormAvatars[i] != null)
                    {
                        chosenMaterial = ActorAvatarManager.Instance.wormAvatars[i];
                        ActorAvatarManager.Instance.wormAvatars[i] = null;
                        avatar.GetComponent<SpriteRenderer>().material = chosenMaterial;
                        return;
                    }
                }
            }
        }
    }
}