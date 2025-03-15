using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teletransporte : MonoBehaviour
{
    public string escenaVictoria = "Victoria"; // Nombre de la escena de victoria
    public GameObject fadePanel; // Aquí va "Fondo cofre"
    public float fadeDuration = 2f; // Duración de la transición

    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (fadePanel != null)
        {
            canvasGroup = fadePanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0; // Asegurar que empieza transparente
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si el jugador toca el cofre
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        if (canvasGroup == null) yield break;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1; // Asegurar que quede completamente negro

        SceneManager.LoadScene(escenaVictoria); // Cargar la nueva escena
    }
}
