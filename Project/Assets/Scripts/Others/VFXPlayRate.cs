using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayRate : MonoBehaviour
{
    public float playRate = 4.2f;
    public float destroyTime = 0.1f;
    private VisualEffect vfx;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();

        vfx.playRate = playRate;

        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            vfx.SetFloat("Rate", 0);

            //vfx.Stop();
            Destroy(gameObject, destroyTime);


            //Vector3.Lerp(vfx.SetFloat("Rate", 20), vfx.SetFloat("Rate", 0), 0.1f);
        }
    }
}
