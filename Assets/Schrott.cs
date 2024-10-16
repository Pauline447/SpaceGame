using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schrott : MonoBehaviour
{
    [SerializeField] private GameObject m_normal;
    [SerializeField] private GameObject m_broken;

    private AudioSource m_audioSource;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" &&m_normal.activeSelf)
        {
            m_normal.SetActive(false);
            m_broken.SetActive(true);

            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.Play();

        }
    }
}
