using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float moveForce = 1f;   // Fuerza de movimiento
    public float maxSpeed = 1f;    // Velocidad m�xima en el agua
    public float waterDrag = 1f;   // Resistencia del agua

    private Rigidbody2D rb;
    private SpriteRenderer spriterender;
    private bool facingRight = true; // Para rastrear la direcci�n actual

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = waterDrag;
        spriterender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveCharacter();
        VoltearSprite();
    }

    void MoveCharacter()
    {
        float moveX = Input.GetAxis("Horizontal"); // Movimiento en X (A/D o Flechas)
        float moveY = Input.GetAxis("Vertical");   // Movimiento en Y (W/S o Flechas)

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Aplicar fuerza solo si la velocidad es menor a la m�xima
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection * moveForce, ForceMode2D.Force);
        }
    }

    void VoltearSprite()
    {
        if (rb.velocity.x > 0.1f && !facingRight)
        {
            spriterender.flipX = false;
            facingRight = true;
        }
        else if (rb.velocity.x < -0.1f && facingRight)
        {
            spriterender.flipX = true;
            facingRight = false;
        }
    }
}