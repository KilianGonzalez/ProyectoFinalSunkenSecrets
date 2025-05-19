using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GestorPersistente : MonoBehaviour
{
    private static GestorPersistente instancia;
 

    private readonly HashSet<string> escenasValidas = new HashSet<string>
    {
        "Prenivel",
        "Nivel1",
        "Nivel2"
    };

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
        GuardarEscenaActualSiEsValida();
    }

    private void OnDestroy()
    {
        GuardarEscenaActualSiEsValida();
    }

    private void GuardarEscenaActualSiEsValida()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        if (!escenasValidas.Contains(escenaActual)) return;

        string ranuraActiva = GestorGuardado.ObtenerRanuraActiva();
        if (string.IsNullOrEmpty(ranuraActiva)) return;

        if (GestorGuardado.ExistePartida(ranuraActiva))
        {
            DatosPartida datos = GestorGuardado.CargarPartida(ranuraActiva);
            if (datos.escenaGuardada != escenaActual)
            {
                datos.escenaGuardada = escenaActual;
                GestorGuardado.GuardarPartida(datos, ranuraActiva);
                Debug.Log("Guardada escena en ranura activa: " + ranuraActiva);
            }
        }
    }
}
