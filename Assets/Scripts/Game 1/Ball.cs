using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float gravity = -9.8f;
    public float bounceDamping = 0.7f;
    public float speedMultiplier = 1f;
    public CustomCollider m_collider;

    private Vector2 velocity;
    private bool isMoving;

    public void Launch(Vector2 initialVelocity)
    {
        velocity = initialVelocity;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        // Aplicar gravedad
        velocity += new Vector2(0, gravity) * Time.deltaTime;

        // Movimiento
        Vector3 prevPos = transform.position;
        Vector3 newPos = prevPos + (Vector3)(velocity * Time.deltaTime * speedMultiplier);
        transform.position = newPos;

        foreach (var groundCol in CollisionManager.m_colliders)
        {
            if (!groundCol.gameObject.CompareTag("Ground")) 
                continue;

            if (m_collider.OnHoldCollision(groundCol.m_renderer))
            {
                Debug.Log("Tocamos piso con: " + groundCol.name);
                Bounce(Vector2.up);
                break; // ← salimos del loop una vez que rebota
            }
        }
    }

    void Bounce(Vector2 normal)
    {
        // Solo rebota si está cayendo
        if (velocity.y < 0f)
        {
            velocity = Vector2.Reflect(velocity, normal) * bounceDamping;
            transform.position += Vector3.up * 0.05f; 
        }
    }
}
