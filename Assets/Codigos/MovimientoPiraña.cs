using UnityEngine;

public class MovimientoPiraña : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 endPoint;
    public float distance = 5f; // Distancia a recorrer
    public float speed = 0.2f; // Velocidad

    private bool movingRight = true; // Dirección del pez

    void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + new Vector3(distance, 0, 0);
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPoint, endPoint, t);

        // Verificar si el pez debe voltearse
        bool shouldFlip = (movingRight && t >= 0.99f) || (!movingRight && t <= 0.01f);
        if (shouldFlip)
        {
            movingRight = !movingRight;
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invertir la escala en X
        transform.localScale = scale;
    }
}
