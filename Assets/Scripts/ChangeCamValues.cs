using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamValues : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_vCam;
    [SerializeField] private float m_camSize;
    [SerializeField] private float m_zoomSpeed;

    private float m_timeElapsed;
    private bool m_lerpCam;
    private float m_prevSize;

    private string m_playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_lerpCam && m_vCam.m_Lens.OrthographicSize != m_camSize)
        {
            LerpCamZoom(m_camSize, m_zoomSpeed);
        }
        else
        {
            m_lerpCam = false;
            m_timeElapsed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == m_playerTag && !m_lerpCam)
        {
            m_prevSize = (float)m_vCam.m_Lens.OrthographicSize;
            m_lerpCam = true;
        }
    }

    private void LerpCamZoom(float m_targetSize, float zoomTime)
    {
        m_vCam.m_Lens.OrthographicSize = Mathf.Lerp(m_prevSize, m_targetSize, m_timeElapsed / m_zoomSpeed);
        m_timeElapsed += Time.deltaTime;
    }
}
