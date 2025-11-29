using System.Linq;
using PUCV.PhysicEngine2D;
using UnityEngine;

public class BowArrow : MonoBehaviour, IHasCollider
{
    public float m_speed;
    public float m_mass = 3;
    public PUCV.PhysicEngine2D.CustomCollider2D m_collider;
    [HideInInspector] public float m_maxForce;
    [HideInInspector] public Parable m_parable;
    [HideInInspector] public Flotability m_flotability;
    [HideInInspector] public Vector3 m_initialPosition;

    private bool m_inFlight;
    private bool m_inWater;

    private Vector2 startPos;
    private Vector3 velocity;
    private Vector2 gravity;

    private float time;

    void Update()
    {
        if (!m_inFlight) return;

        if (!m_inWater) UpdateParabolicMovement();
        else UpdateFloatingMovement();
    }

    public void Aim()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, m_initialPosition);
        m_parable.force = Mathf.Clamp(distance, 0, m_maxForce);
    }

    public void Shoot()
    {
        //-> PARABLE SETTER
        startPos = transform.position;
        velocity = m_parable.velocity;
        gravity = m_parable.gravity;

        m_inFlight = true;
        m_inWater = false;

        time = 0f;
    }

    void UpdateParabolicMovement()
    {
        time += Time.deltaTime * m_speed;
        Vector2 newPos = m_parable.GetRoutePosition(startPos, velocity, gravity, time);
        Vector2 newVel = m_parable.GetCurrentVelocity(velocity, gravity, time);

        transform.position = newPos;
        transform.up = newVel;
    }

    void EnterWater(PUCV.PhysicEngine2D.CustomCollider2D waterCol)
    {
        m_inWater = true;

        m_flotability.m_water = waterCol.GetComponent<Water>();
        Vector2 entryVelocity = m_parable.GetCurrentVelocity(velocity, gravity, time) * 5f;
        m_flotability.SetInitialVelocity(entryVelocity);
    }

    void UpdateFloatingMovement()
    {
        Bounds arrowBounds = m_collider.GetBounds();
        Bounds waterBounds = m_flotability.m_water.GetComponent<SpriteRenderer>().bounds;

        transform.position += m_flotability.GetRoutePosition(arrowBounds, waterBounds, m_mass) * m_speed;
        transform.up = m_flotability.GetCurrentVelocity();
    }

    public void OnInformCollisionEnter2D(CollisionInfo collisionInfo)
    {
        if (collisionInfo.otherCollider.gameObject.CompareTag("Water"))
        {
            Debug.Log("Entró el arrow al agua");
            EnterWater(collisionInfo.otherCollider);
        }
    }

    public void OnInformCollisionExit2D(CollisionInfo collisionInfo)
    {
    }
}
