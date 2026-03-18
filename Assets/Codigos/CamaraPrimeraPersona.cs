using UnityEngine;

public class CamaraPrimeraPersona : MonoBehaviour
{
    public Transform objetivo; // La mona
    public Vector3 ojosOffset = new Vector3(0, 1.6f, 0.2f); // Altura de los ojos
    public float suavizado = 10f;

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Calculamos la posición exacta de los ojos
            Vector3 posicionOjos = objetivo.position + (objetivo.rotation * ojosOffset);

            // Movemos la cámara a esa posición
            transform.position = Vector3.Lerp(transform.position, posicionOjos, suavizado * Time.deltaTime);

            // Hacemos que la cámara mire hacia donde mira la mona
            transform.rotation = Quaternion.Lerp(transform.rotation, objetivo.rotation, suavizado * Time.deltaTime);
        }
    }
}