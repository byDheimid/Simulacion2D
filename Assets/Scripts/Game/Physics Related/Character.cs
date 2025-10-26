using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float m_speed;
    public float m_mass;
    public float m_gravity;
    public CustomCollider m_collider;
    public List<CustomSurface> m_surfaces;
    Vector2 m_movement;

    float m_initialGravity;

    void Start()
    {
        m_initialGravity = m_gravity;
    }

    void Update()
    {
        Vector2 downForce = m_mass * Vector2.down * m_gravity;
        m_movement = new(Input.GetAxisRaw("Horizontal") * m_speed * Time.deltaTime, downForce.y * Time.deltaTime);

        m_gravity = m_surfaces.Any(c => c.m_collider.OnHoldCollision(m_collider.m_renderer)) ? 0 : m_initialGravity;
        transform.position += (Vector3)m_movement;
    }

    public void AddSurface(CustomSurface surface, bool isWater = false)
    {
        m_surfaces.Add(!m_surfaces.Contains(surface) ? surface : null);
        if (isWater) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    } 

    public void RemoveSurface(CustomSurface surface, bool isWater = false)
    {
        m_surfaces.Remove(m_surfaces.Contains(surface) ? surface : null);
    } 
}
