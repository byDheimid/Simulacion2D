using UnityEngine;

public class Bola : MonoBehaviour
{
    public float mass, volume, initialSpeed, gravityMagnitude, dampingInWater, dampingInAir;

    public Water m_water;

    SpriteRenderer m_sp;
    Vector2 m_velocity;
    void Awake()
    {
        TryGetComponent(out m_sp);
        m_velocity = initialSpeed * Vector2.down;
    } 

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 downForce = mass * Vector2.down * gravityMagnitude;

        Bounds ball = m_sp.bounds;
        Bounds waterBounds = m_water.GetComponent<SpriteRenderer>().bounds;
        if (ball.Intersects(waterBounds)) Debug.Log("Entramos al agua");

        m_velocity += downForce * Time.deltaTime;
        transform.position += (Vector3)m_velocity * Time.deltaTime;
    }
}
