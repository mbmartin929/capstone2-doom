using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
    public class WeaponController : MonoBehaviour
    {
        protected Camera fpsCam;

        #region  Main Variables
        [Header("Stats")]
        public int damage = 20;
        [SerializeField] protected int curAmmo;
        public int clipAmmo = 9;
        public float soundRadius = 10.0f;

        public float range = 100f;
        public float spreadFactor = 0.1f;
        public float FOV;

        [Header("Sounds")]
        public AudioClip[] gunshotSounds;
        public AudioClip[] reloadSounds;

        #endregion

        #region Other Variables

        [Header("Others")]
        public bool canSwitch = false;
        public GameObject hitSFX;
        public float painStrength = 1.0f;
        public Vector3 startWeaponSwitchVector;
        public Vector3 startWeaponSwitchRot;

        public List<GameObject> actors = new List<GameObject>();
        public float muzzleLightResetTime = 0.1f;
        public GameObject bulletCasingParticleGo;
        public ParticleSystem bulletTracerParticle;
        public GameObject muzzleLightGo;
        public GameObject hitEffectGo;
        public Vector3 camRotation;
        public Vector3 startPos;
        [SerializeField] protected GameObject bulletHole;

        #endregion

        #region Private Variables
        protected GameObject cameraGo;
        [SerializeField] protected bool canAttack;
        [HideInInspector] public Animator anim;
        protected Vector3 pos;

        #endregion

        void Start()
        {
            //startPos = transform.position;
        }

        protected void PlayGunshotSound()
        {
            GetComponent<AudioSource>().PlayOneShot(gunshotSounds[0]);
        }

        // Used as animation event
        protected void PlayShotgunShootSound(int index)
        {
            GetComponent<AudioSource>().PlayOneShot(gunshotSounds[index]);
        }

        public void PlayReloadSound(int id)
        {
            GetComponent<AudioSource>().PlayOneShot(reloadSounds[id]);
        }
        bool AnimatorIsPlaying(string stateName)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }
        void ExplosionDamage(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
            {
                hitCollider.SendMessage("AddDamage");
            }
        }
        protected void ShootDetection(Vector3 center, float radius)
        {
            // Debug.Log("Shoot Detection");

            // Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            // foreach (var hitCollider in hitColliders)
            // {
            //     if (hitCollider.GetComponent<EnemyController>() != null)
            //     {
            //         Debug.Log(hitCollider.gameObject.transform.parent.name + " detected from shooting");
            //         hitCollider.gameObject.transform.parent.gameObject.GetComponent<EnemyAI>().ChasePlayer();
            //     }
            // }

            Collider[] targetsInViewRadius = Physics.OverlapSphere(GameManager.Instance.playerGo.transform.position, radius, LayerMask.NameToLayer("Default"));

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Debug.Log("Shoot Detection: " + targetsInViewRadius[i].gameObject.name);

                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < 360 /* / 2 */)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, LayerMask.NameToLayer("Ground")))
                    {
                        if (target.gameObject.tag == "Enemy") target.gameObject.transform.parent.gameObject.GetComponent<EnemyAI>().ChasePlayer();
                    }
                }
            }

        }

        public void SwitchWeapon(Transform _weapon)
        {
            WeaponController weapon = _weapon.GetComponent<WeaponController>();

            weapon.anim.SetTrigger("");
        }

        public void SwitchTo()
        {
            anim.SetTrigger("Switch To");
            transform.localPosition = startWeaponSwitchVector;
            transform.localRotation = Quaternion.Euler(startWeaponSwitchRot.x, startWeaponSwitchRot.y, startWeaponSwitchRot.z);
            // StartCoroutine(SwitchToNumerator());
            // Debug.Log("SwitchTo");
            TextManager.Instance.UpdateAmmoText();
        }

        public void SwitchAway()
        {
            // if (weapon.gameObject.activeSelf)
            // {
            //     StartCoroutine(SwitchAwayNumerator());
            //     Debug.Log("SwitchAway");
            // }

            anim.SetTrigger("Switch Away");
            //Debug.Log("SwitchAway");
        }

        private IEnumerator SwitchToNumerator()
        {
            anim.SetTrigger("Switch To");
            yield return new WaitForSeconds(0.30f);
            //SwitchAway();
        }

        private IEnumerator SwitchAwayNumerator()
        {
            anim.SetTrigger("Switch Away");
            yield return new WaitForSeconds(0.30f);
            SwitchTo();
        }

        private void SetIdle()
        {
            anim.SetTrigger("Idle");
            TextManager.Instance.UpdateAmmoText();
        }

        private IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canAttack = true;
        }

        // Used as Animation
        protected void SinMovement()
        {
            //pos += transform.up * Time.deltaTime * sinSpeed;
            //transform.position = pos + transform.up * Mathf.Sin(Time.time * sinFrequency) * sinMagnitude;
        }

        // Used as Animation
        protected void FixIdleAnimationPosition()
        {
            //Debug.Log("FixedIdleAnimation");
            transform.localPosition = startPos;
        }

        // Used as Animation
        private void FinishReload()
        {
            transform.localPosition = startPos;
            anim.SetTrigger("Idle");

            TextManager.Instance.UpdateAmmoText();
        }

        private IEnumerator MuzzleLight()
        {
            // Debug.Log(muzzleLightGo.gameObject.name);
            muzzleLightGo.gameObject.SetActive(true);

            yield return new WaitForSeconds(muzzleLightResetTime);

            muzzleLightGo.gameObject.SetActive(false);
        }

        // Used as animation event
        private void FinishShooting()
        {
            //Debug.Log("FinishShooting: " + canAttack);
            anim.SetTrigger("Idle");
            //canAttack = true;
        }

        private void Idle()
        {
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
}