//=============================================================================
//  ActorBillboard
//  by Mariusz Skowroński from Healthbar Games (http://healthbargames.pl)
//
//  This class represents billboard - actor's visual part that is always
//  orientated to face current camera.
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ActorBillboard : MonoBehaviour
    {
        public Transform actorTransform;
        public enum Enemy { Worm, GreenSlime, RedSlime, Spider, Snail, BbWorm };
        public GameObject slimeShockwave;

        public delegate void BeforeRenderBillboardEvent();

        public BeforeRenderBillboardEvent beforeRenderBillboardEvent;

        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        public bool IsPaused
        {
            get { return isPaused; }
        }

        public ActorAnimation CurrentAnimation
        {
            get { return currentAnimation; }
        }

        private Transform myTransform;
        private SpriteRenderer spriteRenderer;
        private Vector3 actorForwardVector = Vector3.forward;

        private ActorAnimation currentAnimation = null;
        public int currentFrameIndex = 0;
        private float frameChangeDelay = 1.0f;
        private bool isPlaying = false;
        private bool isPaused = false;
        private int playDirection = 1;

        public Enemy enemy = Enemy.Worm;
        public Material[] materials;

        public void SetActorForwardVector(Vector3 actorForward)
        {
            actorForwardVector = actorForward;
        }

        public void PlayAnimation(ActorAnimation animation)
        {
            currentAnimation = animation;
            PlayAnimation();
        }

        public void PlayAnimation()
        {
            if (currentAnimation != null)
            {
                currentFrameIndex = 0;
                playDirection = 1;
                isPlaying = true;
                isPaused = false;
                frameChangeDelay = 1.0f / currentAnimation.Speed;
            }
        }

        public void PauseAnimation()
        {
            if (isPlaying)
            {
                isPaused = true;
            }
        }

        public void ResumeAnimation()
        {
            if (isPlaying)
            {
                isPaused = false;
            }
        }

        public void StopAnimation()
        {
            isPlaying = false;
            isPaused = false;
            currentFrameIndex = 0;
            playDirection = 1;
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            spriteRenderer.receiveShadows = true;

            //Debug.Log(spriteRenderer.sharedMaterial.shader);
            //spriteRenderer.materials = materials;
        }

        private void Update()
        {
            if (actorTransform != null)
            {
                actorForwardVector = actorTransform.forward;
            }

            if (isPlaying == false || isPaused == true)
                return;

            frameChangeDelay -= Time.deltaTime;
            if (frameChangeDelay > 0.0f)
                return;

            if (playDirection > 0)
            {
                currentFrameIndex++;
                if (currentFrameIndex >= currentAnimation.FrameCount)
                {
                    if (currentAnimation.Mode == ActorAnimation.AnimMode.Once)
                    {
                        currentFrameIndex = currentAnimation.FrameCount - 1;
                    }
                    else if (currentAnimation.Mode == ActorAnimation.AnimMode.PingPong)
                    {
                        //currentFrameIndex = (currentAnimation.FrameCount - 1) - (currentFrameIndex - currentAnimation.FrameCount + 1);
                        currentFrameIndex = (currentAnimation.FrameCount << 1) - 2 - currentFrameIndex;
                        playDirection = -playDirection;
                    }
                    else
                    {
                        // Put end of action here

                        currentFrameIndex -= currentAnimation.FrameCount;
                    }
                }

                //Debug.Log("Current Frame Index: " + currentFrameIndex);
                #region Slime Behaviour
                if ((enemy == Enemy.GreenSlime) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    EnemyController enemyController = GetComponent<EnemyController>();

                    if (currentFrameIndex == 1)
                    {
                        transform.GetComponent<EnemySounds>().SlimeChaseSoudOneShot();

                        transform.GetComponentInParent<EnemyAI>().AgentStop(false);
                        transform.GetComponentInParent<EnemyAI>().AgentSetDestinationPlayer();
                    }
                    else if (currentFrameIndex == 3)
                    {
                        try { transform.GetComponentInParent<EnemyAI>().AgentStop(true); }
                        catch (Exception e) { Debug.LogException(e, this); }

                    }
                    else if (currentFrameIndex == 4)
                    {
                        //Debug.Log("Slime Attack");
                        transform.GetComponentInParent<EnemyAI>().SlimeAttack(2.69f);
                        Transform attackLoc = transform.parent.GetChild(1);
                        Instantiate(slimeShockwave, attackLoc.position, slimeShockwave.transform.rotation);
                        //GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.SHOOT);
                    }
                }
                if ((enemy == Enemy.RedSlime) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    EnemyController enemyController = GetComponent<EnemyController>();

                    if (currentFrameIndex == 1)
                    {
                        transform.GetComponent<EnemySounds>().SlimeChaseSoudOneShot();

                        transform.GetComponentInParent<EnemyAI>().AgentStop(false);
                        transform.GetComponentInParent<EnemyAI>().AgentSetDestinationPlayer();
                    }
                    else if (currentFrameIndex == 3)
                    {
                        try { transform.GetComponentInParent<EnemyAI>().AgentStop(true); }
                        catch (Exception e) { Debug.LogException(e, this); }

                    }
                    else if (currentFrameIndex == 4)
                    {
                        //Debug.Log("Slime Attack");
                        transform.GetComponentInParent<EnemyAI>().SlimeAttack(2.69f);
                        //GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.SHOOT);
                    }
                }
                #endregion

                #region Spider Behaviour
                if ((enemy == Enemy.Spider) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    EnemyController enemyController = GetComponent<EnemyController>();

                    if (currentFrameIndex == 1)
                    {
                        //transform.GetComponent<EnemySounds>().SlimeChaseSoudOneShot();
                        transform.GetComponentInParent<EnemyAI>().AgentStop(true);
                    }
                    // else if (currentFrameIndex == 4)
                    // {
                    //     try { transform.GetComponentInParent<EnemyAI>().AgentStop(true); }
                    //     catch (Exception e) { Debug.LogException(e, this); }

                    // }
                    else if (currentFrameIndex == 3)
                    {
                        //Debug.Log("Slime Attack");
                        transform.GetComponentInParent<EnemyAI>().SlimeAttack(3.5f);
                        //GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.SHOOT);
                    }
                    else if (currentFrameIndex == 4)
                    {
                        transform.GetComponentInParent<EnemyAI>().AgentStop(false);

                        if (!GetComponent<EnemyController>().IsDead())
                        {
                            //Debug.Log("Frame: " + currentFrameIndex);
                            //transform.GetComponentInParent<EnemyAI>().AgentSetDestinationPlayer();

                            //GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.WALKING);

                            //transform.GetComponentInParent<EnemyAI>().GetNewDir();

                            if (Vector3.Distance(transform.position, GameManager.Instance.playerGo.transform.position) <= GetComponentInParent<EnemyAI>().distanceToAttack - 0.5f)
                            {
                                //Debug.Log("Actor Attack");
                                GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.SHOOT);
                            }

                        }
                        else GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.DIE);
                    }
                }
                #endregion

                #region Worm Behaviour
                if ((enemy == Enemy.Worm) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    if (currentFrameIndex == 3)
                    {
                        GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.SHOOT);
                    }
                }

                if ((enemy == Enemy.Worm) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    if (currentFrameIndex == 1)
                    {
                        transform.parent.LookAt(GameManager.Instance.playerGo.transform.position);
                        transform.GetComponentInParent<EnemyAI>().WormAttack();


                        // Get new Dir after Attack


                    }
                    else if (currentFrameIndex == 2)
                    {
                        if (!GetComponent<EnemyController>().IsDead())
                        {
                            //Debug.Log(currentAnimation.Action);
                            transform.GetComponentInParent<EnemyAI>().GetNewDir();
                        }
                        else GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.DIE);
                    }
                }
                #endregion

                #region Snail Behaviour
                if ((enemy == Enemy.Snail) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    if (currentFrameIndex == 3)
                    {
                        GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.SHOOT);
                    }
                }

                if ((enemy == Enemy.Snail) && (currentAnimation.Action == ActorAnimation.AnimAction.Attack))
                {
                    if (currentFrameIndex == 6)
                    {
                        transform.parent.LookAt(GameManager.Instance.playerGo.transform.position);
                        transform.GetComponentInParent<EnemyAI>().SnailAttack();


                        // Get new Dir after Attack


                    }
                    else if (currentFrameIndex == 7)
                    {
                        if (!GetComponent<EnemyController>().IsDead())
                        {
                            //Debug.Log(currentAnimation.Action);
                            transform.GetComponentInParent<EnemyAI>().GetNewDir();
                        }
                        else GetComponentInParent<EnemyAI>().actor.SetCurrentState(DemoActor.State.DIE);
                    }
                }
                #endregion
            }
            else if (playDirection < 0)
            {
                currentFrameIndex--;
                if (currentFrameIndex < 0)
                {
                    if (currentAnimation.Mode == ActorAnimation.AnimMode.Once)
                    {
                        currentFrameIndex = 0;
                    }
                    else if (currentAnimation.Mode == ActorAnimation.AnimMode.PingPong)
                    {
                        currentFrameIndex = -currentFrameIndex;
                        playDirection = -playDirection;
                    }
                    else
                    {
                        currentFrameIndex += currentAnimation.FrameCount;
                    }
                }
            }


            frameChangeDelay += 1.0f / currentAnimation.Speed;
        }

        private void OnWillRenderObject()
        {
            if (Camera.main == null) { Debug.Log("No Camera Detected"); return; }

            if (beforeRenderBillboardEvent != null)
                beforeRenderBillboardEvent();

            // calculate camera position on 2D XZ plane
            Vector3 cameraPosition2D = Camera.main.transform.position;
            cameraPosition2D.y = 0.0f;

            // calculate billboard position on 2D XZ plane
            Vector3 billboardPosition2D = myTransform.position;
            billboardPosition2D.y = 0.0f;

            // calculate billboard to camera vector
            Vector3 cameraVector = cameraPosition2D - billboardPosition2D;
            if (cameraVector.sqrMagnitude > Mathf.Epsilon)
            {
                cameraVector.Normalize();

                if (currentAnimation != null && spriteRenderer != null)
                {
                    int animFrameDirection = 0;

                    if (currentAnimation.AnimType == ActorAnimation.AnimDirType.EightDirections)
                    {
                        // calculate angle between billboard to camera vector and actor look direction as view angle
                        float viewAngle = Vector3.SignedAngle(cameraVector, actorForwardVector, Vector3.up);

                        if (viewAngle >= 360.0f)
                        {
                            viewAngle -= 360.0f;
                        }
                        else if (viewAngle < 0)
                        {
                            viewAngle += 360.0f;
                        }

                        // calculate sprite animation frame for view angle
                        animFrameDirection = (int)(viewAngle / 45.0f + 0.5f);
                        //Debug.Log(animFrameDirection);
                    }


                    Sprite sprite = currentAnimation.GetSprite(currentFrameIndex, animFrameDirection);
                    spriteRenderer.sprite = sprite;
                }

                Quaternion billboardRotation = Quaternion.LookRotation(-cameraVector, Vector3.up);

                myTransform.rotation = billboardRotation;
            }
        }

    }
}
