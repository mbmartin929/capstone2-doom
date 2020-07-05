using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{


    public class Projectile : MonoBehaviour
    {
        public GameObject cursor;
        public LayerMask layer;
        public Transform shootPoint;

        public EnemyAI enemyAI;

        private Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;

            //LaunchProjectile();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LaunchProjectile1()
        {
            //Ray camRay = cam.ScreenPointToRay()
            RaycastHit hit;

            //if (Physics.Raycast())

            Vector3 playerPos = GameManager.Instance.playerGo.transform.position;
            Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, 0.75f);

            transform.rotation = Quaternion.LookRotation(Vo);

            gameObject.GetComponent<Rigidbody>().velocity = Vo;
        }

        public void LaunchProjectile2()
        {
            Vector3 playerPos = GameManager.Instance.playerGo.transform.position + GameManager.Instance.playerGo.transform.right * 6.9f;
            Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, 0.75f);

            transform.rotation = Quaternion.LookRotation(Vo);

            gameObject.GetComponent<Rigidbody>().velocity = Vo;
        }

        public void LaunchProjectile3()
        {
            Vector3 playerPos = GameManager.Instance.playerGo.transform.position - GameManager.Instance.playerGo.transform.right * 6.9f;
            Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, 0.75f);

            transform.rotation = Quaternion.LookRotation(Vo);

            gameObject.GetComponent<Rigidbody>().velocity = Vo;
        }

        private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0f;

            float Sy = distance.y;
            float Sxz = distanceXZ.magnitude;

            float Vxz = Sxz / time;
            float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

            Vector3 result = distanceXZ.normalized;
            result *= Vxz;
            result.y = Vy;

            return result;
        }
    }
}