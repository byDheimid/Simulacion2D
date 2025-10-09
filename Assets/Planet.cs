using UnityEditor.ShaderGraph;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public NaveEspacial m_player;
    public float m_distanceDetection;
    public Vector3 m_gravity;

    public float m_mass;
    public bool m_detected;

    void Update()
    {
        Vector3 direction = transform.position - m_player.transform.position; // Se usará luego.
        float ditance = direction.magnitude;

        if (ditance < m_distanceDetection)
        {
            if (!m_detected)
            {
                Debug.Log("Planeta actual es " + this.gameObject.name);
                m_player.m_currentPlanet = this;
                m_detected = true;
            }

        }
        else
        {
            if (m_detected)
            {
                Debug.Log("ya no hay planeta");
                m_player.m_currentPlanet = null;
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
