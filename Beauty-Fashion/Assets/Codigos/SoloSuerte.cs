using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloSuerte : MonoBehaviour
{
    public void IntentarPasar()
    {
        // Tira un dado del 1 al 3. Si sale 1, reinicia.
        if (Random.Range(1, 4) == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}