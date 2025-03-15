using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FondoNegroEfecto : MonoBehaviour
{
    public Image fondoNegro;
    public float duracionOscurecimiento = 3f;

    void Start()
    {
        fondoNegro.color = new Color(0, 0, 0, 0); // Comienza transparente
    }

    public void IniciarOscurecimiento()
    {
        StartCoroutine(OscurecerFondo());
    }

    IEnumerator OscurecerFondo()
    {
        float tiempo = 0;
        while (tiempo < duracionOscurecimiento)
        {
            tiempo += Time.deltaTime;
            float alpha = tiempo / duracionOscurecimiento;
            fondoNegro.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
