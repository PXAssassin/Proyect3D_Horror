using UnityEngine;

public class InicializadorInventario : MonoBehaviour
{
    public GameObject prefabInventario;

    private static bool instanciado = false;

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
