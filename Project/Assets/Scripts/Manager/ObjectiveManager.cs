using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public int targetNumber = 10;
    public int currentNumber = 0;
    public GameObject eggDoor;

    public TMP_Text textMesh;

    public float defaultTypeTime = 0.029f;

    public float starTime = 6.9f;

    [SerializeField] private TextWriter objective;
    [SerializeField] private TextWriter currentObjective;
    [SerializeField] private GameObject objectivesGo;

    void Awake()
    {
        Instance = this;
        if (Instance == this) Debug.Log("Objective Manager Singleton Initialized");
    }

    void Start()
    {
        if (GameManager.Instance.level == 1) StartCoroutine(SetActive(starTime));
        else if (GameManager.Instance.level == 2) StartCoroutine(SetActive(starTime - 4.2f));

        Debug.Log("Found Door from Objective Manager");
    }

    public void UpdateTargetNumberObjective()
    {
        Debug.Log("Update Text Objective");

        if (GameManager.Instance.level == 2)
        {
            textMesh.text = "Exterminate Eggs " + currentNumber + "/" + targetNumber;
            //textMesh.text = "Destroy the eggs ";
        }

        if (currentNumber >= targetNumber)
        {
            if (GameManager.Instance.level == 2)
            {
                StartCoroutine(TypeObjective("Regroup with Kaichi", 0.069f, 2.9f));
                eggDoor = GameObject.Find("IMPORTANT Door near Stage (1)");
                StartCoroutine(eggDoor.GetComponent<ImportantDoor>().PlayAnimation());
            }
            Debug.Log("Objective Finished");
        }
    }

    public IEnumerator SetActive(float time)
    {
        yield return new WaitForSeconds(time);
        objectivesGo.SetActive(true);
        objective.AddWriter("", defaultTypeTime, true);
        currentObjective.AddWriter("", defaultTypeTime, true);

        if (GameManager.Instance.level == 1)
        {
            yield return new WaitForSeconds(2.9f);
            currentObjective.AddWriter("Current Objective:", 0.09f, true);
            yield return new WaitForSeconds(0.69f);
            StartCoroutine(TypeObjective("Find a Weapon", 0.075f, 0f));
        }
        else if (GameManager.Instance.level == 2)
        {
            currentObjective.AddWriter("Current Objective:", 0.09f, true);
            yield return new WaitForSeconds(0.69f);
            StartCoroutine(TypeObjective("Exterminate Eggs " + currentNumber + "/" + targetNumber, 0.069f, 0f));
        }
    }

    public IEnumerator TypeObjective(string objectiveText, float time, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        objective.AddWriter(" ", defaultTypeTime, true);
        yield return new WaitForSeconds(1.114f);
        objective.AddWriter(objectiveText, defaultTypeTime + time, true);
    }
}
