using UnityEngine;

public class Atraction : MonoBehaviour
{
    public Bow m_bow;
    public float m_distanceDetection, m_mass;
    public Vector3 m_gravity;
    public bool m_detected;

    void Update()
    {
        if (m_bow.m_arrows != null)
        {
            BowArrow arrow = m_bow.m_arrows[0];

            Vector3 direction = transform.position - arrow.transform.position; // Se usará luego.
            float distance = direction.magnitude;

            if (distance < m_distanceDetection)
            {
                if (!m_detected)
                {
                    Debug.Log("Planeta actual es " + this.gameObject.name);
                    // m_bow.AddPlanet(this);
                    m_detected = true;
                }

            }
            else
            {
                if (m_detected)
                {
                    Debug.Log("ya no hay planeta");
                    // m_bow.RemovePlanet(this);
                    m_detected = false;
                }

            }
        }
    }

    public Vector3 GetGravityForce()
    {
        Vector3 direction = transform.position - m_bow.transform.position; // Se usará luego.
        float gravityForce = 1 * m_mass * m_bow.m_arrows[0].m_mass / (direction.magnitude * direction.magnitude);
        return direction.normalized * gravityForce;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_distanceDetection);
    }
}
