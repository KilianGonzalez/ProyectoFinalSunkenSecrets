using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BarraOxigeno : MonoBehaviour
{
    public RectTransform oxigenoRectTransform; // Referencia a la barra de ox�geno
    public Image oscurecimientoImagen; // Imagen para el efecto de oscurecimiento
    public float oxigenoMaximo = 100f; // Cantidad m�xima de ox�geno
    private float oxigenoActual; // Ox�geno actual
    private float alturaInicial; // Altura de la barra
    public float velocidadConsumo = 0.5f; // Velocidad de consumo del ox�geno
    private bool oscureciendo = false; // Controla si ya se activ� la oscuridad

    void Start()
    {
        oxigenoActual = oxigenoMaximo;
        alturaInicial = oxigenoRectTransform.sizeDelta.y;

        // Asegurar que el pivot est� en la parte superior
        oxigenoRectTransform.pivot = new Vector2(0.5f, 1f);

        // Verificar que la imagen de oscurecimiento est� asignada
        if (oscurecimientoImagen != null)
        {
            oscurecimientoImagen.color = new Color(0, 0, 0, 0); // Transparente al inicio
        }
    }

    void Update()
    {
        // Reducimos ox�geno de manera fluida
        oxigenoActual -= velocidadConsumo * Time.deltaTime;
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        // Calculamos la nueva altura
        float nuevaAltura = (oxigenoActual / oxigenoMaximo) * alturaInicial;

        // Aplicamos la nueva altura sin modificar la posici�n
        oxigenoRectTransform.sizeDelta = new Vector2(oxigenoRectTransform.sizeDelta.x, nuevaAltura);

        // Activar oscurecimiento cuando queden 3 segundos de ox�geno
        if (oxigenoActual <= 3f && !oscureciendo)
        {
            oscureciendo = true;
            if (oscurecimientoImagen != null)
            {
                StartCoroutine(OscurecerPantalla(3f)); // Ahora dura 3 segundos
            }
            else
            {
                Debug.LogWarning("OscurecimientoImagen no est� asignado en el Inspector.");
            }
        }

        // Si el ox�geno llega a 0, cambiar de escena dependiendo del nivel
        if (oxigenoActual <= 0)
        {
            CambiarEscenaDerrota();
        }
    }

    private void CambiarEscenaDerrota()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        if (escenaActual == "Nivel1")
        {
            SceneManager.LoadScene("Derrota");
        }
        else if (escenaActual == "Nivel2")
        {
            SceneManager.LoadScene("Derrota2");
        }
        
    }

    IEnumerator OscurecerPantalla(float duracion)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion; // Va de 0 a 1

            // Ajusta el alfa para hacer la pantalla m�s oscura progresivamente
            oscurecimientoImagen.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, progreso));

            yield return null;
        }

        // Aseguramos que el alpha quede en 1 al final
        oscurecimientoImagen.color = new Color(0, 0, 0, 1);
    }
}
