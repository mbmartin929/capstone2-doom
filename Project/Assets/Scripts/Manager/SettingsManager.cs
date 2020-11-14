using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering;
using TMPro;
// using UnityEngine.Experimental.Rendering.HDPipeline;
// using UnityEngine.Experimental.Rendering.URPipeline;
// using UnityEngine.Rendering.URPipeline;

public class SettingsManager : MonoBehaviour
{
    // Instantiates Singleton
    public static SettingsManager Instance { set; get; }

    public TextMeshPro curGammaText;
    public float maxGamma;
    public float curGamma;
    public float minGamma;

    private Volume volume;

    private LiftGammaGain liftGammaGain;

    void Awake()
    {
        // Sets Singleton
        Instance = this;

        if (Instance == this) Debug.Log("SettingsManager Singleton Initialized");


    }

    private void Start()
    {
        // volume = GameManager.Instance.volume;
        // volume.profile.TryGet(out liftGammaGain);
        // curGamma = liftGammaGain.gamma.value.w;
        // curGammaText.text = "" + (liftGammaGain.gamma.value.w * 10).ToString("F0");
    }

    private void Update()
    {
        // Default Gamma is 0.1

        // if (Input.GetKeyDown(KeyCode.Q))
        // {

        //     IncreaseGamma();
        // }
        // if (Input.GetKeyDown(KeyCode.E))
        // {

        //     DecreaseGamma();
        // }
    }

    public void RestartSettingsManager()
    {
        volume = GameManager.Instance.volume;
        volume.profile.TryGet(out liftGammaGain);
        curGamma = liftGammaGain.gamma.value.w;
        curGammaText.text = "" + (liftGammaGain.gamma.value.w * 10).ToString("F0");

        Debug.Log("Found New Settings");
    }

    public void IncreaseGamma()
    {
        Vector4 newGamma = new Vector4(0, 0, 0, 0.1f);

        if (curGamma >= maxGamma) return;
        liftGammaGain.gamma.value += newGamma;
        curGammaText.text = "" + (liftGammaGain.gamma.value.w * 10).ToString("F0");
    }

    public void DecreaseGamma()
    {
        Vector4 newGamma = new Vector4(0, 0, 0, 0.1f);

        if (curGamma <= minGamma) return;
        liftGammaGain.gamma.value -= newGamma;
        curGammaText.text = "" + (liftGammaGain.gamma.value.w * 10).ToString("F0");
    }
}
