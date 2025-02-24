using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraOxigeno : MonoBehaviour
{
    public Image oxigenoFill; // La imagen de la barra de oxígeno
    public float oxigenoMaximo = 100f; // Oxígeno máximo
    private float oxigenoActual; // Oxígeno actual
    private float tiempoDescuento = 2f; // Tiempo en segundos para reducir el oxígeno
    private float tiempoRestante; // Tiempo restante para reducir el oxígeno

    void Start()
    {
        oxigenoActual = oxigenoMaximo; // Inicia el oxígeno actual en 100
        tiempoRestante = tiempoDescuento; // Inicia el tiempo restante para el descuento
    }

    void Update()
    {
        // Reducir el tiempo restante
        tiempoRestante -= Time.deltaTime;

        // Si el tiempo ha pasado, descontamos 1 unidad de oxígeno
        if (tiempoRestante <= 0)
        {
            oxigenoActual -= 1f; // Descontamos 1 unidad de oxígeno
            tiempoRestante = tiempoDescuento; // Reiniciamos el temporizador
        }

        // Aseguramos que el oxígeno actual no sea menor que 0
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        // Actualiza la barra visualmente
        oxigenoFill.fillAmount = Mathf.Clamp01(oxigenoActual / oxigenoMaximo);

        // Si el oxígeno llega a 0, cambia a la escena de "Derrota"
        if (oxigenoActual <= 0)
        {
            SceneManager.LoadScene("Derrota");
        }
    }
}
