using System.Collections.Generic;
using UnityEngine;

public class AttractiveObject : MonoBehaviour
{
    public float m_mass, m_speed;
    public List<Attraction> m_attractions;
    public Vector3 m_gravityForce, m_velocity;
    public Transform target;

    void Start() => m_velocity = Vector3.right * m_speed;

    float timer;

    void Update()
    {
        Gravity();
        Move();
        Rotation();

        timer += Time.deltaTime;
        if (timer>=12)
        {
            Teleport();
            timer = 0;
        }
    }

    void Gravity()
    {
        m_gravityForce = Vector2.zero;
        m_attractions?.ForEach(c => m_gravityForce += c.GetGravityForce());
    }

    public void AddAttraction(Attraction attraction) => m_attractions.Add(!m_attractions.Contains(attraction) ? attraction : null);
    public void RemoveAttraction(Attraction attraction) => m_attractions.Remove(m_attractions.Contains(attraction) ? attraction : null);

    void Move()
    {
        m_velocity += m_gravityForce * Time.deltaTime;
        transform.position += m_velocity * Time.deltaTime;
    }

    void Rotation() => transform.up = m_velocity.normalized;

    public void Teleport()
    {
        m_velocity = Vector3.right * m_speed;
        transform.position = target.position;
    }
}