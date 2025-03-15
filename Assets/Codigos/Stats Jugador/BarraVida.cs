using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public RectTransform VidaFill; // Referencia a la barra de vida
    public float maxHealth = 100f; // Vida máxima del jugador
    private float currentHealth; // Vida actual
    private float initialHeight; // Altura inicial de la barra

    void Start()
    {
        currentHealth = maxHealth;
        initialHeight = VidaFill.sizeDelta.y;

        // Asegurar que el pivot está en la parte superior
        VidaFill.pivot = new Vector2(0.5f, 1f);

        UpdateHealthBar(); // Actualizar la barra al inicio
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        // Si la vida llega a 0, cambiar de escena
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Derrota");
        }
    }

    private void UpdateHealthBar()
    {
        // Calculamos la nueva altura
        float newHeight = (currentHealth / maxHealth) * initialHeight;

        // Aplicamos la nueva altura sin modificar la posición
        VidaFill.sizeDelta = new Vector2(VidaFill.sizeDelta.x, newHeight);
    }
}
