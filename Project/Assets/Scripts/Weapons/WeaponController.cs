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
        public int maxCapacity;
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

        #endregion

        #region Private Variables
        protected GameObject cameraGo;
        [SerializeField] protected bool canAttack;
        [HideInInspector] public Animator anim;

        #endregion

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
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<EnemyAI>() != null)
                {
                    Debug.Log(hitCollider.gameObject.name + "is chasing");
                    hitCollider.GetComponent<EnemyAI>().ChasePlayer();
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
        private void FinishReload()
        {
            anim.SetTrigger("Idle");
            TextManager.Instance.UpdateAmmoText();
        }

        // public void PickUpAmmo(int amount)
        // {
        //     maxAmmo += amount;
        //     if (maxAmmo >= maxCapacity) maxAmmo = maxCapacity;
        //     TextManager.Instance.UpdateAmmoText();
        // }

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

        protected void HitLevel(RaycastHit hit)
        {
            // Raycast hits Level

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