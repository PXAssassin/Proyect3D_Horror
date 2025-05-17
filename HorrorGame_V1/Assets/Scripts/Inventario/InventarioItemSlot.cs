[System.Serializable]
public class InventarioItemSlot
{
    public InventarioItemData itemData; //Esto nos podra almacenar la informacion de los objetos que predefinimos
    public int cantidad;

    public bool EstaVacio => itemData == null;
}

