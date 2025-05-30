using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Clase que permite detectar objetos frente al jugador mediante un raycast,
/// mostrar mensajes interactivos en pantalla y realizar acciones seg�n el objeto detectado.
/// </summary>
public class Selected : MonoBehaviour
{
    /// <summary>
    /// Estructura para asociar un tag con un mensaje que se mostrar� en pantalla.
    /// </summary>
    [System.Serializable]
    public struct MensajePorTag
    {
        public string tag;       // Tag del objeto a detectar
        public string mensaje;   // Mensaje a mostrar cuando se detecte el objeto con ese tag
    }

    [Header("Configuraci�n de Interacci�n")]
    [Tooltip("Lista de mensajes interactivos asociados a tags de objetos")]
    public List<MensajePorTag> mensajesInteractivos;

    [Tooltip("Distancia m�xima para detectar objetos")]
    public float distancia = 15f;

    [Tooltip("Textura del puntero que se muestra en el centro de la pantalla")]
    public Texture2D puntero;

    [Tooltip("M�scara de capas para filtrar objetos detectados por el raycast")]
    public LayerMask mask;

    [Tooltip("Referencia directa al texto UI de TextMeshPro para mostrar mensajes")]
    public TMP_Text textoUI;

    void Start()
    {
        // Establecer la m�scara para detectar s�lo objetos en la capa "Raycast Detected"
        mask = LayerMask.GetMask("Raycast Detected");

        // Asegurarse de que el texto de interacci�n est� oculto al iniciar
        if (textoUI != null)
        {
            textoUI.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Por defecto ocultar el texto cada frame para evitar mostrar mensajes err�neos
        if (textoUI != null) textoUI.gameObject.SetActive(false);

        // Realizar un raycast desde la posici�n del objeto hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            string tagDetectado = hit.collider.tag;

            // Obtener el mensaje correspondiente al tag detectado
            string mensaje = ObtenerMensajePorTag(tagDetectado);

            // Si hay mensaje para mostrar, actualizar el texto UI y mostrarlo
            if (!string.IsNullOrEmpty(mensaje) && textoUI != null)
            {
                textoUI.text = mensaje;
                textoUI.gameObject.SetActive(true);
            }

            // Detectar la pulsaci�n de la tecla E para interactuar con el objeto
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Si el objeto es una puerta, cambiar su estado
                if (tagDetectado == "Door")
                {
                    hit.collider.transform.GetComponent<DoorS>()?.ChangeDoorOnState();
                }
                else
                {
                    // Si el objeto es recolectable, intentar agregarlo al inventario
                    var recolectable = hit.collider.GetComponent<ObjetoRecolectable>();
                    if (recolectable != null)
                    {
                        bool fueAgregado = InventarioHotbar.Instancia.AgregarItem(recolectable.datosItem);
                        if (fueAgregado)
                        {
                            // Si fue agregado con �xito, destruir el objeto en la escena
                            Destroy(recolectable.gameObject);
                        }
                    }
                }
            }

            // Dibujar una l�nea roja para depuraci�n en la escena que muestra el raycast
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
    }

    /// <summary>
    /// Busca y devuelve el mensaje asociado a un tag espec�fico.
    /// </summary>
    /// <param name="tag">El tag a buscar</param>
    /// <returns>Mensaje asociado al tag o cadena vac�a si no existe</returns>
    string ObtenerMensajePorTag(string tag)
    {
        foreach (var mensaje in mensajesInteractivos)
        {
            if (mensaje.tag == tag)
                return mensaje.mensaje;
        }
        return "";
    }

    /// <summary>
    /// Dibuja el puntero personalizado en el centro de la pantalla.
    /// </summary>
    private void OnGUI()
    {
        if (puntero != null)
        {
            Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
            GUI.DrawTexture(rect, puntero);
        }
    }
}
