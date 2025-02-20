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
}
