using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RadiacionSolar : MonoBehaviour
{
    public Transform sol;                // Referencia al Sol
    public float intensidadSolar = 100f; // Fuerza de la radiación
    public float temperatura = 20f;      // Temperatura inicial (ej: temperatura normal)
    public float factorAbsorcion = 1f;   // Qué tanto absorbe el objeto
    public float tasaEnfriamiento = 5f;  // Qué tan rápido se enfría lejos del sol
    public float temperaturaMin = -50f;  // Límite inferior
    public float temperaturaMax = 500f;  // Límite superior
    public TextMeshProUGUI texto;
    public Image image;

    private void Start()
    {
        
    }
    void Update()
    {
        texto.text = temperatura.ToString("F1") + "°";

        image.fillAmount = temperatura / 50;

        if (temperatura > 35)
        {
            image.color = Color.red;
        }

        float distancia = Vector3.Distance(transform.position, sol.position);

        // Radiación solar según distancia
        float energiaRecibida = intensidadSolar / (distancia * distancia);

        if (energiaRecibida > 0.1f) // Si la energía recibida es significativa
        {
            // Calentamiento
            temperatura += energiaRecibida * factorAbsorcion * Time.deltaTime;
        }
        else
        {
            // Enfriamiento natural
            temperatura -= tasaEnfriamiento * Time.deltaTime;
        }

        // Limitar la temperatura a un rango
        temperatura = Mathf.Clamp(temperatura, temperaturaMin, temperaturaMax);

        Debug.Log("Temperatura actual: " + temperatura);
    }
}
