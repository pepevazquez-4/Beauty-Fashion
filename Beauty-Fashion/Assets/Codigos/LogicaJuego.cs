using UnityEngine;
using TMPro;

public class LogicaJuego : MonoBehaviour
{
    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoDialogo;
    public GameObject panelDialogo;   // El objeto "Panel" rosa
    public GameObject panelRopa;      // El objeto "PanelRopa"

    [Header("Escenarios")]
    public GameObject escenarioX;     // Tu salón de belleza actual
    public GameObject escenarioBonito; // El set de foto (puedes usar un plano de color)

    [Header("Cámara Final")]
    public Transform puntoCamaraFinal; // Crea un Empty y arrástralo aquí

    void Start()
    {
        // Configuración inicial
        if (escenarioX != null) escenarioX.SetActive(true);
        if (escenarioBonito != null) escenarioBonito.SetActive(false);

        panelRopa.SetActive(false);
        panelDialogo.SetActive(true);
        textoDialogo.text = "¡Hola! Bienvenida a Beauty Fashion.";
    }

    // Lo llama el botón "Siguiente"
    public void AvanzarAOutfit()
    {
        textoDialogo.text = "Elige un outfit y maquillaje para comenzar.";
        panelRopa.SetActive(true);
    }

    // Lo llama el script de MecanicaMaquillaje al presionar "Listo!!!"
    public void DesactivarTodoElDialogo()
    {
        if (panelDialogo != null) panelDialogo.SetActive(false);
        if (panelRopa != null) panelRopa.SetActive(false);
    }

    // Lo llama el script de MecanicaMaquillaje al presionar "¡TERMINAR!"
    public void FinalizarLook()
    {
        textoDialogo.text = "¡Te ves increíble! Look completado.";

        // Apagamos el salón y prendemos el fondo nuevo
        if (escenarioX != null) escenarioX.SetActive(false);
        if (escenarioBonito != null) escenarioBonito.SetActive(true);

        // Movemos la cámara para el "Shot" final
        if (puntoCamaraFinal != null)
        {
            Camera.main.transform.position = puntoCamaraFinal.position;
            Camera.main.transform.rotation = puntoCamaraFinal.rotation;
        }
    }
}