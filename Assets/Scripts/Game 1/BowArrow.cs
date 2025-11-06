using System.Linq;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    public float m_speed;
    public float m_mass = 3;
    public CustomCollider m_collider;
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

        CustomCollider waterCol = CollisionManager.m_colliders.FirstOrDefault(c => c.gameObject.CompareTag("Water"));
        if (waterCol != null && m_collider.OnHoldCollision(waterCol.m_renderer)) if (!m_inWater) EnterWater(waterCol);

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

    void EnterWater(CustomCollider waterCol)
    {
        m_inWater = true;

        m_flotability.m_water = waterCol.GetComponent<Water>();
        Vector2 entryVelocity = m_parable.GetCurrentVelocity(velocity, gravity, time) * 5f;
        m_flotability.SetInitialVelocity(entryVelocity);
    }

    void UpdateFloatingMovement()
    {
        Bounds arrowBounds = m_collider.m_renderer.bounds;
        Bounds waterBounds = m_flotability.m_water.GetComponent<SpriteRenderer>().bounds;

        transform.position += m_flotability.GetRoutePosition(arrowBounds, waterBounds, m_mass) * m_speed;
        transform.up = m_flotability.GetCurrentVelocity();
    }
}
