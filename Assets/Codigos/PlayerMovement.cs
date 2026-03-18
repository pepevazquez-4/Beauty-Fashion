using UnityEngine;
using UnityEngine.InputSystem; // Añadimos la librería del nuevo sistema

public class PlayerMovement : MonoBehaviour
{
    [Header("Componentes")]
    public CharacterController controller;
    public Animator anim;

    [Header("Configuración de Movimiento")]
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Header("Gravedad")]
    public float gravity = -9.81f;
    private Vector3 velocity;

    [Header("Controles Nuevos")]
    public InputAction moveAction; // Variable para el nuevo Input

    void OnEnable()
    {
        moveAction.Enable(); // Hay que prender el input
    }

    void OnDisable()
    {
        moveAction.Disable(); // Y apagarlo
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Leemos los valores del nuevo Input System (nos da X y Y en lugar de X y Z)
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (anim != null) anim.SetBool("isWalking", true);
        }
        else
        {
            if (anim != null) anim.SetBool("isWalking", false);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}