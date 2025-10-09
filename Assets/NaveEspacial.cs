using UnityEngine;

public class NaveEspacial : MonoBehaviour
{
   public float m_mass;
    public float m_speed;
    public bool m_canMove = false;
    public Planet m_currentPlanet;
    public Vector3 m_gravityForce;
    private Vector3 m_veclocity;

    void Start()
    {
        m_veclocity = Vector3.right * m_speed;
    }

    void Update()
    {
        Gravity();
        Move();
    }

    void Gravity()
    {
        if (m_currentPlanet != null) 
            m_gravityForce = m_currentPlanet.GetGravityForce();
    }

    void Move()
    {
        m_veclocity += m_gravityForce * Time.deltaTime;
        transform.position += m_veclocity * Time.deltaTime;
    }
}
