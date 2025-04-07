using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonDesbloqueo : MonoBehaviour
{
    public GameObject mensajeE;
    public GameObject paredADestruir;
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (paredADestruir != null)
            {
                Destroy(paredADestruir);
                mensajeE.SetActive(false); 
            }
        }
    }

    private void Start()
    {
        if (mensajeE != null)
        {
            mensajeE.SetActive(false); // Desactivado al inicio
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            if (mensajeE != null)
            {
                mensajeE.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (mensajeE != null)
            {
                mensajeE.SetActive(false);
            }
        }
    }
}
