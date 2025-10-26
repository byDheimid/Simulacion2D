using UnityEngine;

public class Flotability : MonoBehaviour
{
    public float mass, volume, initialSpeed, gravityMagnitude, dampingInWater, dampingInAir;

    public Water m_water;

    [HideInInspector] public Vector2 m_velocity;

    public void SetInitialVelocity(Vector2 velocity)
    {
        m_velocity = velocity;
    }

    public Vector3 GetCurrentVelocity()
    {
        return m_velocity.normalized;
    }

    public Vector3 GetRoutePosition(Bounds objectBounds, Bounds waterBounds, float _mass)
    {
        if (m_water == null) return Vector2.zero;

        float submergedFraction = 0f;

        if (objectBounds.Intersects(waterBounds))
        {
            float overlapBottom = Mathf.Max(objectBounds.min.y, waterBounds.min.y);
            float overlapTop = Mathf.Min(objectBounds.max.y, waterBounds.max.y);
            float overlapHeight = Mathf.Max(0f, overlapTop - overlapBottom);

            float fracVertical = Mathf.Clamp01(overlapHeight / objectBounds.size.y);
            submergedFraction = fracVertical;
        }

        Vector2 buoyantForce = m_water.density * volume * submergedFraction * gravityMagnitude * Vector2.up;
        Vector2 gravityForce = Vector2.down * gravityMagnitude * (_mass != 0 ? _mass : mass);

        Vector2 netForce = buoyantForce + gravityForce;
        m_velocity += netForce / mass * Time.deltaTime;

        m_velocity *= (1f - dampingInWater * Time.deltaTime);

        return (Vector3)(m_velocity * Time.deltaTime);
    }
}
