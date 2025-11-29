using System.Collections.Generic;
using System.Linq;
using PUCV.PhysicEngine2D;
using UnityEngine;

public class Character : MonoBehaviour, IHasCollider
{
    public CustomRigidbody2D m_cRbd;
    public PUCV.PhysicEngine2D.CustomCollider2D m_col;

    public float m_speed;
    public float m_mass;
    public float m_gravity;
    Vector2 m_movement;

    public bool m_isGrounded;

    public void OnInformCollisionEnter2D(CollisionInfo collisionInfo)
    {
        if (collisionInfo.otherCollider != null) m_isGrounded = true;
        else m_isGrounded = false;
    }

    public void OnInformCollisionExit2D(CollisionInfo collisionInfo)
    {
        if (collisionInfo != null) m_isGrounded = false;
    }

    void FixedUpdate()
    {
        float downForce = m_mass * Vector2.down.y * m_gravity;
        m_movement = new(Input.GetAxisRaw("Horizontal") * m_speed, downForce);

        if (m_isGrounded) 
        {
            m_movement.y = 0;
        }

        m_cRbd.velocity = m_movement;
    }

    public void Teleport(Transform target)
    {
        transform.position = target.position;
    }
}