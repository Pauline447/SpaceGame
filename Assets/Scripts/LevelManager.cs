using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float m_standardZoomValue;

    private CinemachineVirtualCamera m_vCam;
    // Start is called before the first frame update
    void Start()
    {
        m_vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        m_vCam.m_Lens.OrthographicSize = m_standardZoomValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
