using UnityEngine;
using UnityEngine.UI; // Necesario para manejar el texto

public class GloboIndicacion : MonoBehaviour
{
    public GameObject globoCompleto; // El Canvas del globo

    void Update()
    {
        // Hace que el texto siempre rote hacia la cámara (Billboard effect)
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                             Camera.main.transform.rotation * Vector3.up);
        }
    }

    // Llama a esta función para ocultar el mensaje
    public void OcultarMensaje()
    {
        if (globoCompleto != null)
        {
            globoCompleto.SetActive(false);
        }
    }
}