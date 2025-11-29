using System.Linq;
using PUCV.PhysicEngine2D;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float gravity = -9.8f;
    public float bounceDamping = 0.7f;
    public float speedMultiplier = 1f;

    public CustomRigidbody2D m_cRbd;
    private bool isMoving;

    public void Launch(Vector2 initialVelocity)
    {
        m_cRbd.velocity = initialVelocity;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        // Aplicar gravedad
        m_cRbd.velocity += new Vector2(0, gravity) * Time.deltaTime;

        // Movimiento
        Vector3 prevPos = transform.position;
        Vector3 newPos = prevPos + (Vector3)(m_cRbd.velocity * Time.deltaTime * speedMultiplier);
        transform.position = newPos;
    }

    public void OnInformCollisionExit2D(CollisionInfo collisionInfo)
    {
        
    }
}
