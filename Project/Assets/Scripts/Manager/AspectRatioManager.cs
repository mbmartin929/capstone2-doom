using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class AspectRatioManager : MonoBehaviour
{
    public Vector3 ammoIconRectVector3 = new Vector3(839.7f, -316.7f, 17f);
    public Vector3 WBorderRectVector3 = new Vector3(855.29f, -321.1f, 47f);

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

            //RectTransform weaponRect = weaponHolder.GetComponent(typeof(RectTransform)) as RectTransform;

            // weaponRect.pivot = new Vector2(0.5f, -0.2f);
            // weaponRect.anchorMax = new Vector2(0.29f, 0.5f);

            // Code Below is required for 21:9 fix
            //weaponRect.localPosition = new Vector2(weaponHolderPos[1].x, weaponHolderPos[1].y);

            // float targetaspect = 16.0f / 9.0f;
            // float windowAspect = (float)Screen.width / (float)Screen.height;
            // float scaleHeight = windowAspect / targetaspect;

            // float scaleWidth = 1.0f / scaleHeight;

            // // if scaled height is less than current height, add letterbox
            // if (scaleHeight < 1.0f)
            // {
            //     Rect rect = Camera.main.rect;

            //     rect.width = 1.0f;
            //     rect.height = scaleHeight;
            //     rect.x = 0;
            //     rect.y = (1.0f - scaleHeight) / 2.0f;

            //     Camera.main.rect = rect;
            // }
            // else // add pillarbox
            // {
            //     float scalewidth = 1.0f / scaleHeight;

            //     Rect rect = Camera.main.rect;

            //     rect.width = scalewidth;
            //     rect.height = 1.0f;
            //     rect.x = (1.0f - scalewidth) / 2.0f;
            //     rect.y = 0;

            //     Camera.main.rect = rect;
            // }

            RectTransform healthRect = HSBox.GetComponent(typeof(RectTransform)) as RectTransform;
            healthRect.localPosition = new Vector2(healthHolderPos[1].x, healthHolderPos[1].y);

            RectTransform weaponRect = weaponHolder.GetComponent(typeof(RectTransform)) as RectTransform;
            weaponRect.localPosition = new Vector2(weaponHolderPos[1].x, weaponHolderPos[1].y);
            weaponRect.pivot = new Vector2(2.0f, 0.35f);

            // RectTransform ammoIconRect = weaponHolder.transform.GetChild(2).GetComponent(typeof(RectTransform)) as RectTransform;
            // ammoIconRect.localPosition = ammoIconRectVector3;

            // RectTransform WBorderRect = weaponHolder.transform.GetChild(3).GetComponent(typeof(RectTransform)) as RectTransform;
            // WBorderRect.localPosition = WBorderRectVector3;

            RectTransform dialogueRect = dialogueHolder.GetComponent(typeof(RectTransform)) as RectTransform;
            dialogueRect.localPosition = new Vector2(dialogueHolderPos[1].x, dialogueHolderPos[1].y);
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
            weaponRect.pivot = new Vector2(2.0f, 0.35f);

            RectTransform ammoIconRect = weaponHolder.transform.GetChild(2).GetComponent(typeof(RectTransform)) as RectTransform;
            ammoIconRect.localPosition = ammoIconRectVector3;

            RectTransform WBorderRect = weaponHolder.transform.GetChild(3).GetComponent(typeof(RectTransform)) as RectTransform;
            WBorderRect.localPosition = WBorderRectVector3;

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
