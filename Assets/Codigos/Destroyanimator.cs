using UnityEngine;

public class DestroyAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Obtener la duraci�n de la animaci�n
        float tiempoDuracion = animator.GetCurrentAnimatorStateInfo(0).length;

        // Desactivar el Animator al terminar la animaci�n
        Invoke(nameof(DetenerAnimacion), tiempoDuracion);
    }

    void DetenerAnimacion()
    {
        animator.enabled = false;
        Destroy(gameObject); // Luego destruye el objeto
    }
}
