using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    //private float timeShake;
    private CinemachineBasicMultiChannelPerlin perlin;
    private CinemachineVirtualCamera cinemachineVC;
    [SerializeField] private float shakeTimer = 0f;
    [SerializeField] private float startIntensity =0f;
    [SerializeField] private float shakeTimerTotal = 0f;

    private void Start()
    {
        MyEventManager.OnCameraShake.AddListener(Shake);
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
        perlin = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0f;
    }

    private void Shake(float intensity, float time)
    {
        Debug.Log("Shake");
        perlin.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            perlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));

        }
    }

}
