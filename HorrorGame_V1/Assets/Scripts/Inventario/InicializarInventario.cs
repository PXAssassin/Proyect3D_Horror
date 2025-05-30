using UnityEngine;

/// <summary>
/// Se encarga de instanciar el prefab del inventario una �nica vez en la escena,
/// asegurando que persista entre cambios de escena.
/// </summary>
public class InicializadorInventario : MonoBehaviour
{
    /// <summary>
    /// Prefab del inventario que se instanciar�.
    /// </summary>
    public GameObject prefabInventario;

    /// <summary>
    /// Controla si el inventario ya fue instanciado para evitar duplicados.
    /// </summary>
    private static bool instanciado = false;

    /// <summary>
    /// Se ejecuta al iniciar el objeto y realiza la instancia si a�n no existe.
    /// </summary>
    void Awake()
    {
        if (!instanciado)
        {
            GameObject inventario = Instantiate(prefabInventario);
            DontDestroyOnLoad(inventario);
            instanciado = true;
        }
    }
}
