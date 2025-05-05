using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TMPro.Examples;

public class ControladorMenuJugar : MonoBehaviour
{
    public TMP_InputField campoNombrePartida;
    public TMP_Dropdown desplegableRanuras;
    public GameObject panelNueva;
    public GameObject panelCargar;
    public GameObject panelBorrar;

    private void Start()
    {
        ActualizarDesplegable();
    }

    public void MostrarPanelNueva() => panelNueva.SetActive(true);
    public void MostrarPanelCargar() => panelCargar.SetActive(true);
    public void MostrarPanelBorrar() => panelBorrar.SetActive(true);

    //Cancelar borrado
    public void Cancelar()
    {
        panelNueva.SetActive(false);
        panelCargar.SetActive(false);
        panelBorrar.SetActive(false);
    }

    public void CrearPartidaNueva()
    {
        string nombre = campoNombrePartida.text.Trim();
        if (string.IsNullOrEmpty(nombre)) return;

        foreach (string ranura in GestorGuardado.ObtenerRanuras())
        {
            if (!GestorGuardado.ExistePartida(ranura))
            {
                DatosPartida datos = new DatosPartida()
                {
                    nombrePartida = nombre,
                    fechaCreacion = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    escenaGuardada = "Prenivel"
                };

                GestorGuardado.GuardarPartida(datos, ranura);
                SceneManager.LoadScene("Prenivel");
                return;
            }
        }

        Debug.LogWarning("Todas las ranuras ocupadas.");
    }

    public void CargarPartidaSeleccionada()
    {
        string ranura = ObtenerRanuraSeleccionada();
        if (!GestorGuardado.ExistePartida(ranura)) return;

        DatosPartida datos = GestorGuardado.CargarPartida(ranura);
        SceneManager.LoadScene(datos.escenaGuardada);
    }

    public void BorrarPartidaSeleccionada()
    {
        string ranura = ObtenerRanuraSeleccionada();
        if (GestorGuardado.ExistePartida(ranura))
            GestorGuardado.BorrarPartida(ranura);

        ActualizarDesplegable();
    }

    private string ObtenerRanuraSeleccionada()
    {
        int indice = desplegableRanuras.value;
        return GestorGuardado.ObtenerRanuras()[indice];
    }

    private void ActualizarDesplegable()
    {
        desplegableRanuras.ClearOptions();
        var opciones = new System.Collections.Generic.List<string>();

        foreach (var ranura in GestorGuardado.ObtenerRanuras())
        {
            if (GestorGuardado.ExistePartida(ranura))
            {
                DatosPartida datos = GestorGuardado.CargarPartida(ranura);
                opciones.Add($"{datos.nombrePartida} - {datos.escenaGuardada}");
            }
            else
            {
                opciones.Add("Vacía");
            }
        }

        desplegableRanuras.AddOptions(opciones);
    }
}