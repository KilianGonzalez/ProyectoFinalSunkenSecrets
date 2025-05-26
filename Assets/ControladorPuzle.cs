using UnityEngine;

public class PuzzleNumero : MonoBehaviour
{
    public Sprite[] spritesNumeros;
    public GameObject mensajeInteractuar;
    public int valorCorrecto = 0;
    public SpriteRenderer imagenNumero;
    public float distanciaInteraccion = 2f;
    public Transform jugador;
    public GameObject objetoADestruir;

    private int valorActual = 0;
    private static PuzzleNumero[] todasLasRuedas;

    void Start()
    {
        if (spritesNumeros.Length != 10)
            Debug.LogError("¡Faltan sprites de número!");

        imagenNumero.sprite = spritesNumeros[valorActual];

        if (todasLasRuedas == null)
            todasLasRuedas = FindObjectsOfType<PuzzleNumero>();

        mensajeInteractuar.SetActive(false);
    }

    void Update()
    {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Mostrar mensaje solo si esta es la más cercana
        if (EsLaRuedaMasCercana() && distancia <= distanciaInteraccion)
        {
            mensajeInteractuar.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                valorActual = (valorActual + 1) % 10;
                imagenNumero.sprite = spritesNumeros[valorActual];
                ComprobarPuzzle();
            }
        }
        else
        {
            mensajeInteractuar.SetActive(false);
        }
    }

    bool EsLaRuedaMasCercana()
    {
        PuzzleNumero masCercana = null;
        float menorDistancia = Mathf.Infinity;

        foreach (var rueda in todasLasRuedas)
        {
            float dist = Vector2.Distance(rueda.transform.position, jugador.position);
            if (dist < menorDistancia)
            {
                menorDistancia = dist;
                masCercana = rueda;
            }
        }

        return masCercana == this;
    }

    void ComprobarPuzzle()
    {
        foreach (var rueda in todasLasRuedas)
        {
            if (rueda.valorActual != rueda.valorCorrecto)
                return;
        }

        if (objetoADestruir != null)
            Destroy(objetoADestruir);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mensajeInteractuar.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mensajeInteractuar.SetActive(false);
        }
    }
}
