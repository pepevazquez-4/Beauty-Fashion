using UnityEngine;

public class MecanicaMaquillaje : MonoBehaviour
{
    [Header("Conexión con Lógica Global")]
    public LogicaJuego logicaSistema; // Arrastra aquí el objeto 'AdministradorDeJuego'

    [Header("Referencias de Posición")]
    public Transform puntoCara;

    [Header("Paneles de Interfaz")]
    public GameObject panelMaquillaje;
    public GameObject panelFinal;

    [Header("Configuración de Pintura")]
    public Renderer mallaCara;
    public int tamañoPincel = 12;

    private Texture2D texturaPintable;
    private Color colorActual = Color.red;
    private bool puedePintar = false;

    // Esta función se activa con el botón "Listo!!!"
    public void IrAMaquillaje()
    {
        // 1. Limpiamos la pantalla usando el script de LogicaJuego
        if (logicaSistema != null)
        {
            logicaSistema.DesactivarTodoElDialogo();
        }

        // 2. Encendemos los botones de colores
        if (panelMaquillaje != null)
            panelMaquillaje.SetActive(true);

        // 3. Movemos la cámara a la posición de la cara
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

        // Creamos una copia de la textura original para poder rayarla
        Texture original = mallaCara.material.mainTexture;
        texturaPintable = new Texture2D(original.width, original.height);

        RenderTexture tmp = RenderTexture.GetTemporary(original.width, original.height, 0);
        Graphics.Blit(original, tmp);
        RenderTexture anterior = RenderTexture.active;
        RenderTexture.active = tmp;
        texturaPintable.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        texturaPintable.Apply();
        RenderTexture.active = anterior;
        RenderTexture.ReleaseTemporary(tmp);

        mallaCara.material.mainTexture = texturaPintable;
    }

    void Update()
    {
        // Si ya estamos en modo maquillaje y presionamos el clic
        if (puedePintar && Input.GetMouseButton(0))
        {
            PintarConMouse();
        }
    }

    void PintarConMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // LÍNEA DE DIAGNÓSTICO: Esto aparecerá en la consola de Unity
            Debug.Log("El mouse está tocando el objeto: " + hit.collider.name);

            // Verificamos si lo que tocamos es la cabeza
            if (hit.collider.gameObject == mallaCara.gameObject)
            {
                Vector2 uv = hit.textureCoord;
                int x = (int)(uv.x * texturaPintable.width);
                int y = (int)(uv.y * texturaPintable.height);

                // Pintamos un círculo alrededor del punto de contacto
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

    public void SeleccionarColor(string color)
    {
        if (color == "rojo") colorActual = Color.red;
        if (color == "azul") colorActual = Color.blue;
        if (color == "negro") colorActual = Color.black;
    }

    public void FinalizarTodo()
    {
        puedePintar = false;
        if (panelMaquillaje != null) panelMaquillaje.SetActive(false);
        if (panelFinal != null) panelFinal.SetActive(true);

        // Avisamos a la lógica global que termine
        if (logicaSistema != null) logicaSistema.FinalizarLook();
    }
}