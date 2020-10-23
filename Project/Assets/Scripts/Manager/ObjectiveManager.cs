using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public int targetNumber = 10;
    public int currentNumber = 0;
    public TMP_Text textMesh;

    public float defaultTypeTime = 0.029f;

    public float starTime = 6.9f;

    [SerializeField] private TextWriter objective;
    [SerializeField] private TextWriter currentObjective;
    [SerializeField] private GameObject ObjectivesGo;

    void Awake()
    {
        Instance = this;
        if (Instance == this) Debug.Log("Objective Manager Singleton Initialized");
    }

    void Start()
    {
        if (GameManager.Instance.level == 1) StartCoroutine(SetActive(starTime));
        else if (GameManager.Instance.level == 2) StartCoroutine(SetActive(starTime - 4.2f));
    }

    public void UpdateTargetNumberObjective()
    {
        Debug.Log("Update Text Objective");
        textMesh.text = "Exterminate Eggs " + currentNumber + "/" + targetNumber;

        if (currentNumber >= targetNumber)
        {
            Debug.Log("Objective Finished");
        }
    }

    private IEnumerator SetActive(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectivesGo.SetActive(true);
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
