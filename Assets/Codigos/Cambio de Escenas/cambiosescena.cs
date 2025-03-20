using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiosescena : MonoBehaviour
{
    public void CambiarTutorial() 
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void CambiarOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void CambiarJugar()
    {
        SceneManager.LoadScene("Jugar");
    }

    public void CambiarEnemigos()
    {
        SceneManager.LoadScene("Enemigos");
    }

    public void CambiarAMenu()
    {
        SceneManager.LoadScene("Menú");
    }

    public void Salir() 
    {
        Application.Quit();
    }

    public void CambiarANivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    public void CambiarANivel1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Presalida()
    {
        SceneManager.LoadScene("Presalida");
    }

}
