using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float moveForce = 5f;   // Fuerza de movimiento
    public float maxSpeed = 3f;    // Velocidad máxima en el agua
    public float waterDrag = 1.5f; // Resistencia del agua

    private Rigidbody2D rb;
    private SpriteRenderer spriterender;
    private PolygonCollider2D polyCollider;
    private bool facingRight = true; // Para rastrear la dirección actual

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = waterDrag;
        spriterender = GetComponent<SpriteRenderer>();
        polyCollider = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        MoveCharacter();
        VoltearSpriteYCollider();
    }

    void MoveCharacter()
    {
        float moveX = Input.GetAxis("Horizontal"); // Movimiento en X (A/D o Flechas)
        float moveY = Input.GetAxis("Vertical");   // Movimiento en Y (W/S o Flechas)

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Aplicar fuerza solo si la velocidad es menor a la máxima
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection * moveForce, ForceMode2D.Force);
        }
    }

    void VoltearSpriteYCollider()
    {
        if (rb.velocity.x > 0.1f && !facingRight)
        {
            spriterender.flipX = false;
            FlipCollider();
            facingRight = true;
        }
        else if (rb.velocity.x < -0.1f && facingRight)
        {
            spriterender.flipX = true;
            FlipCollider();
            facingRight = false;
        }
    }

    void FlipCollider()
    {
        // Obtener los puntos originales del colisionador
        Vector2[] points = polyCollider.points;

        // Reflejar los puntos en el eje X
        for (int i = 0; i < points.Length; i++)
        {
            points[i].x *= -1;
        }

        // Asignar los nuevos puntos al colisionador
        polyCollider.points = points;
    }
}
