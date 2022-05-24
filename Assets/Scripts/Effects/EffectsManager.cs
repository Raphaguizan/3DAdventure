using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume processVolume;

    [SerializeField]
    private float duration = .5f;
    [SerializeField]
    private Color _color;
    [SerializeField]
    private float _intensity = .5f;

    private Vignette _vignette;
    private Color initialColor;
    private float initialIntensity;

    private void Start()
    {
        Vignette tmp;
        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }
        initialColor = _vignette.color;
        initialIntensity = _vignette.intensity;
    }

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        
        ColorParameter c = new ColorParameter();
        float currentIntensity = initialIntensity;
        float currentDuration = duration / 2;
        float time = 0;

        while(time < currentDuration)
        {
            c.value = Color.Lerp(initialColor, _color, time/ currentDuration);
            currentIntensity = Mathf.Lerp(initialIntensity, _intensity, time/currentDuration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            _vignette.intensity.Override(currentIntensity);
            yield return new WaitForEndOfFrame();
        }
        
        time = 0;
        while (time < currentDuration)
        {
            c.value = Color.Lerp(_color, initialColor, time / currentDuration);
            currentIntensity = Mathf.Lerp(_intensity, initialIntensity, time/currentDuration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            _vignette.intensity.Override(currentIntensity);
            yield return new WaitForEndOfFrame();
        }
    }
}
