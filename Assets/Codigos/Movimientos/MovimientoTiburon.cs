using System.Collections;
using UnityEngine;

public class MovimientoTiburon : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad del tibur�n
    public float changeDirectionTime = 3f; // Tiempo antes de cambiar direcci�n
    public float movementRange = 5f; // Rango de movimiento desde la posici�n inicial

    private Vector2 targetPosition; // Pr�ximo destino aleatorio
    private SpriteRenderer spriteRenderer;
    private Vector2 initialPosition; // Posici�n donde fue colocado en el editor

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Guardar la posici�n inicial para definir el rango
        initialPosition = transform.position;

        // Establecer un primer destino aleatorio dentro del rango
        SetNewTargetPosition();

        // Iniciar la rutina para cambiar de direcci�n
        StartCoroutine(ChangeDirectionRoutine());
    }
    void Update()
    {
        MoveShark();
    }
    void MoveShark()
    {
        // Mueve el tibur�n hacia el target de forma suave
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Si llega al destino, elige otro punto
        if ((Vector2)transform.position == targetPosition)
        {
            SetNewTargetPosition();
        }

        // Voltear el sprite seg�n la direcci�n
        spriteRenderer.flipX = targetPosition.x < transform.position.x;
    }

    void SetNewTargetPosition()
    {
        // Generar una nueva posici�n aleatoria dentro del rango autom�tico
        float randomX = Random.Range(initialPosition.x - movementRange, initialPosition.x + movementRange);
        float randomY = Random.Range(initialPosition.y - movementRange, initialPosition.y + movementRange);
        targetPosition = new Vector2(randomX, randomY);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);
            SetNewTargetPosition();
        }
    }
}
