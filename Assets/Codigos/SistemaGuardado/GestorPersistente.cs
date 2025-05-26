using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GestorPersistente : MonoBehaviour
{
    public static GestorPersistente instancia;

    private string escenaActual;
    private string escenaAnterior;

    private static readonly HashSet<string> escenasValidas = new()
    {
        "Prenivel", "Nivel1", "Nivel2"
    };

    private void Awake()
    {
        // Singleton
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        escenaAnterior = escenaActual;
        escenaActual = escena.name;
    }

    // Devuelve el nombre de la escena anterior.
    public static string ObtenerEscenaAnterior()
    {
        return instancia.escenaAnterior;
    }

    // Devuelve el nombre de la escena actual.
    public static string ObtenerEscenaActual()
    {
        return instancia.escenaActual;
    }

    // Devuelve true si la escena es válida para guardado (Prenivel, Nivel1, Nivel2).
    public static bool EsEscenaValida(string escena)
    {
        return escenasValidas.Contains(escena);
    }
}
