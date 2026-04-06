using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSalon : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelRopa;       // El que tiene Outfit 1, 2, 3
    public GameObject panelMaquillaje; // El que tiene Rojo, Azul, Negro
    public GameObject camaraCara;      // Tu CamaraMaquillaje_Real

    [Header("Configuración de la Modelo")]
    public Renderer mallaCara;         // La malla que se va a pintar

    private GameObject outfitSeleccionado;

    // 1. Se asigna a los botones Outfit 1, 2 y 3
    public void SeleccionarRopa(GameObject boton)
    {
        outfitSeleccionado = boton;

        // Ejecuta tu script original de texturas que está en el botón
        var scriptTexturas = boton.GetComponent<BotonOutfitCompleto>();
        if (scriptTexturas != null)
        {
            scriptTexturas.enabled = true;
            Debug.Log("Outfit seleccionado: " + boton.name);
        }
    }

    // 2. Se asigna SOLO al botón "Listo!!!"
    public void ConfirmarSeleccion()
    {
        if (outfitSeleccionado == null)
        {
            Debug.LogWarning("Debes seleccionar un outfit primero.");
            return;
        }

        // 1 de 3 probabilidades de reinicio (33%)
        int suerte = Random.Range(1, 4);

        if (suerte == 1)
        {
            Debug.Log("Mala suerte, reiniciando salón...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("¡ÉXITO! Pasando a fase de maquillaje.");

            // Apagamos el menú de ropa
            if (panelRopa != null) panelRopa.SetActive(false);

            // Encendemos el menú de colores
            if (panelMaquillaje != null) panelMaquillaje.SetActive(true);

            // Activamos la cámara de zoom a la cara
            if (camaraCara != null) camaraCara.SetActive(true);

            // Forzamos que aparezca el mouse para poder picar los colores
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // 3. Se asigna a los botones de colores (Rojo, Azul, Negro)
    public void PintarCara(string color)
    {
        if (mallaCara == null) return;

        // Cambia el color del material de la cara
        if (color == "Rojo") mallaCara.material.color = Color.red;
        if (color == "Azul") mallaCara.material.color = Color.blue;
        if (color == "Negro") mallaCara.material.color = Color.black;
    }
}