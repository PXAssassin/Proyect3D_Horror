using UnityEngine;

// <summary>
/// Define los datos b�sicos para un objeto que ira en el inventario, con el fin de hacer mas comoda la
/// creacion de objetos que interactuarian con el entorno y el inventarioTipo Hotbar
/// Este ScriptableObject permite crear �tems configurables desde el editor de Unity.
/// </summary>
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
   
    [Header("Caracter�sticas de curaci�n")]
    public bool esCurativo = false; //Pero si es curativo, no ejecuta ataques pero servira para subir la vida del personaje
    public int cantidadCura = 0;
}


