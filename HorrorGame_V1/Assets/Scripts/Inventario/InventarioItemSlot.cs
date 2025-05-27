[System.Serializable]

/// <summary>
/// Esta Clase permite almacenar los datos del objeto itemData
/// manejar la cantidad que tenia. Saber luego si tenia informacion o no.
/// </summary>
public class InventarioItemSlot
{
    public InventarioItemData itemData; //Esto nos podra almacenar la informacion de los objetos que predefinimos
    public int cantidad; //Cantidad de objetos que tenemos, esto serviria para un adicional al inventario, en caso de tener objetos sumables en cantidad (no se implemento)

    public bool EstaVacio => itemData == null; //Controlar luego para ver si itemData tiene informacion.
}

