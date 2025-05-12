using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarFondoNegro2D : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float distanciaActivacion = 5f; // Distancia a la que empieza a aparecer el fondo
    private SpriteRenderer fondoRenderer; // Referencia al SpriteRenderer del fondo negro

    void Start()
    {
        // Obtener el componente SpriteRenderer del fondo
        fondoRenderer = GetComponent<SpriteRenderer>();

        // Asegurarse de que el fondo empieza completamente invisible
        fondoRenderer.color = new Color(0f, 0f, 0f, 0f);
    }

    void Update()
    {
        // Calcular la distancia entre el jugador y el fondo
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador está dentro del rango de activación
        if (distancia <= distanciaActivacion)
        {
            // Hacer que el fondo negro se haga visible progresivamente
            float alpha = Mathf.Lerp(0f, 1f, 1 - (distancia / distanciaActivacion));
            fondoRenderer.color = new Color(0f, 0f, 0f, alpha); // Cambiar opacidad del fondo
        }
        else
        {
            // Si el jugador está fuera del rango, el fondo vuelve a ser invisible
            fondoRenderer.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}

