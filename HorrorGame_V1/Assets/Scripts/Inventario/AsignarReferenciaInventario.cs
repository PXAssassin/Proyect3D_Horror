using UnityEngine;

/// <summary>
/// Clase encargada de reasignar las referencias del inventario Hotbar al iniciar la escena.
/// </summary>
public class AsignarReferenciasInventario : MonoBehaviour
{
    /// <summary>
    /// Método llamado al iniciar el objeto.
    /// Verifica si la instancia del inventario Hotbar existe y reasigna sus referencias en la escena actual.
    /// </summary>
    void Start()
    {
        if (InventarioHotbar.Instancia != null)
        {
            InventarioHotbar.Instancia.ReasignarReferenciasEnEscena();
        }
    }
}
