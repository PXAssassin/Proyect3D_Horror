using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase singleton que controla el icono que se arrastra en la UI.
/// </summary>
public class DragIcon : MonoBehaviour
{
    /// <summary>
    /// Instancia est�tica para acceso global al DragIcon.
    /// </summary>
    public static DragIcon Instance;

    /// <summary>
    /// Imagen UI que muestra el icono arrastrado.
    /// </summary>
    public Image iconoUI;

    /// <summary>
    /// M�todo llamado al inicializar el objeto.
    /// Asigna la instancia y oculta el icono por defecto.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Ocultar();
    }

    /// <summary>
    /// Muestra el icono con el sprite especificado en la posici�n dada.
    /// </summary>
    /// <param name="sprite">Sprite que se mostrar� como icono.</param>
    /// <param name="posicion">Posici�n en pantalla donde se mostrar� el icono.</param>
    public void Mostrar(Sprite sprite, Vector3 posicion)
    {
        iconoUI.sprite = sprite;
        iconoUI.enabled = true;
        iconoUI.transform.position = posicion;
    }

    /// <summary>
    /// Mueve el icono a una nueva posici�n.
    /// </summary>
    /// <param name="posicion">Nueva posici�n en pantalla del icono.</param>
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
