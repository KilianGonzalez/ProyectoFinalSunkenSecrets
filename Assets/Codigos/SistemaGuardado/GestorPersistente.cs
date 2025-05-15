using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorPersistente : MonoBehaviour
{
    private static GestorPersistente instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        GuardarEscenaActual();
    }

    private void OnDestroy()
    {
        GuardarEscenaActual();
    }

    private void GuardarEscenaActual()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        foreach (string ranura in GestorGuardado.ObtenerRanuras())
        {
            if (GestorGuardado.ExistePartida(ranura))
            {
                DatosPartida datos = GestorGuardado.CargarPartida(ranura);
                if (datos.escenaGuardada != escenaActual)
                {
                    datos.escenaGuardada = escenaActual;
                    GestorGuardado.GuardarPartida(datos, ranura);
                }
            }
        }

        Debug.Log("Guardado automático de escena: " + escenaActual);
    }
}