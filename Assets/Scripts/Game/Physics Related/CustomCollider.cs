using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public SpriteRenderer m_renderer;
    public bool m_isCircle;

    public Bounds GetBounds()
    {
        return m_renderer.bounds;
    }

    public bool OnHoldCollision(SpriteRenderer renderer)
    {
        //! HAY PROBLEMAS SI EL OBJETO ROTA, NO LO TOMAREMOS EN CUENTA PORQUE NO SE ENSEÑÓ EN CLASES, 
        //! Y COPIAR Y PEGAR NO ES LO NUESTRO.
        //! LA FORMULA O LO QUE PROPONE LA IA ES DESCARADO Y EXTENSO, NO ES NECESARIO.

        //>> PARA EVITAR EL PROBLEMA, SE CAMBIARÁ LA FLECHA RECTANGULAR POR UN SIMPLE CIRCULO.

        if (!m_isCircle)
            return m_renderer.bounds.Intersects(renderer.bounds);

        SpriteRenderer circleRenderer = m_isCircle ? m_renderer : renderer;
        SpriteRenderer rectRenderer   = m_isCircle ? renderer : m_renderer;

        Vector3 circleCenter = circleRenderer.bounds.center;
        float radius = circleRenderer.bounds.extents.x; 

        Vector3 closestPoint = new Vector3(
        Mathf.Clamp(circleCenter.x, rectRenderer.bounds.min.x, rectRenderer.bounds.max.x),
        Mathf.Clamp(circleCenter.y, rectRenderer.bounds.min.y, rectRenderer.bounds.max.y),
        circleCenter.z
    );

    // Colisiona si la distancia al cuadrado <= radio^2
    float distanceSqr = (circleCenter - closestPoint).sqrMagnitude;
    return distanceSqr <= radius * radius;
    }

    void OnEnable() => CollisionManager.m_colliders.Add(this);
    void OnDisable() => CollisionManager.m_colliders.Remove(this);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (!m_isCircle)
            Gizmos.DrawWireCube(m_renderer.bounds.center, m_renderer.bounds.size);
        else
            Gizmos.DrawWireSphere(m_renderer.bounds.center, m_renderer.bounds.extents.x);
    }
}
