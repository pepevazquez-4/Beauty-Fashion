using UnityEngine;
using UnityEngine.UI;

public class BotonOutfitCompleto : MonoBehaviour
{
    public Renderer mallaSueter;
    public Renderer mallaPantalon;
    public Renderer mallaZapatos;

    public Texture nuevaTexSueter;
    public Texture nuevaTexPantalon;
    public Texture nuevaTexZapatos;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AplicarTodoElLook);
    }

    public void AplicarTodoElLook()
    {
        // Cambiamos _BaseMap por _MainTex que es el nombre correcto para el Shader Standard
        if (mallaSueter != null && nuevaTexSueter != null)
            mallaSueter.sharedMaterial.SetTexture("_MainTex", nuevaTexSueter);

        if (mallaPantalon != null && nuevaTexPantalon != null)
            mallaPantalon.sharedMaterial.SetTexture("_MainTex", nuevaTexPantalon);

        if (mallaZapatos != null && nuevaTexZapatos != null)
            mallaZapatos.sharedMaterial.SetTexture("_MainTex", nuevaTexZapatos);

        Debug.Log("¡Outfit cambiado con éxito!");
    }
}