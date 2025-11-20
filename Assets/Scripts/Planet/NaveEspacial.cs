using System.Collections.Generic;
using UnityEngine;

public class NaveEspacial : MonoBehaviour
{
   public float m_mass, m_speed;
    public List<Planet> m_planets;
    public Vector3 m_gravityForce, m_velocity;

    void Start() => m_velocity = Vector3.right * m_speed;

    void Update()
    {
        Gravity();
        Move();
        Rotation();
    }

    void Gravity()
    {
        m_gravityForce = Vector2.zero;
        m_planets?.ForEach(p => m_gravityForce += p.GetGravityForce(this));
    }

    public void AddPlanet(Planet planet)
    {
        if (!m_planets.Contains(planet))
            m_planets.Add(planet);
    }

    public void RemovePlanet(Planet planet)
    {
        if (m_planets.Contains(planet))
            m_planets.Remove(planet);
    }
    void Move()
    {
        m_velocity += m_gravityForce * Time.deltaTime;
        transform.position += m_velocity * Time.deltaTime;
    }

    void Rotation() => transform.up = m_velocity.normalized;
}
