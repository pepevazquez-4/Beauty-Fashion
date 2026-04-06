using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaSalon : MonoBehaviour
{
    [Header("Cosas que se apagan y prenden")]
    public GameObject panelRopa;       // El rosa (Outfit 1, 2, 3)
    public GameObject panelMaquillaje; // El de colores (Rojo, Azul, Negro)
    public GameObject camaraZoom;      // Tu CamaraMaquillaje_Real

    private GameObject botonGuardado;

    // 1. Pon esto en los botones Outfit 1, 2 y 3
    public void ProbarRopa(GameObject elBoton)
    {
        botonGuardado = elBoton;
        // Activa el script de texturas que ya tienes en el botón
        var script = elBoton.GetComponent<BotonOutfitCompleto>();
        if (script != null) script.enabled = true;
    }

    // 2. Pon esto en el botón "Listo!!!"
    public void BotonListo()
    {
        if (botonGuardado == null) return;

        // Tira los dados (1 de 3 falla)
        int suerte = Random.Range(1, 4);

        if (suerte == 1)
        {
            // MALA SUERTE: Reinicia la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // BUENA SUERTE: Cambia de fase
            if (panelRopa != null) panelRopa.SetActive(false);
            if (panelMaquillaje != null) panelMaquillaje.SetActive(true);
            if (camaraZoom != null) camaraZoom.SetActive(true);

            // Asegura que el mouse se vea
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}