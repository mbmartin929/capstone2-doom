using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // References Singleton
    protected GameManager gameManager;
    public enum projectileType { raycast, gameOject };

    #region Variables
    [Header("Game Objects")]
    public GameObject projectileGo;
    public ParticleSystem pistolMuzzle;
    public GameObject muzzleLight;
    public GameObject hitEffect;
    public GameObject crosshair;

    [Header("Stats")]
    public projectileType ProjectileType;
    public int damage;
    public float range = 150f;
    public float muzzleLightResetTime = 0.1f;

    [Header("Others")]
    protected Camera fpsCam;
    protected GameObject cameraGo;
    public Vector3 camRotation;
    private List<GameObject> actors = new List<GameObject>();
    private Animator anim;
    private bool canAttack;
    public float FOV;

    protected AudioSource audioSource;
    public AudioClip audioShoot;
    public AudioClip audioReload;

    #endregion

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

        if (ProjectileType == projectileType.raycast) if (Input.GetMouseButtonDown(0)) RaycastShoot();
            else
            {

            }
    }

    protected void RaycastShoot()
    {
        fpsCam.transform.eulerAngles += camRotation;

        RaycastHit hit;
        if (canAttack)
        {
            //GetComponent<AudioSource>().Play();

            // Plays shooting audio
            audioSource.PlayOneShot(audioShoot);

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
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();

                    if (!enemy.IsDead()) crosshair.GetComponent<Animator>().SetTrigger("Crosshair");

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

                    if (hit.transform.gameObject != null)
                    {

                    }

                    enemy.TakeDamage(damage);
                }

                pistolMuzzle.Play();



                anim.SetTrigger("Shoot");
                StartCoroutine("MuzzleLight");
                canAttack = false;
            }
        }
    }

    protected IEnumerator MuzzleLight()
    {
        muzzleLight.gameObject.SetActive(true);

        yield return new WaitForSeconds(muzzleLightResetTime);

        muzzleLight.gameObject.SetActive(false);
    }

    protected void FinishShooting()
    {
        canAttack = true;
        anim.SetTrigger("Idle");
    }

    #region Get Mesh from Raycast
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

    #endregion
}
