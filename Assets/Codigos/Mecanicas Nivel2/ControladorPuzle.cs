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
    private PuzzleNumero[] todasLasRuedas;

    void Start()        
    {
        if (spritesNumeros.Length != 10)
            Debug.LogError("¡Faltan sprites de número!");

        imagenNumero.sprite = spritesNumeros[valorActual];

        
        todasLasRuedas = FindObjectsOfType<PuzzleNumero>();

        if (jugador == null)
            jugador = GameObject.FindGameObjectWithTag("Player")?.transform;

        mensajeInteractuar.SetActive(false);
    }

    void Update()
    {
        if (jugador == null)
        {
            GameObject nuevoJugador = GameObject.FindGameObjectWithTag("Player");
            if (nuevoJugador != null)
                jugador = nuevoJugador.transform;
            else
                return;
        }


        float distancia = Vector2.Distance(transform.position, jugador.position);

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
        if (jugador == null || todasLasRuedas == null)
            return false;

        PuzzleNumero masCercana = null;
        float menorDistancia = Mathf.Infinity;

        foreach (var rueda in todasLasRuedas)
        {
            if (rueda == null) continue; // <-- Esto evita el error

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

            if (rueda == null) continue;

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
