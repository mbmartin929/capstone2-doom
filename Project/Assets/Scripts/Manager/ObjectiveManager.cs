using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public float defaultTypeTime = 0.029f;

    public float starTime = 6.9f;

    [SerializeField] private TextWriter objective;
    [SerializeField] private TextWriter currentObjective;
    [SerializeField] private GameObject ObjectivesGo;

    void Awake()
    {
        Instance = this;
        if (Instance == this) Debug.Log("Objective Manager Singleton Initialized");

        StartCoroutine(SetActive(starTime));
    }

    void Start()
    {

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
            StartCoroutine(TypeObjective("Find a Weapon", 0.075f));
        }
        else
        {
            yield return new WaitForSeconds(2.69f);
        }
    }

    public IEnumerator TypeObjective(string objectiveText, float time)
    {
        yield return new WaitForSeconds(1.69f);

        objective.AddWriter(objectiveText, defaultTypeTime + time, true);
    }
}
