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

            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.GreenSlime)
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

            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.RedSlime)
            {
                for (int i = 0; i < ActorAvatarManager.Instance.redSlimeAvatars.Length; i++)
                {
                    if (ActorAvatarManager.Instance.redSlimeAvatars[i] != null)
                    {
                        chosenMaterial = ActorAvatarManager.Instance.redSlimeAvatars[i];
                        ActorAvatarManager.Instance.redSlimeAvatars[i] = null;
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

            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Spider)
            {
                for (int i = 0; i < ActorAvatarManager.Instance.spiderAvatars.Length; i++)
                {
                    if (ActorAvatarManager.Instance.spiderAvatars[i] != null)
                    {
                        chosenMaterial = ActorAvatarManager.Instance.spiderAvatars[i];
                        ActorAvatarManager.Instance.spiderAvatars[i] = null;
                        avatar.GetComponent<SpriteRenderer>().material = chosenMaterial;
                        return;
                    }
                }
            }

            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Snail)
            {
                for (int i = 0; i < ActorAvatarManager.Instance.snailAvatars.Length; i++)
                {
                    if (ActorAvatarManager.Instance.snailAvatars[i] != null)
                    {
                        chosenMaterial = ActorAvatarManager.Instance.snailAvatars[i];
                        ActorAvatarManager.Instance.snailAvatars[i] = null;
                        avatar.GetComponent<SpriteRenderer>().material = chosenMaterial;
                        return;
                    }
                }
            }


            if (avatar.GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.BbWorm)
            {
                for (int i = 0; i < ActorAvatarManager.Instance.bbWormAvatars.Length; i++)
                {
                    if (ActorAvatarManager.Instance.bbWormAvatars[i] != null)
                    {
                        chosenMaterial = ActorAvatarManager.Instance.bbWormAvatars[i];
                        ActorAvatarManager.Instance.bbWormAvatars[i] = null;
                        avatar.GetComponent<SpriteRenderer>().material = chosenMaterial;
                        return;
                    }
                }
            }
        }
    }
}