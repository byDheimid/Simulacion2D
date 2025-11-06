using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;
    public float launchForce = 10f;

    List<GameObject> m_balls = new();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        if (ballPrefab == null) return;

        // Crear pelota
        m_balls.Where(c => c != null).ToList().ForEach(z => Destroy(z));
        m_balls.Clear();
        RadiacionSolar.Instance.target = null;
        RadiacionSolar.Instance.temperatura = 20;

        GameObject ballObj = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        RadiacionSolar.Instance.target = ballObj.transform;
        m_balls.Add(ballObj);

        // Calcular dirección hacia el mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 dir = (mousePos - transform.position).normalized;

        // Asignar velocidad inicial
        Ball ball = ballObj.GetComponent<Ball>();
        if (ball != null)
        {
            ball.Launch(dir * launchForce);
        }
    }
}
