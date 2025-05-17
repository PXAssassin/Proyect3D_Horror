using UnityEngine;
using UnityEngine.UI;

public class DragIcon : MonoBehaviour
{
    public static DragIcon Instance;

    public Image iconoUI;

    private void Awake()
    {
        Instance = this;
        Ocultar();
    }

    public void Mostrar(Sprite sprite, Vector3 posicion)
    {
        iconoUI.sprite = sprite;
        iconoUI.enabled = true;
        iconoUI.transform.position = posicion;
    }

    public void Mover(Vector3 posicion)
    {
        iconoUI.transform.position = posicion;
    }

    public void Ocultar()
    {
        iconoUI.sprite = null;
        iconoUI.enabled = false;
    }
}
