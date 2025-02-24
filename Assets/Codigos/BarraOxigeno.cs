using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraOxigeno : MonoBehaviour
{
    public Image oxigenoFill; // La imagen de la barra de ox�geno
    public float oxigenoMaximo = 100f; // Ox�geno m�ximo
    private float oxigenoActual; // Ox�geno actual
    private float tiempoDescuento = 2f; // Tiempo en segundos para reducir el ox�geno
    private float tiempoRestante; // Tiempo restante para reducir el ox�geno

    void Start()
    {
        oxigenoActual = oxigenoMaximo; // Inicia el ox�geno actual en 100
        tiempoRestante = tiempoDescuento; // Inicia el tiempo restante para el descuento
    }

    void Update()
    {
        // Reducir el tiempo restante
        tiempoRestante -= Time.deltaTime;

        // Si el tiempo ha pasado, descontamos 1 unidad de ox�geno
        if (tiempoRestante <= 0)
        {
            oxigenoActual -= 1f; // Descontamos 1 unidad de ox�geno
            tiempoRestante = tiempoDescuento; // Reiniciamos el temporizador
        }

        // Aseguramos que el ox�geno actual no sea menor que 0
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        // Actualiza la barra visualmente
        oxigenoFill.fillAmount = Mathf.Clamp01(oxigenoActual / oxigenoMaximo);

        // Si el ox�geno llega a 0, cambia a la escena de "Derrota"
        if (oxigenoActual <= 0)
        {
            SceneManager.LoadScene("Derrota");
        }
    }
}
