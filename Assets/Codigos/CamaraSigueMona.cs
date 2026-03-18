using UnityEngine;

public class CamaraSigueMona : MonoBehaviour
{
    public Transform objetivo; // Aquí arrastraremos a la mona

    // Estos valores definen qué tan cerca o lejos está la cámara
    public Vector3 offset = new Vector3(0, 1.8f, -2.5f);
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Calculamos la posición detrás del personaje usando su rotación
            Vector3 posicionDeseada = objetivo.position + (objetivo.rotation * offset);

            // Movemos la cámara de forma fluida
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);

            // Hacemos que la cámara siempre mire un poco arriba de la cintura de la mona
            transform.LookAt(objetivo.position + Vector3.up * 1.5f);
        }
    }
}