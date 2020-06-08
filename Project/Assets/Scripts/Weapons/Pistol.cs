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
        if (canAttack)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

                // Mesh m = GetMesh(hit.transform.gameObject);
                // if (m)
                // {
                //     int[] hittedTriangle = new int[]
                //     {
                //         m.triangles[hit.triangleIndex * 3],
                //         m.triangles[hit.triangleIndex * 3 + 1],
                //         m.triangles[hit.triangleIndex * 3 + 2]
                //     };
                //     for (int i = 0; i < m.subMeshCount; i++)
                //     {
                //         int[] subMeshTris = m.GetTriangles(i);
                //         for (int j = 0; j < subMeshTris.Length; j += 3)
                //         {
                //             if (subMeshTris[j] == hittedTriangle[0] &&
                //                 subMeshTris[j + 1] == hittedTriangle[1] &&
                //                 subMeshTris[j + 2] == hittedTriangle[2])
                //             {
                //                 Debug.Log(string.Format("triangle index:{0} submesh index:{1} submesh triangle index:{2}", hit.triangleIndex, i, j / 3));


                //             }
                //         }
                //     }
                // }

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
                }
                else if (hit.transform.tag == "Enemy")
                {

                }

                pistolMuzzle.Play();

                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

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

}
