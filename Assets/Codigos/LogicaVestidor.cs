using UnityEngine;

public class LogicaVestidor : MonoBehaviour
{
    [Header("Cámaras")]
    public GameObject camaraJugador;
    public GameObject camaraMaquillaje;

    [Header("Referencias de Posición")]
    public Transform puntoParadaJugador;

    [Header("Interfaz")]
    public GameObject canvasVestidor;

    private GameObject jugadorActual;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorActual = other.gameObject;

            PlayerMovement pm = jugadorActual.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = false;

            Animator anim = jugadorActual.GetComponent<Animator>();
            if (anim != null) anim.SetBool("isWalking", false);

            // ¡EL TRUCO DE LA CÁPSULA! 
            // La apagamos y la dejamos apagada para que el mouse pase directo a la cara
            CharacterController cc = jugadorActual.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            if (puntoParadaJugador != null)
            {
                jugadorActual.transform.position = puntoParadaJugador.position;
                jugadorActual.transform.rotation = puntoParadaJugador.rotation;
            }

            if (camaraJugador != null) camaraJugador.SetActive(false);
            if (camaraMaquillaje != null) camaraMaquillaje.SetActive(true);

            if (canvasVestidor != null) canvasVestidor.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SalirDelVestidor()
    {
        if (canvasVestidor != null) canvasVestidor.SetActive(false);
        if (camaraMaquillaje != null) camaraMaquillaje.SetActive(false);
        if (camaraJugador != null) camaraJugador.SetActive(true);

        if (jugadorActual != null)
        {
            PlayerMovement pm = jugadorActual.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = true;

            // ¡VITAL! Volvemos a encender la cápsula de física al salir 
            // para que no se caiga al vacío al volver a caminar
            CharacterController cc = jugadorActual.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}