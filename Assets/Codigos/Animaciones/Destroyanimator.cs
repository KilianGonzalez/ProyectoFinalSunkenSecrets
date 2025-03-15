using UnityEngine;

public class DestroyAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Obtener la duración de la animación
        float tiempoDuracion = animator.GetCurrentAnimatorStateInfo(0).length;

        // Desactivar el Animator al terminar la animación
        Invoke(nameof(DetenerAnimacion), tiempoDuracion);
    }

    void DetenerAnimacion()
    {
        animator.enabled = false;
        Destroy(gameObject); // Luego destruye el objeto
    }
}
