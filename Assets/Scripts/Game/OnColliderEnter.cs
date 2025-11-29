using System.Linq;
using PUCV.PhysicEngine2D;
using UnityEngine;
using UnityEngine.Events;

public class OnColliderEnter : MonoBehaviour, IHasCollider
{
    public string m_tag;
    public UnityEvent m_enterEvent;
    public UnityEvent m_exitEvent;

    public void OnInformCollisionEnter2D(CollisionInfo collisionInfo)
    {
        if (collisionInfo.otherCollider.gameObject.CompareTag(m_tag))
        {
            m_enterEvent?.Invoke();
        }
    }

    public void OnInformCollisionExit2D(CollisionInfo collisionInfo)
    {
        if (collisionInfo.otherCollider != null)
        {
            if (collisionInfo.otherCollider.gameObject.CompareTag(m_tag))
            {
                m_exitEvent?.Invoke();
            }
        }
    }
}