using UnityEngine;
using UnityEngine.UI; // Si usas Text normal
// using TMPro; // Si usas TextMeshPro

public class MostrarMensaje : MonoBehaviour
{
    [SerializeField] private GameObject mensajeUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mensajeUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mensajeUI.SetActive(false);
        }
    }
}
