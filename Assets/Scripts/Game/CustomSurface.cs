using UnityEngine;

public class CustomSurface : MonoBehaviour
{
    public Character m_player;
    public CustomCollider m_collider;

    public bool m_isWater;
    bool m_detected;

    void Update()
    {
        if (m_collider.OnHoldCollision(m_player.m_collider.m_renderer))
        {
            if (!m_detected)
            {
                Debug.Log("actual surface " + this.gameObject.name);
                m_player.AddSurface(this, m_isWater);
                m_detected = true;
            }
        }
        else
        {
            if (m_detected)
            {
                Debug.Log("ya no hay planeta");
                m_player.RemoveSurface(this);
                m_detected = false;
            }

        }
    }
}
