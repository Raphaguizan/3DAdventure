using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeathCamEffect : MonoBehaviour
{
    [Header("Effect"), SerializeField]
    private PostProcessVolume processVolume;
    [SerializeField]
    private int saturation = -90;
    [SerializeField]
    private float duration = 1f;

    private ColorGrading _cGrading;
    private FloatParameter _saturationParameter = new FloatParameter();
    private float _initialSaturation;

    private void Awake()
    {
        ColorGrading tmp;
        if (processVolume.profile.TryGetSettings<ColorGrading>(out tmp))
        {
            _cGrading = tmp;
        }
        _initialSaturation = _cGrading.saturation.value;
    }

    [NaughtyAttributes.Button]
    public void ActiveEffect()
    {
        StartCoroutine(ApplyEffect());
    }
    
    IEnumerator ApplyEffect()
    {
        float time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            _saturationParameter.value = (int) Mathf.Lerp(_initialSaturation, saturation, time/duration);
            _cGrading.saturation.Override(_saturationParameter);
            yield return new WaitForEndOfFrame();
        }

    }
}
