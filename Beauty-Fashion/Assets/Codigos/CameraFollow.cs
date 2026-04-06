using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    public Transform target; // Aquí arrastraremos a la mona

    [Header("Configuración")]
    // Distancia y altura fija de la cámara respecto al jugador (X, Y, Z)
    // Valores sugeridos para empezar: X:0, Y:3, Z:-5
    public Vector3 offset = new Vector3(0f, 3f, -5f);

    // Qué tan suave es el seguimiento (valores más bajos = más lento/suave)
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        // Si no hay objetivo, no hacemos nada para evitar errores
        if (target == null) return;

        // 1. Calculamos la posición Fija a la que debe ir la cámara
        Vector3 desiredPosition = target.position + offset;

        // 2. Suavizamos la transición desde la posición actual a la deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // 3. Hacemos que la cámara siempre mire un poco arriba del objetivo
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}