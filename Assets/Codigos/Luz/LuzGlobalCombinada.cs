using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzGlobalCombinada : MonoBehaviour
{
    public Transform jugador;

    // Parámetros para la zona oscura horizontal
    public float zonaCentroX = 0f;
    public float anchoZona = 20f;
    public float intensidadZonaOscura = 0.15f;
    public float intensidadZonaNormal = 0.8f;

    // Parámetros para la profundidad (vertical)
    public float superficieY = 40f;
    public float profundidadMaxima = -50f;
    public float intensidadMaxima = 1f;
    public float intensidadMinima = 0.2f;

    public float suavizado = 2f;

    private Light2D luzGlobal;

    void Start()
    {
        luzGlobal = GetComponent<Light2D>();
        if (luzGlobal == null)
        {
            Debug.LogError("Falta el componente Light2D en este GameObject.");
        }
    }

    void Update()
    {
        if (jugador == null || luzGlobal == null) return;

        // ---- Intensidad horizontal ----
        float posX = jugador.position.x;
        float mitadAncho = anchoZona / 2f;
        float entradaX = zonaCentroX - mitadAncho;
        float salidaX = zonaCentroX + mitadAncho;
        float intensidadX = (posX >= entradaX && posX <= salidaX)
            ? intensidadZonaOscura
            : intensidadZonaNormal;

        // ---- Intensidad vertical ----
        float distancia = Mathf.Clamp(jugador.position.y - superficieY, profundidadMaxima, 0f);
        float tVertical = Mathf.InverseLerp(profundidadMaxima, 0f, distancia);
        float intensidadY = Mathf.Lerp(intensidadMinima, intensidadMaxima, tVertical);

        // ---- Combinación final ----
        float intensidadObjetivo = Mathf.Min(intensidadX, intensidadY); // o usa otro método de combinación si prefieres
        luzGlobal.intensity = Mathf.Lerp(luzGlobal.intensity, intensidadObjetivo, Time.deltaTime * suavizado);
    }

    void OnDrawGizmosSelected()
    {
        float mitadAncho = anchoZona / 2f;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(zonaCentroX - mitadAncho, -100, 0), new Vector3(zonaCentroX - mitadAncho, 100, 0));
        Gizmos.DrawLine(new Vector3(zonaCentroX + mitadAncho, -100, 0), new Vector3(zonaCentroX + mitadAncho, 100, 0));
    }
}

