// Script: Selected.cs (versión con referencia directa desde el inspector a textoInteraccion)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selected : MonoBehaviour
{
    [System.Serializable]
    public struct MensajePorTag //Estruc tura Objeto que nos seriva para crear una lista
    {
        public string tag;
        public string mensaje;
    }

    public List<MensajePorTag> mensajesInteractivos; //aqui pondremos los objetos creados
    public float distancia = 15f;
    public Texture2D puntero;
    public LayerMask mask;
    public TMP_Text textoUI; // Referencia directa desde el Inspector

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detected");
        if (textoUI != null)
        {
            textoUI.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (textoUI != null) textoUI.gameObject.SetActive(false);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            string tagDetectado = hit.collider.tag;
            string mensaje = ObtenerMensajePorTag(tagDetectado);

            if (!string.IsNullOrEmpty(mensaje) && textoUI != null)
            {
                textoUI.text = mensaje;
                textoUI.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (tagDetectado == "Door")
                {
                    hit.collider.transform.GetComponent<DoorS>()?.ChangeDoorOnState();
                }
                else
                {
                    var recolectable = hit.collider.GetComponent<ObjetoRecolectable>();
                    if (recolectable != null)
                    {
                        bool fueAgregado = InventarioHotbar.Instancia.AgregarItem(recolectable.datosItem);
                        if (fueAgregado)
                        {
                            Destroy(recolectable.gameObject);
                        }
                    }
                }
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
    }

    string ObtenerMensajePorTag(string tag)
    {
        foreach (var mensaje in mensajesInteractivos)
        {
            if (mensaje.tag == tag)
                return mensaje.mensaje;
        }
        return "";
    }

    private void OnGUI()
    {
        if (puntero != null)
        {
            Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
            GUI.DrawTexture(rect, puntero);
        }
    }
}