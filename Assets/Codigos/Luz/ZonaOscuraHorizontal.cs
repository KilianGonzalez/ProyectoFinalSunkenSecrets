using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ZonaOscuraHorizontal : MonoBehaviour
{
    public Transform jugador; // Asigna el jugador
    public float zonaCentroX = 0f; // Centro de la zona oscura
    public float anchoZona = 20f;  // Ancho total de la zona
    public float intensidadZonaOscura = 0.15f;
    public float intensidadZonaNormal = 0.8f;
    public float suavizado = 2f; // Transición suave

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

        float posX = jugador.position.x;
        float mitadAncho = anchoZona / 2f;
        float entradaX = zonaCentroX - mitadAncho;
        float salidaX = zonaCentroX + mitadAncho;


        float intensidadObjetivo = (posX >= entradaX && posX <= salidaX)
            ? intensidadZonaOscura
            : intensidadZonaNormal;

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
