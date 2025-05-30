using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase singleton que controla el icono que se arrastra en la UI.
/// </summary>
public class DragIcon : MonoBehaviour
{
    /// <summary>
    /// Instancia estática para acceso global al DragIcon.
    /// </summary>
    public static DragIcon Instance;

    /// <summary>
    /// Imagen UI que muestra el icono arrastrado.
    /// </summary>
    public Image iconoUI;

    /// <summary>
    /// Método llamado al inicializar el objeto.
    /// Asigna la instancia y oculta el icono por defecto.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Ocultar();
    }

    /// <summary>
    /// Muestra el icono con el sprite especificado en la posición dada.
    /// </summary>
    /// <param name="sprite">Sprite que se mostrará como icono.</param>
    /// <param name="posicion">Posición en pantalla donde se mostrará el icono.</param>
    public void Mostrar(Sprite sprite, Vector3 posicion)
    {
        iconoUI.sprite = sprite;
        iconoUI.enabled = true;
        iconoUI.transform.position = posicion;
    }

    /// <summary>
    /// Mueve el icono a una nueva posición.
    /// </summary>
    /// <param name="posicion">Nueva posición en pantalla del icono.</param>
    public void Mover(Vector3 posicion)
    {
        iconoUI.transform.position = posicion;
    }

    /// <summary>
    /// Oculta el icono y elimina el sprite asignado.
    /// </summary>
    public void Ocultar()
    {
        iconoUI.sprite = null;
        iconoUI.enabled = false;
    }
}
