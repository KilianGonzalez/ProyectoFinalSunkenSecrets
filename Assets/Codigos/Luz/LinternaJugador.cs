using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class LinternaJugador : MonoBehaviour
{
    public Light2D linterna;
    public KeyCode teclaActivar = KeyCode.L;

    public Image bateria100;
    public Image bateria66;
    public Image bateria33;
    public Image bateria0;

    private int nivelBateria = 3;
    private float tiempoParaBajar = 60f;
    private float tiempoAcumulado = 0f;
    private bool linternaEncendida = false;

    void Start()
    {
        if (linterna != null)
            linterna.enabled = false;

        if (bateria100 == null)
        {
            GameObject go = GameObject.Find("Bateria100");
            if (go != null)
                bateria100 = go.GetComponent<Image>();
        }
        if (bateria66 == null)
        {
            GameObject go = GameObject.Find("Bateria66");
            if (go != null)
                bateria66 = go.GetComponent<Image>();
        }
        if (bateria33 == null)
        {
            GameObject go = GameObject.Find("Bateria33");
            if (go != null)
                bateria33 = go.GetComponent<Image>();
        }
        if (bateria0 == null)
        {
            GameObject go = GameObject.Find("Bateria0");
            if (go != null)
                bateria0 = go.GetComponent<Image>();
        }

        ActualizarUIBateria();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaActivar) && nivelBateria > 0)
        {
            linternaEncendida = !linternaEncendida;
            if (linterna != null)
                linterna.enabled = linternaEncendida;
        }

        if (linternaEncendida)
        {
            tiempoAcumulado += Time.deltaTime;
            if (tiempoAcumulado >= tiempoParaBajar)
            {
                tiempoAcumulado = 0f;
                nivelBateria--;
                if (nivelBateria <= 0)
                {
                    nivelBateria = 0;
                    linternaEncendida = false;
                    if (linterna != null)
                        linterna.enabled = false;
                }
                ActualizarUIBateria();
            }
        }
    }

    void ActualizarUIBateria()
    {
        if (bateria100 != null) bateria100.gameObject.SetActive(nivelBateria == 3);
        if (bateria66 != null) bateria66.gameObject.SetActive(nivelBateria == 2);
        if (bateria33 != null) bateria33.gameObject.SetActive(nivelBateria == 1);
        if (bateria0 != null) bateria0.gameObject.SetActive(nivelBateria == 0);
    }
}
