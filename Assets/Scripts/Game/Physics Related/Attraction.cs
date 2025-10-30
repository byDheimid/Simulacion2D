using UnityEngine;

public class Attraction : MonoBehaviour
{
    public AttractiveObject m_player;
    public float m_distanceDetection, m_mass;
    public Vector3 m_gravity;
    public bool m_detected;

    void Update()
    {
        Vector3 direction = transform.position - m_player.transform.position; // Se usará luego.
        float distance = direction.magnitude;

        if (distance < m_distanceDetection)
        {
            if (!m_detected)
            {
                Debug.Log("Planeta actual es " + this.gameObject.name);
                m_player.AddAttraction(this);
                m_detected = true;
            }

        }
        else
        {
            if (m_detected)
            {
                Debug.Log("ya no hay planeta");
                m_player.RemoveAttraction(this);
                m_detected = false;
            }

        }
    }

    public Vector3 GetGravityForce()
    {
        Vector3 direction = transform.position - m_player.transform.position; // Se usará luego.
        float gravityForce = 1 * m_mass * m_player.m_mass / (direction.magnitude * direction.magnitude);
        return direction.normalized * gravityForce;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_distanceDetection);
    }
}
