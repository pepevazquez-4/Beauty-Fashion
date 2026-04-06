using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrarAlSalon : MonoBehaviour
{
    public string nombreDelInterior = "InteriorSalon";

    private void OnTriggerEnter(Collider other)
    {
        // Esto nos dirá en la Consola quién chocó
        Debug.Log("Algo tocó la puerta: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Es el jugador! Cambiando a: " + nombreDelInterior);
            SceneManager.LoadScene(nombreDelInterior);
        }
        else
        {
            Debug.Log("Tocó la puerta pero no tiene el Tag 'Player'");
        }
    }
}