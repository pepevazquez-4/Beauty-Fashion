using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Componentes (Arrastrar en el Inspector)")]
    public CharacterController controller; // Link in Inspector
    public Animator anim; // Link in Inspector

    [Header("Configuración de Movimiento")]
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Header("Gravedad (El nucleo del problema)")]
    public float gravity = -9.81f; // Gravity force
    private Vector3 velocity; // Internal velocity vector

    [Header("Controles Nuevos (Input System)")]
    public InputAction moveAction; // Define in Inspector

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        // -- CONTROL DE SEGURIDAD: VERIFICAR SI LAS REFERENCIAS ESTÁN LLENAS --
        // This stops the game if the slots are empty and puts an error in the console.
        if (controller == null)
        {
            Debug.LogError("Error: La casilla 'Controller' en el script PlayerMovement está VACÍA. Por favor, arrastra el componente Character Controller en el inspector de " + gameObject.name);
            return; // Detiene la ejecución de este frame
        }

        // 1. Gravedad básica
        // This applies gravity force to the internal velocity vector
        velocity.y += gravity * Time.deltaTime;

        // Then move the controller based on that velocity
        // This is how the character gets gravity and checks for collision with the floor
        controller.Move(velocity * Time.deltaTime);

        // --- Reiniciar la velocidad vertical si está tocando el suelo ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantener una pequeña fuerza hacia abajo para mantenerse pegado
        }

        // --- El resto del código de movimiento usando Input System ---
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the character forward
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (anim != null) anim.SetBool("isWalking", true);
        }
        else
        {
            if (anim != null) anim.SetBool("isWalking", false);
        }
    }
}