using UnityEngine;

public class AsignarReferenciasInventario : MonoBehaviour
{
    void Start()
    {
        if (InventarioHotbar.Instancia != null)
        {
            InventarioHotbar.Instancia.ReasignarReferenciasEnEscena();
        }
    }
}
