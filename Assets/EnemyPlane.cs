using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed =5f;
    [SerializeField] private GameObject m_normal;
    [SerializeField] private GameObject m_broken;

    [SerializeField] private AudioClip m_breakAudio;
    [SerializeField] private AudioClip m_startAudio;

    private AudioSource m_audioSource;
    private float m_currentMoveSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_currentMoveSpeed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * m_currentMoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Planet") && m_normal.activeSelf)
        {
            m_normal.SetActive(false);
            m_broken.SetActive(true);

            m_audioSource.clip = m_breakAudio;
            m_audioSource.Play();

            m_currentMoveSpeed = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ActivatePlane" && m_broken.activeSelf)
        {
            Debug.Log("ActivatePlane");
            m_normal.SetActive(true);
            m_broken.SetActive(false);

            m_currentMoveSpeed = m_moveSpeed;

            m_audioSource.clip = m_startAudio;
            m_audioSource.Play();
        }
    }
}
