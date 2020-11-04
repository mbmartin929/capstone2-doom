using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ProjectileStraight : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float duration;
    [HideInInspector] public int damage;
    public Transform mouthOfAlien;


    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
       
        LaunchProjectile();
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LaunchProjectile()
    {
        GameObject proj = Instantiate(bullet, mouthOfAlien.transform.position, transform.rotation);
        float dist = Vector3.Distance(GameManager.Instance.playerGo.transform.position, mouthOfAlien.position);
        //GetComponent<Rigidbody>().AddForce(mouthOfAlien.transform.position * projectileSpeed * dist);
        proj.GetComponent<Rigidbody>().velocity = transform.right * projectileSpeed * dist;

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

            //Debug.Log("Collided with Level");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
