using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public RectTransform VidaFill; // Referencia a la barra de vida
    public float maxHealth = 100f; // Vida m�xima del jugador
    private float currentHealth; // Vida actual
    private float initialHeight; // Altura inicial de la barra

    void Start()
    {
        currentHealth = maxHealth;
        initialHeight = VidaFill.sizeDelta.y;

        // Asegurar que el pivot est� en la parte superior
        VidaFill.pivot = new Vector2(0.5f, 1f);

        UpdateHealthBar(); // Actualizar la barra al inicio
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
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

    private void UpdateHealthBar()
    {
        // Calculamos la nueva altura
        float newHeight = (currentHealth / maxHealth) * initialHeight;

        // Aplicamos la nueva altura sin modificar la posici�n
        VidaFill.sizeDelta = new Vector2(VidaFill.sizeDelta.x, newHeight);
    }
}
