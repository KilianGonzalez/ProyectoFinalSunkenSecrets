using UnityEngine;

public class Palanca : MonoBehaviour
{
    public GameObject mensaje;
    public GameObject palancaNueva;
    public GameObject objetoAEliminar;
    public GameObject explosionPrefab; // Prefab de la explosi�n

    private bool jugadorCerca = false;

    void Start()
    {
        mensaje.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            CambiarPalanca();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mensaje.SetActive(true);
            jugadorCerca = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mensaje.SetActive(false);
            jugadorCerca = false;
        }
    }

    void CambiarPalanca()
    {
        // Guardamos la posici�n del objeto antes de eliminarlo
        Vector3 posicionObjeto = objetoAEliminar != null ? objetoAEliminar.transform.position : Vector3.zero;

        // Eliminar el objeto de la escena
        if (objetoAEliminar != null)
        {
            Destroy(objetoAEliminar);
        }

        // Instanciar la explosi�n en la misma posici�n
        if (explosionPrefab != null && objetoAEliminar != null)
        {
            Instantiate(explosionPrefab, posicionObjeto, Quaternion.identity);
        }

        // Cambiar la palanca despu�s de 1 segundo (opcional para que se vea mejor la explosi�n)
        Invoke(nameof(ActivarNuevaPalanca), 1f);
    }

    void ActivarNuevaPalanca()
    {
        Instantiate(palancaNueva, transform.position, transform.rotation);
        Destroy(gameObject); // Eliminar la palanca original
    }
}
