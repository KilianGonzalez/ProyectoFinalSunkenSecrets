using UnityEngine;

public class DamageEnemigo : MonoBehaviour
{
    public float damageAmount = 10f; // Da�o que hace este enemigo

    private bool hasDealtDamage = false; // Para evitar da�o continuo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDealtDamage)
        {
            HealthSystem playerHealth = collision.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                hasDealtDamage = true; // Marca que ya hizo da�o
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasDealtDamage = false; // Permitir hacer da�o de nuevo si el jugador se aleja
        }
    }
}