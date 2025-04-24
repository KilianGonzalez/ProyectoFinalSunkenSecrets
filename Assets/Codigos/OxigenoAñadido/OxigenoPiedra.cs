using UnityEngine;

public class OxigenoPiedra : MonoBehaviour
{
    public float velocidadRecarga = 5f; // Oxígeno por segundo
    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && BarraOxigeno.Instance != null)
        {
            BarraOxigeno.Instance.RecargarOxigeno(velocidadRecarga * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
        }
    }
}
