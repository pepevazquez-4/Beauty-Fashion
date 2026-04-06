using UnityEngine;

public class MecanicaMaquillaje : MonoBehaviour
{
    [Header("Conexión con Lógica Global")]
    public LogicaJuego logicaSistema;

    [Header("Referencias de Posición")]
    public Transform puntoCara;           // El zoom para maquillar
    public Transform puntoCamaraFinal;    // Posición de cámara en paredes rosas
    public Transform puntoModelajeFinal;  // NUEVO: Donde se para la mona frente a las paredes

    [Header("Personaje")]
    public GameObject personaje;          // Arrastra aquí a 'untitled@Female'

    [Header("Paneles de Interfaz")]
    public GameObject panelMaquillaje;
    public GameObject panelFinal;

    [Header("Escenario")]
    public GameObject escenarioFinal;     // El objeto que tiene las paredes rosas

    [Header("Configuración de Pintura")]
    public Renderer mallaCara;
    public int tamañoPincel = 12;

    private Texture2D texturaPintable;
    private Color colorActual = Color.red;
    private bool puedePintar = false;

    // --- COLORES PREMIUM (Nuevos) ---
    private Color rosaPremium = new Color(0.91f, 0.12f, 0.39f, 1f);   // Rosa Mate
    private Color nudePremium = new Color(0.74f, 0.56f, 0.56f, 1f);   // Nude Satinado
    private Color vinoPremium = new Color(0.50f, 0f, 0.13f, 1f);      // Vino Mate

    public void IrAMaquillaje()
    {
        if (logicaSistema != null) logicaSistema.DesactivarTodoElDialogo();
        if (panelMaquillaje != null) panelMaquillaje.SetActive(true);

        if (puntoCara != null)
        {
            Camera.main.transform.position = puntoCara.position;
            Camera.main.transform.rotation = puntoCara.rotation;
        }

        PrepararTextura();
        puedePintar = true;
    }

    void PrepararTextura()
    {
        if (mallaCara == null) return;
        Texture original = mallaCara.material.mainTexture;
        texturaPintable = new Texture2D(original.width, original.height);
        RenderTexture tmp = RenderTexture.GetTemporary(original.width, original.height, 0);
        Graphics.Blit(original, tmp);
        RenderTexture anterior = RenderTexture.active;
        RenderTexture.active = tmp;
        texturaPintable.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        texturaPintable.Apply();
        RenderTexture.active = anterior;
        RenderTexture.active = tmp;
        RenderTexture.ReleaseTemporary(tmp);
        mallaCara.material.mainTexture = texturaPintable;
    }

    void Update()
    {
        if (puedePintar && Input.GetMouseButton(0)) PintarConMouse();
    }

    void PintarConMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == mallaCara.gameObject)
            {
                Vector2 uv = hit.textureCoord;
                int x = (int)(uv.x * texturaPintable.width);
                int y = (int)(uv.y * texturaPintable.height);
                for (int i = -tamañoPincel; i < tamañoPincel; i++)
                {
                    for (int j = -tamañoPincel; j < tamañoPincel; j++)
                    {
                        if (i * i + j * j <= tamañoPincel * tamañoPincel)
                            texturaPintable.SetPixel(x + i, y + j, colorActual);
                    }
                }
                texturaPintable.Apply();
            }
        }
    }

    // ACTUALIZADO: Ahora usa los colores Premium pero con tus mismos nombres de botones
    public void SeleccionarColor(string color)
    {
        if (color == "rojo") colorActual = rosaPremium;
        if (color == "azul") colorActual = nudePremium;
        if (color == "negro") colorActual = vinoPremium;
    }

    public void FinalizarTodo()
    {
        puedePintar = false;
        if (panelMaquillaje != null) panelMaquillaje.SetActive(false);

        if (escenarioFinal != null) escenarioFinal.SetActive(true);

        if (personaje != null && puntoModelajeFinal != null)
        {
            personaje.transform.position = puntoModelajeFinal.position;
            personaje.transform.rotation = puntoModelajeFinal.rotation;
        }

        if (puntoCamaraFinal != null)
        {
            Camera.main.transform.position = puntoCamaraFinal.position;
            Camera.main.transform.rotation = puntoCamaraFinal.rotation;
        }

        if (panelFinal != null) panelFinal.SetActive(true);
        if (logicaSistema != null) logicaSistema.FinalizarLook();
    }
}