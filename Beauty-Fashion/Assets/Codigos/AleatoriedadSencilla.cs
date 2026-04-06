using UnityEngine;
using UnityEngine.SceneManagement;

public class AleatoriedadSencilla : MonoBehaviour
{
    [Header("Interfaz y Cámaras")]
    public GameObject canvasRosa;      // El panel con Outfit 1, 2, 3
    public GameObject panelMaquillaje; // El panel con Rojo, Azul, Negro
    public GameObject camaraCara;      // Tu CamaraMaquillaje_Real

    private GameObject outfitSeleccionado;

    public void ProbarOutfit(GameObject botonPresionado)
    {
        outfitSeleccionado = botonPresionado;
        var scriptTexturas = botonPresionado.GetComponent<BotonOutfitCompleto>();
        if (scriptTexturas != null)
        {
            scriptTexturas.enabled = true;
        }
    }

    public void ValidarSeleccion()
    {
        if (outfitSeleccionado == null) return;

        int suerte = Random.Range(1, 4);

        if (suerte == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Activando Fase de Maquillaje...");

            // 1. Apagamos el menú de selección de ropa
            if (canvasRosa != null) canvasRosa.SetActive(false);

            // 2. Encendemos el panel de maquillaje
            if (panelMaquillaje != null)
            {
                panelMaquillaje.SetActive(true);

                // FORZADO: Aseguramos que el Canvas padre del panel esté encendido
                Canvas parentCanvas = panelMaquillaje.GetComponentInParent<Canvas>();
                if (parentCanvas != null) parentCanvas.enabled = true;
            }

            // 3. Zoom a la cara
            if (camaraCara != null) camaraCara.SetActive(true);

            // 4. LIBERAR MOUSE (Vital para ver y usar botones)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void FinalizarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}