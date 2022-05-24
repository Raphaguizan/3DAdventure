using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _amplitude;
    [SerializeField]
    private float _frequency;
    [SerializeField]
    private float time;

    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(time);
    }
    public void Shake(float time)
    {
        StartCoroutine(ShakeTimer(time));
    }

    IEnumerator ShakeTimer(float time)
    {
        perlinNoise.m_AmplitudeGain = _amplitude;
        perlinNoise.m_FrequencyGain = _frequency;
        yield return new WaitForSeconds(time);
        perlinNoise.m_AmplitudeGain = 0f;
        perlinNoise.m_FrequencyGain = 0f;
    }
}
