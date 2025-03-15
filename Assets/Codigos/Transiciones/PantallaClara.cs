using UnityEngine;
using UnityEngine.UI;

public class PantallaClara : MonoBehaviour
{
    public Image fadeImage;  // La imagen negra que cubre la pantalla.
    public float fadeDuration = 2f;  // Tiempo de duración del fade.

    private float fadeTimer = 0f;  // Temporizador para controlar el tiempo transcurrido.
    private bool isFading = false;  // Para saber si el fade ha comenzado.

    void Start()
    {
        if (fadeImage == null)
        {
            Debug.LogError("No se ha asignado la imagen a la variable fadeImage");
            return;
        }

        // Inicializamos la imagen como completamente opaca.
        Color startColor = fadeImage.color;
        startColor.a = 1f;  // Opaco
        fadeImage.color = startColor;
        IniciarFade();
    }

    void Update()
    {
        if (isFading)
        {
            // Incrementamos el temporizador por el tiempo transcurrido en este frame.
            fadeTimer += Time.deltaTime;

            // Calculamos el valor del alpha (transparencia) con Lerp entre 1 (opaco) y 0 (transparente)
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);

            // Actualizamos el color de la imagen
            Color currentColor = fadeImage.color;
            currentColor.a = alpha;
            fadeImage.color = currentColor;

            // Si ya hemos pasado el tiempo de desvanecimiento, dejamos la imagen completamente transparente.
            if (fadeTimer >= fadeDuration)
            {
                isFading = false;  // Detenemos el fade.
            }
        }
    }

    // Método para iniciar el fade
    public void IniciarFade()
    {
        fadeTimer = 0f;  // Reseteamos el temporizador
        isFading = true;  // Iniciamos el fade
    }
}
