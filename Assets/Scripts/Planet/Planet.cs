using UnityEngine;
using System.Collections.Generic;

public class Planet : MonoBehaviour
{
    public float m_distanceDetection, m_mass;

    private List<NaveEspacial> m_detectedShips = new();
    private NaveEspacial[] m_allShips;

    void Start()
    {
        // Obtener todas las naves espaciales presentes en la escena
        m_allShips = FindObjectsOfType<NaveEspacial>();
    }

    void Update()
    {
        foreach (var ship in m_allShips)
        {
            float distance = Vector3.Distance(transform.position, ship.transform.position);

            bool isInside = distance < m_distanceDetection;
            bool wasInside = m_detectedShips.Contains(ship);

            if (isInside && !wasInside)
            {
                Debug.Log("Planeta detectó a " + ship.name);
                m_detectedShips.Add(ship);
                ship.AddPlanet(this);
            }
            else if (!isInside && wasInside)
            {
                Debug.Log("Nave salió del planeta: " + ship.name);
                m_detectedShips.Remove(ship);
                ship.RemovePlanet(this);
            }
        }
    }

    public Vector3 GetGravityForce(NaveEspacial ship)
    {
        Vector3 direction = transform.position - ship.transform.position;
        float gravityForce = 1 * m_mass * ship.m_mass / (direction.sqrMagnitude);
        return direction.normalized * gravityForce;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_distanceDetection);
    }
}
