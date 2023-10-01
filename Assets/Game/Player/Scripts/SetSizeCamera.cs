using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSizeCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    [SerializeField] private float resizeCameraOnStart;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        virtualCam.m_Lens.OrthographicSize = resizeCameraOnStart;
    }
}