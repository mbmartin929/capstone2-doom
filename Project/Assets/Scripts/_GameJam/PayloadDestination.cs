using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PayloadDestination : MonoBehaviour
{
    //public Image loadingImage;
    public UnityEngine.UI.Image image;

    private GameObject payloadGo;

    public float radius = 6.9f;

    public float waitTime = 6.9f;
    public bool arrived = false;

    private bool startLoading = false;

    private float timer = 0f;
    private float displayValue = 0f;

    public int destinationID = 0;

    private AudioSource audioSource;
    private Payload payload;

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
        payloadGo = GameManager.Instance.cargoGo;
        payload = payloadGo.GetComponent<Payload>();
    }

    // Update is called once per frame
    void Update()
    {
        payloadGo = GameManager.Instance.cargoGo;
        payload = payloadGo.GetComponent<Payload>();

        if ((Vector3.Distance(transform.position, payloadGo.transform.position) < radius && !arrived))
        {
            Debug.Log("Arrived");
            arrived = true;
            StartCoroutine(CollidePayload(payload));
        }


        if (startLoading)
        {
            //audioSource.Play();
            Debug.Log("Loading");
            timer += Time.deltaTime / waitTime;
            displayValue = Mathf.Lerp(0, 1.0f, timer);
            image.fillAmount = displayValue;
        }

        if (image.fillAmount >= 1.0f && destinationID == 0)
        {
            //audioSource.Play();
            Debug.Log("Play First Email");
            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FirstEmail());
            payload.curMoveSpeed = payload.defaultMoveSpeed;
            Destroy(gameObject, 0f);
        }
        else if (image.fillAmount >= 1.0f && destinationID == 1)
        {
            //audioSource.Play();
            Debug.Log("Play Second Email");
            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.SecondEmail());
            payload.curMoveSpeed = payload.defaultMoveSpeed;
            Destroy(gameObject, 0f);
        }
        else if (image.fillAmount >= 1.0f && destinationID == 2)
        {
            //audioSource.Play();
            Debug.Log("Play Third Email");
            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.ThirdEmail());
            payload.curMoveSpeed = payload.defaultMoveSpeed;
            Destroy(gameObject, 0f);
        }
        else if (image.fillAmount >= 1.0f && destinationID == 3)
        {
            //audioSource.Play();
            Debug.Log("Play Fourth Email");
            DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FourthEmail());
            payload.curMoveSpeed = payload.defaultMoveSpeed;
            Destroy(gameObject, 0f);
        }
    }

    private IEnumerator CollidePayload(Payload payload)
    {
        payload.curMoveSpeed = 0;

        startLoading = true;

        yield return null;
        // yield return new WaitForSeconds(waitTime);

        // audioSource.Play();

        // if (destinationID == 0)
        // {
        //     Debug.Log("Play First Email");
        //     DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FirstEmail());
        //     payload.curMoveSpeed = payload.defaultMoveSpeed;
        //     this.enabled = false;
        // }
        // else if (destinationID == 1)
        // {
        //     Debug.Log("Play Second Email");
        //     DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.SecondEmail());
        //     payload.curMoveSpeed = payload.defaultMoveSpeed;
        //     this.enabled = false;
        // }
        // else if (destinationID == 2)
        // {
        //     Debug.Log("Play Third Email");
        //     DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.ThirdEmail());
        //     payload.curMoveSpeed = payload.defaultMoveSpeed;
        //     this.enabled = false;
        // }
        // else if (destinationID == 3)
        // {
        //     Debug.Log("Play Fourth Email");
        //     DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.FourthEmail());
        //     payload.curMoveSpeed = payload.defaultMoveSpeed;
        //     this.enabled = false;
        // }

        // Debug.Log("Continue to next waypoint");

        // //payload.current++;

        // Debug.Log("Payload going next");

        // StartCoroutine(DisableComponent());
    }

    private IEnumerator DisableComponent()
    {
        yield return new WaitForSeconds(21f);
        this.enabled = false;
    }
}
