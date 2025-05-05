using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LinternaJugador : MonoBehaviour
{
    public Light2D linterna;        // Arrastra aquí tu Light2D en el Inspector
    public KeyCode teclaActivar = KeyCode.L;  // Tecla para encender/apagar

    private bool encendida = false;

    void Start()
    {
        if (linterna != null)
        {
            linterna.enabled = false; // Apagada al iniciar
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaActivar) && linterna != null)
        {
            encendida = !encendida;
            linterna.enabled = encendida;
        }
    }
}

