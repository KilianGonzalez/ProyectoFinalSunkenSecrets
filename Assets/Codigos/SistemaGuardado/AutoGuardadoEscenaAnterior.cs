using UnityEngine;

public class AutoGuardadoEscenaAnterior : MonoBehaviour
{
    private void Start()
    {
        string escenaAnterior = GestorPersistente.ObtenerEscenaAnterior();

        if (GestorPersistente.EsEscenaValida(escenaAnterior))
        {
            string ranuraActiva = GestorGuardado.ObtenerRanuraActiva();
            if (!string.IsNullOrEmpty(ranuraActiva) && GestorGuardado.ExistePartida(ranuraActiva))
            {
                DatosPartida datos = GestorGuardado.CargarPartida(ranuraActiva);
                datos.escenaGuardada = escenaAnterior;
                GestorGuardado.GuardarPartida(datos, ranuraActiva);

                Debug.Log("Guardado automático: " + escenaAnterior);
            }
        }
    }
}
