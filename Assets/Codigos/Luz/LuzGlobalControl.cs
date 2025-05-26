using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzGlobalControl : MonoBehaviour
{
    public Transform jugador; // Asigna el jugador en el Inspector
    public float superficieY = 40f; // Altura del agua/superficie
    public float profundidadMaxima = -50f; // Hasta d�nde puede bajar
    public float intensidadMaxima = 1f; // Luz m�s intensa en la superficie
    public float intensidadMinima = 0.2f; // Oscuridad m�xima al fondo

    private Light2D luzGlobal;

    void Start()
    {
        luzGlobal = GetComponent<Light2D>();

        if (luzGlobal == null)
        {
            Debug.LogError("Este objeto no tiene una Light2D asignada.");
        }
    }

    void Update()
    {
        if (jugador == null || luzGlobal == null) return;

        // Distancia vertical desde la superficie
        float distancia = Mathf.Clamp(jugador.position.y - superficieY, profundidadMaxima, 0f);

        // Normalizar de profundidadMaxima (m�s abajo) a 0 (superficie)
        float t = Mathf.InverseLerp(profundidadMaxima, 0f, distancia);

        // Interpolar la intensidad entre m�nima y m�xima
        luzGlobal.intensity = Mathf.Lerp(intensidadMinima, intensidadMaxima, t);
    }
}
