using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThrowGrenade : MonoBehaviour
{

    public GameObject grenade;
   float holdDownStrtTime;
    float holdDownTime;
    float maxForceHoldDownTime = 1f;
    float holdTimeNormalizeForce;
    float holdTimeNormalizeUp;
    public float  maxForce;
    public float maxUpForce;
    

    float force = 15;
    float upwardForce = 15;
    [SerializeField]
    private float delay;
    float countDown;

    public Transform crossHair;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        throwGrenade();
  
    }

    void throwGrenade()
    {
      
        if (Input.GetMouseButtonDown(0))
        {     
            holdDownStrtTime = Time.time;

        }
        countDown -= Time.deltaTime;
        if  (Input.GetMouseButtonUp(0) && countDown <= 0)
        {

            holdDownTime = Time.time - holdDownStrtTime;
            GameObject grenadeClone = Instantiate(grenade, crossHair.position, crossHair.rotation);
            grenadeClone.GetComponent<Rigidbody>().AddRelativeForce(0, upwardForce, calculateHoldDownForce(holdDownTime), ForceMode.Impulse);
            countDown = delay;
            //grenadeClone.GetComponent<Rigidbody>().AddForce(Vector3.forward * calculateHoldDownForce(holdDownTime));
            
        }
    }

    private float calculateHoldDownForce(float holdTime)
    {
        holdTimeNormalizeForce = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        force = holdTimeNormalizeForce * maxForce;
        return force;
    }


}

