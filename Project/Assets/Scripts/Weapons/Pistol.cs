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

    public Vector3 camRotation;
    public float FOV;

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

        FOV = fpsCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCam.transform.eulerAngles += camRotation;
        fpsCam.fieldOfView = FOV;
        //Debug.Log(fpsCam.transform.eulerAngles);

        //if (fpsCam.fieldOfView)

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {

    }

    private void Shoot()
    {
        RaycastHit hit;
        if (canAttack)
        {
            //fpsCam.gameObject.transform.Rotate()


            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

                if (hit.transform.tag == "Level")
                {
                    MeshCollider collider = hit.collider as MeshCollider;
                    // Remember to handle case where collider is null because you hit a non-mesh primitive...

                    Mesh mesh = collider.sharedMesh;

                    // There are 3 indices stored per triangle
                    int limit = hit.triangleIndex * 3;
                    int submesh;
                    for (submesh = 0; submesh < mesh.subMeshCount; submesh++)
                    {
                        int numIndices = mesh.GetTriangles(submesh).Length;
                        if (numIndices > limit)
                            break;

                        limit -= numIndices;
                    }

                    Material material = collider.GetComponent<MeshRenderer>().sharedMaterials[submesh];

                    Debug.Log(material.name);

                    Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else if (hit.transform.tag == "Enemy")
                {
                    TempEnemy enemy = hit.transform.GetComponent<TempEnemy>();
                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));
                            bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit.point, item.transform.rotation);
                            bloodGo.transform.parent = hit.transform;
                        }

                    }
                    // GameObject bloodSplashGo = Instantiate(enemy.bloodSplashGo, hit.point, Quaternion.LookRotation(hit.normal));
                    // bloodSplashGo.transform.parent = hit.transform;
                    if (hit.transform.gameObject != null)
                    {
                        //hit.transform.GetComponent<TempEnemy>().PlayParticleSystem();
                    }
                }

                pistolMuzzle.Play();



                anim.SetTrigger("Shoot");
                StartCoroutine("MuzzleLight");
                canAttack = false;

                UnitController target = hit.transform.GetComponent<UnitController>();

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
        // Debug.Log(cameraGo.name);

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
                // camTransform = cameraGo.transform;
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }

    static Mesh GetMesh(GameObject go)
    {
        if (go)
        {
            MeshFilter mf = go.GetComponent<MeshFilter>();
            if (mf)
            {
                Mesh m = mf.sharedMesh;
                if (!m) { m = mf.mesh; }
                if (m)
                {
                    return m;
                }
            }
        }
        return (Mesh)null;
    }

    public void CameraX(float value)
    {
        fpsCam.transform.eulerAngles += new Vector3(value, 0, 0);
    }

}
