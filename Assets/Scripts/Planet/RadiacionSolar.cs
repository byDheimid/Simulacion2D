using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RadiacionSolar : MonoBehaviour
{
    public Transform player;             // Referencia al player
    public float intensidadSolar = 100f; // Fuerza de la radiación
    public float temperatura = 20f;      // Temperatura inicial (ej: temperatura normal)
    public float factorAbsorcion = 1f;   // Qué tanto absorbe el objeto
    public float tasaEnfriamiento = 5f;  // Qué tan rápido se enfría lejos del sol
    public float radioInfluencia = 10f;  // Radio máximo de influencia de la radiación solar
    public float temperaturaMin = -50f;  // L�mite inferior
    public float temperaturaMax = 500f;  // L�mite superior
    public TextMeshProUGUI texto;
    public Image image;
    public SceneManager sceneManager;

    private void Start()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.2f); // Amarillo semitransparente
        Gizmos.DrawWireSphere(transform.position, radioInfluencia);
    }
    void Update()
    {
        texto.text = temperatura.ToString("F1") + "�";

        image.fillAmount = temperatura / 50;

        if (temperatura > 35)
        {
            image.color = Color.red;
        }
        if (temperatura >= 50)
        {
            sceneManager.LoadScene("SC_Game2");
        }

        float distancia = Vector3.Distance(player.position, transform.position);

        // Verifica si estamos dentro del radio de influencia
        if (distancia <= radioInfluencia)
        {
            // Radiación solar según distancia (ley del cuadrado inverso)
            float energiaRecibida = intensidadSolar / (distancia * distancia);
            
            // Factor de atenuación basado en la distancia (1 en el centro, 0 en el borde)
            float factorDistancia = 1f - (distancia / radioInfluencia);
            
            // Calentamiento con atenuación por distancia
            temperatura += energiaRecibida * factorAbsorcion * factorDistancia * Time.deltaTime;
        }
        else
        {
            // Enfriamiento natural fuera del radio de influencia
            temperatura -= tasaEnfriamiento * Time.deltaTime;
        }

        // Limitar la temperatura a un rango
        temperatura = Mathf.Clamp(temperatura, temperaturaMin, temperaturaMax);
    }
}