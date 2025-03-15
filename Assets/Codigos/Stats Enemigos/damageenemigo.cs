using UnityEngine;

public class DamageEnemigo : MonoBehaviour
{
    public float damageAmount = 10f; // Daño que hace este enemigo

    private bool hasDealtDamage = false; // Para evitar daño continuo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDealtDamage)
        {
            HealthSystem playerHealth = collision.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                hasDealtDamage = true; // Marca que ya hizo daño
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasDealtDamage = false; // Permitir hacer daño de nuevo si el jugador se aleja
        }
    }
}