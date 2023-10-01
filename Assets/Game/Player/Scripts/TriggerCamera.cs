using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private Transform player;
    [SerializeField] private float resizeCameraOnTriger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            virtualCam.Follow = player;
            virtualCam.m_Lens.OrthographicSize = resizeCameraOnTriger;
            Destroy(this.gameObject);
        }
    }

}
