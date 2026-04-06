using UnityEngine;

public class QuitarGlobo : MonoBehaviour
{
    public GameObject globoCanvas; // Arrastra el Canvas del globo aquí

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el que entró a la silla es el jugador
        if (other.CompareTag("Player"))
        {
            if (globoCanvas != null)
            {
                globoCanvas.SetActive(false); // Apaga el globo
                Debug.Log("Jugador sentado, quitando globo.");
            }
        }
    }
}