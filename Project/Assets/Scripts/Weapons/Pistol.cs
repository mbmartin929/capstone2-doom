using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;

    public Vector3 startingPos;

    private GameObject cameraGo;

    public List<GameObject> actors = new List<GameObject>();

    public float muzzleLightResetTime = 0.1f;

    public ParticleSystem pistolMuzzle;
    public GameObject muzzleLight;

    public GameObject hitEffect;

    private bool canAttack;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        cameraGo = GameObject.FindGameObjectWithTag("Player");
        FindObjectwithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (canAttack)
            {
                pistolMuzzle.Play();

                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

                Debug.Log(hit.transform.name);
                anim.SetTrigger("Shoot");
                StartCoroutine("MuzzleLight");
                canAttack = false;

                TempEnemy target = hit.transform.GetComponent<TempEnemy>();
                if (target != null)
                {
                    target.TakeDamage(10);
                }
            }
        }
    }

    private IEnumerator MuzzleLight()
    {
        muzzleLight.gameObject.SetActive(true);

        yield return new WaitForSeconds(muzzleLightResetTime);

        muzzleLight.gameObject.SetActive(false);
    }

    private void FinishShooting()
    {
        canAttack = true;
        anim.SetTrigger("Idle");
    }

    public void FindObjectwithTag(string _tag)
    {
        Debug.Log(cameraGo.name);

        actors.Clear();
        Transform parent = cameraGo.transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                actors.Add(child.gameObject);
                cameraGo = child.gameObject;
                fpsCam = cameraGo.GetComponent<Camera>();
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}
