using System.Collections.Generic;
using UnityEngine;

public class Parable : MonoBehaviour
{
    public float force = 10f;
    public float verticalBoost = 5f;
    public Vector2 gravity = new Vector2(0, -9.81f);

    public Color gizmoColor = Color.yellow;
    public int resolution = 30;
    public float previewTime = 2f; 

    [HideInInspector] public Vector2 startPos;
    [HideInInspector] public Vector2 velocity;

    void Update() => velocity = (Vector2.right * force) + (Vector2.up * verticalBoost);

    public Vector2 GetRoutePosition(Vector2 _startPos, Vector2 _velocity, Vector2 _gravity, float _time)
    {
        Vector2 position = _startPos + _velocity * _time + 0.5f * _gravity * Mathf.Pow(_time, 2);
        return position;
    }
    
    public Vector2 GetCurrentVelocity(Vector2 _velocity, Vector2 _gravity, float _time)
    {
        Vector2 currentVelocity = _velocity + _gravity * _time;
        return currentVelocity.normalized;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector2 direction = Vector2.right;
        Vector2 initialVelocity = (direction * force) + (Vector2.up * verticalBoost);
        Vector2 start = transform.position;

        Vector2 prevPoint = start;
        for (int i = 1; i <= resolution; i++)
        {
            float t = previewTime / resolution * i;
            Vector2 point = start + initialVelocity * t + 0.5f * gravity * t * t;

            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(prevPoint, 0.1f);
    }
}
