using UnityEngine;

public class LogicaVestidor : MonoBehaviour
{
    [Header("Referencias de Posición")]
    public Transform puntoEsfera;       // La Esfera (Donde aparece la mona)
    public Transform puntoCamaraOutfit; // El objeto vacío 'PuntoCamaraVestidor'

    [Header("Interfaz")]
    public GameObject canvasVestidor;   // El Panel con el texto rosa y botones

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que sea la mona la que toca el cubo
        if (other.CompareTag("Player"))
        {
            // 1. Teletransportar al personaje a la Esfera
            other.transform.position = puntoEsfera.position;
            other.transform.rotation = puntoEsfera.rotation;

            // 2. Apagar movimiento para que no se salga de la zona
            if(other.GetComponent<ControlJugador>() != null)
                other.GetComponent<ControlJugador>().enabled = false;
            
            // 3. Apagar la cámara de Primera Persona para tomar control manual
            if(Camera.main.GetComponent<CamaraPrimeraPersona>() != null)
                Camera.main.GetComponent<CamaraPrimeraPersona>().enabled = false;

            // 4. Mover la Cámara al punto de referencia (PuntoCamaraVestidor)
            // Esto evita que la cámara se quede en el suelo
            Camera.main.transform.position = puntoCamaraOutfit.position;
            Camera.main.transform.rotation = puntoCamaraOutfit.rotation;

            // 5. Activar el Canvas y mostrar el mouse
            if(canvasVestidor != null) canvasVestidor.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Teletransporte y Cámara listos.");
        }
    }
}