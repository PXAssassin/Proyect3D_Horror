using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventarioItemData : ScriptableObject
{
    public string id;
    public string nombreItem;
    public Sprite icono;
    public GameObject prefab;
    public int maximoItem = 1;

    [Header("Caracter�sticas de combate")]
    public bool esArma = false;          // Si es un arma que puede atacar
    public int da�o = 0;                 // Da�o que puede causar
    public string nombreAnimacionAtaque; // Nombre de la animaci�n para atacar (ej: "AtacarCuchillo")
}

