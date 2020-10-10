using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PayloadDestination : MonoBehaviour
{
    //public Image loadingImage;
    public UnityEngine.UI.Image image;

    public GameObject payloadGo;

    public float radius = 6.9f;

    public float waitTime = 6.9f;
    public bool arrived = false;

    public float activeTime = 0f;

    private bool startLoading = false;

    private float timer = 0f;
    private float displayValue = 0f;

    public int destinationID = 0;

    private AudioSource audioSource;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // image.minValue = Time.time;
        // image.maxValue = Time.time + waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, payloadGo.transform.position) < radius && !arrived))
        {
            arrived = true;
            Payload payload = payloadGo.GetComponent<Payload>();
            StartCoroutine(CollidePayload(payload));
        }


        if (startLoading)
        {
            timer += Time.deltaTime / waitTime;
            displayValue = Mathf.Lerp(0, 1.0f, timer);
            image.fillAmount = displayValue;
        }
    }

    private IEnumerator CollidePayload(Payload payload)
    {
        payload.curMoveSpeed = 0;

        startLoading = true;

        yield return new WaitForSeconds(waitTime);

        audioSource.Play();

        if (destinationID == 0)
        {
            Debug.Log("Play First Email");
            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FirstEmail());
        }

        Debug.Log("Continue to next waypoint");

        //payload.current++;
        payload.curMoveSpeed = payload.defaultMoveSpeed;

        Debug.Log("Payload going next");
    }
}
