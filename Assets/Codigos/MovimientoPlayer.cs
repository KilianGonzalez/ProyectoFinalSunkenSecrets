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

        // Aplicar fuerza solo si la velocidad es menor a la máxima
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection * moveForce, ForceMode2D.Force);
        }
    }
    void VoltearSprite()
    {
        if (rb.velocity.x > 0.1f)
        {
            spriterender.flipX = false;
        }
        else if (rb.velocity.x < -0.1f)
        {
            spriterender.flipX = true;
        }
    }

}
