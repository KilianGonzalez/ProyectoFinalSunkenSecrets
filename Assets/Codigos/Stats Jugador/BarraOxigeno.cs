using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BarraOxigeno : MonoBehaviour
{
    public static BarraOxigeno Instance; // Singleton

    public RectTransform oxigenoRectTransform;
    public Image oscurecimientoImagen;
    public float oxigenoMaximo = 100f;
    private float oxigenoActual;
    private float alturaInicial;
    public float velocidadConsumo = 0.5f;
    private bool oscureciendo = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        oxigenoActual = oxigenoMaximo;
        alturaInicial = oxigenoRectTransform.sizeDelta.y;
        oxigenoRectTransform.pivot = new Vector2(0.5f, 1f);

        if (oscurecimientoImagen != null)
        {
            oscurecimientoImagen.color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {
        oxigenoActual -= velocidadConsumo * Time.deltaTime;
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        float nuevaAltura = (oxigenoActual / oxigenoMaximo) * alturaInicial;
        oxigenoRectTransform.sizeDelta = new Vector2(oxigenoRectTransform.sizeDelta.x, nuevaAltura);

        if (oxigenoActual <= 3f && !oscureciendo)
        {
            oscureciendo = true;
            if (oscurecimientoImagen != null)
            {
                StartCoroutine(OscurecerPantalla(3f));
            }
            else
            {
                Debug.LogWarning("OscurecimientoImagen no está asignado en el Inspector.");
            }
        }

        if (oxigenoActual <= 0)
        {
            CambiarEscenaDerrota();
        }
    }

    private void CambiarEscenaDerrota()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        if (escenaActual == "Nivel1")
        {
            SceneManager.LoadScene("Derrota");
        }
        else if (escenaActual == "Nivel2")
        {
            SceneManager.LoadScene("Derrota2");
        }
    }

    IEnumerator OscurecerPantalla(float duracion)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;
            oscurecimientoImagen.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, progreso));
            yield return null;
        }

        oscurecimientoImagen.color = new Color(0, 0, 0, 1);
    }

    public void RecargarOxigeno(float cantidad)
    {
        oxigenoActual += cantidad;
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        float nuevaAltura = (oxigenoActual / oxigenoMaximo) * alturaInicial;
        oxigenoRectTransform.sizeDelta = new Vector2(oxigenoRectTransform.sizeDelta.x, nuevaAltura);
    }
}
