using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidadBus = 5.0f;
    public float velocidadGiro = 150.0f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float movV = Input.GetAxis("Vertical");
        float movH = Input.GetAxis("Horizontal");

        // Movimiento y Rotación
        transform.Translate(0, 0, movV * velocidadBus * Time.deltaTime);
        transform.Rotate(0, movH * velocidadGiro * Time.deltaTime, 0);

        // Activamos la animación si hay movimiento
        bool moviendose = (movV != 0 || movH != 0);
        anim.SetBool("isWalking", moviendose);
    }
}