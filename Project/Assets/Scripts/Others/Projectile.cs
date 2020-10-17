using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class Projectile : MonoBehaviour
    {
        [HideInInspector] public int damage;

        public float duration = 1.29f;

        public EnemyAI enemyAI;

        private Camera cam;

        [SerializeField] private float forwardMultiplier = 2.0f;
        [SerializeField] private float upwardMultiplier = 42.0f;

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

        public void LaunchSnailProjectile(Vector3 transformPosition)
        {
            // Vector3 playerPos = GameManager.Instance.playerGo.transform.position;
            // Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, duration);

            // transform.rotation = Quaternion.LookRotation(Vo);

            // gameObject.GetComponent<Rigidbody>().velocity = Vo;

            float dist = Vector3.Distance(GameManager.Instance.playerGo.transform.position, transformPosition);

            //Debug.Log("Distance: " + dist);
            GetComponent<Rigidbody>().AddForce(((enemyAI.attackLoc.forward + enemyAI.attackLoc.up + enemyAI.attackLoc.forward) * (forwardMultiplier * dist)));
            //GetComponent<Rigidbody>().AddRelativeForce(enemyAI.attackLoc.up * upwardMultiplier * (dist * 0.75f));
            //Debug.Log("Multiplied Distance: " + multiplier * dist);
        }

        public void LaunchProjectile1()
        {
            //Ray camRay = cam.ScreenPointToRay()
            RaycastHit hit;

            //if (Physics.Raycast())

            Vector3 playerPos = GameManager.Instance.playerGo.transform.position;
            Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, duration);

            transform.rotation = Quaternion.LookRotation(Vo);

            gameObject.GetComponent<Rigidbody>().velocity = Vo;
        }

        public void LaunchProjectile2()
        {
            Vector3 playerPos = GameManager.Instance.playerGo.transform.position + GameManager.Instance.playerGo.transform.right * 6.9f;
            Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, duration);

            transform.rotation = Quaternion.LookRotation(Vo);

            gameObject.GetComponent<Rigidbody>().velocity = Vo;
        }

        public void LaunchProjectile3()
        {
            Vector3 playerPos = GameManager.Instance.playerGo.transform.position - GameManager.Instance.playerGo.transform.right * 6.9f;
            Vector3 Vo = CalculateVelocity(playerPos, enemyAI.attackLoc.position, duration);

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

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Level"))
            {
                if (transform.childCount <= 0)
                {

                }
                else if (transform.GetChild(0) != null)
                {
                    // Debug.Log(transform.GetChild(0).name);
                    transform.GetChild(0).transform.parent = null;
                }

                Debug.Log("Collided with Level");
                Destroy(gameObject);
            }
            else if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().TakeDamage(damage);

                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            GetComponent<Grenade>().damage = damage;
            GetComponent<Grenade>().EnemyExplode();
            Debug.Log("Collided with Level");
            Destroy(gameObject);

            if (other.gameObject.CompareTag("Level"))
            {

            }
        }
    }
}