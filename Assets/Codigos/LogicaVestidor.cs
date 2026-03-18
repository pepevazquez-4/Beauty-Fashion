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
            // Apagamos la física un milisegundo para que Unity nos deje moverlo
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            // 1. Teletransportar al personaje a la Esfera
            other.transform.position = puntoEsfera.position;
            other.transform.rotation = puntoEsfera.rotation;

            // Volvemos a encender la física
            if (cc != null) cc.enabled = true;

            // 2. Apagar movimiento 
            if (other.GetComponent<PlayerMovement>() != null)
                other.GetComponent<PlayerMovement>().enabled = false;

            // 3. Apagar la cámara de seguimiento (¡VOLVEMOS A BUSCAR CAMERA FOLLOW!)
            if (Camera.main.GetComponent<CameraFollow>() != null)
                Camera.main.GetComponent<CameraFollow>().enabled = false;

            // 4. Mover la Cámara al punto de referencia
            Camera.main.transform.position = puntoCamaraOutfit.position;
            Camera.main.transform.rotation = puntoCamaraOutfit.rotation;

            // 5. Activar el Canvas y mostrar el mouse
            if (canvasVestidor != null) canvasVestidor.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Teletransporte y Cámara Fija listos.");
        }
    }
}