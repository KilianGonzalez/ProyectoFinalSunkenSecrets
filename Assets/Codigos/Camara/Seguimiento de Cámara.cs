using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientodeCámara : MonoBehaviour
{
    public Transform target; // Referencia al personaje
    public float smoothSpeed = 5f; // Velocidad de suavizado
    public Vector3 offset = new Vector3(0, 0, -10); // Desplazamiento de la cámara

    void LateUpdate()
    {
        if (target != null)
        {
            // Posición deseada con el offset
            Vector3 desiredPosition = target.position + offset;
            // Suaviza el movimiento con Lerp
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
