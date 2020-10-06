using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class AspectRatioManager : MonoBehaviour
{
    public GameObject[] bloodOverlays;
    public Vector3[] bloodOverlayScale;

    public GameObject HSBox;
    public GameObject dialogueHolder;
    public GameObject weaponHolder;
    public Vector3[] healthHolderPos;
    public Vector3[] weaponHolderPos;
    public Vector3[] dialogueHolderPos;

    void Awake()
    {
        if (Camera.main.aspect >= 1.8)
        {
            Debug.Log("21:9");
            foreach (var overlay in bloodOverlays)
            {
                RectTransform rt = overlay.GetComponent(typeof(RectTransform)) as RectTransform;
                rt.sizeDelta = new Vector2(bloodOverlayScale[0].x, bloodOverlayScale[0].y);
            }

            // RectTransform healthRect = healthHolder.GetComponent(typeof(RectTransform)) as RectTransform;
            // healthRect.localPosition = new Vector2(healthHolderPos[0].x, healthHolderPos[0].y);
            // healthRect.anchoredPosition = Vector3.zero;

            //RectTransform weaponRect = weaponHolder.GetComponent(typeof(RectTransform)) as RectTransform;
            //weaponRect.anchorMin = new Vector2(weaponHolderPos[0].x, weaponHolderPos[0].y);
        }
        else if (Camera.main.aspect >= 1.7)
        {
            Debug.Log("16:9");
            foreach (var overlay in bloodOverlays)
            {
                RectTransform rt = overlay.GetComponent(typeof(RectTransform)) as RectTransform;
                rt.sizeDelta = new Vector2(bloodOverlayScale[1].x, bloodOverlayScale[1].y);
            }

            RectTransform healthRect = HSBox.GetComponent(typeof(RectTransform)) as RectTransform;
            healthRect.localPosition = new Vector2(healthHolderPos[1].x, healthHolderPos[1].y);

            RectTransform weaponRect = weaponHolder.GetComponent(typeof(RectTransform)) as RectTransform;
            weaponRect.localPosition = new Vector2(weaponHolderPos[1].x, weaponHolderPos[1].y);

            RectTransform dialogueRect = dialogueHolder.GetComponent(typeof(RectTransform)) as RectTransform;
            dialogueRect.localPosition = new Vector2(dialogueHolderPos[1].x, dialogueHolderPos[1].y);
        }
        else if (Camera.main.aspect >= 1.5) Debug.Log("3:2");
        else Debug.Log("4:3");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
