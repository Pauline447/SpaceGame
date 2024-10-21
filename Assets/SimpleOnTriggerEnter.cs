using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleOnTriggerEnter : MonoBehaviour
{
    [SerializeField] bool m_destroyCollidingGameObject;
    [SerializeField] bool m_destroyCollidingParent;
    [SerializeField] bool m_destroyThisGameObject;
    [SerializeField] bool m_destroyThisParentGameObject;
    [SerializeField] List<string> m_tag;
    [SerializeField] UnityEvent m_events;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in m_tag)
        {
            if (collision.tag == tag)
            {
                m_events.Invoke();

                if (m_destroyCollidingGameObject)
                {
                    Destroy(collision.gameObject);
                }
                if (m_destroyCollidingParent)
                {
                    Destroy(collision.transform.parent.gameObject);
                }
                if (m_destroyThisGameObject)
                {
                    Destroy(this.gameObject);
                }
                if (m_destroyThisParentGameObject)
                {
                    Destroy(this.transform.parent.gameObject);
                }
            }
        }
    }
}
