using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorMenuJugar : MonoBehaviour
{
    public TMP_InputField campoNombrePartida;
    public TMP_Dropdown desplegableCargar;
    public TMP_Dropdown desplegableBorrar;
    public GameObject panelNueva;
    public GameObject panelCargar;
    public GameObject panelBorrar;
    public GameObject panelRanuras;

    private void Start()
    {
        ActualizarDesplegableCargar();
        ActualizarDesplegableBorrar();
    }

    public void MostrarPanelNueva() => panelNueva.SetActive(true);

    public void MostrarPanelCargar()
    {
        ActualizarDesplegableCargar();
        panelCargar.SetActive(true);
    }

    public void MostrarPanelBorrar()
    {
        ActualizarDesplegableBorrar();
        panelBorrar.SetActive(true);
    }

    public void Cancelar()
    {
        panelNueva.SetActive(false);
        panelCargar.SetActive(false);
        panelBorrar.SetActive(false);
        panelRanuras.SetActive(false);
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
                GestorGuardado.EstablecerRanuraActiva(ranura);
                SceneManager.LoadScene("Prenivel");
                return;
            }
        }

        panelNueva.SetActive(false);
        panelRanuras.SetActive(true);
    }

    public void CargarPartidaSeleccionada()
    {
        string ranura = ObtenerRanuraSeleccionada(desplegableCargar);
        if (!GestorGuardado.ExistePartida(ranura)) return;

        DatosPartida datos = GestorGuardado.CargarPartida(ranura);
        GestorGuardado.EstablecerRanuraActiva(ranura);
        SceneManager.LoadScene(datos.escenaGuardada);
    }

    public void BorrarPartidaSeleccionada()
    {
        string ranura = ObtenerRanuraSeleccionada(desplegableBorrar);
        if (GestorGuardado.ExistePartida(ranura))
            GestorGuardado.BorrarPartida(ranura);

        ActualizarDesplegableCargar();
        ActualizarDesplegableBorrar();
    }

    private string ObtenerRanuraSeleccionada(TMP_Dropdown dropdown)
    {
        int indice = dropdown.value;
        return GestorGuardado.ObtenerRanuras()[indice];
    }

    private void ActualizarDesplegableCargar()
    {
        desplegableCargar.ClearOptions();
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

        desplegableCargar.AddOptions(opciones);
    }

    private void ActualizarDesplegableBorrar()
    {
        desplegableBorrar.ClearOptions();
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

        desplegableBorrar.AddOptions(opciones);
    }
}