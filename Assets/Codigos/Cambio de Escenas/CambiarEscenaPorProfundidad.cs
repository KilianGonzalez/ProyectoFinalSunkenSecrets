using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambiarEscenaPorProfundidad : MonoBehaviour
{
    public string nombreNuevaEscena = "Nivel1";
    private bool cambiandoEscena = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cambiandoEscena)
        {
            cambiandoEscena = true;
            StartCoroutine(TransicionYCambioDeEscena());
        }
    }

    IEnumerator TransicionYCambioDeEscena()
    {
        // Intentamos encontrar el script FondoNegroEfecto
        FondoNegroEfecto fondoNegro = FindObjectOfType<FondoNegroEfecto>();

        if (fondoNegro != null)
        {
            fondoNegro.IniciarOscurecimiento();
            yield return new WaitForSeconds(fondoNegro.duracionOscurecimiento); // Esperamos la duración del efecto
        }
        else
        {
            Debug.LogError("Error: No se encontró el objeto FondoNegroEfecto en la escena.");
            yield return new WaitForSeconds(3f); // Espera por defecto si no se encuentra
        }

        SceneManager.LoadScene(nombreNuevaEscena);
    }
}