using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OnColliderEnter : MonoBehaviour
{
    public string m_tag;
    public CustomCollider m_collider;
    public UnityEvent m_eventEnter;
    
    bool m_isEnter;

    void Update()
    {
        CustomCollider col = CollisionManager.m_colliders.FirstOrDefault(c => c.gameObject.CompareTag(m_tag));
        if (col == null) return;

        if (m_collider.OnHoldCollision(col.m_renderer) && !m_isEnter)
        {
            m_isEnter = true;
            m_eventEnter?.Invoke();
        }
    }
}
