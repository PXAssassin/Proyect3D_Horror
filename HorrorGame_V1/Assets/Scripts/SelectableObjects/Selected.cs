using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 15f;
    public Texture2D puntero;
    //public GameObject textInfoMachete;
    //public GameObject textInforDoor;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detected"); // Asegúrate de tener este layer en Unity y asignado a los objetos interactivos
        //textInfoMachete.SetActive(false);
        //textInfoMachete.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            if (hit.collider.CompareTag("Recolectable"))
            {
                //textInfoMachete.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    ObjetoRecolectable recolectable = hit.collider.GetComponent<ObjetoRecolectable>();
                    if (recolectable != null)
                    {
                        bool fueAgregado = InventarioHotbar.Instancia.AgregarItem(recolectable.datosItem);
                        if (fueAgregado)
                        {
                            Destroy(recolectable.gameObject);
                            Debug.Log("Objeto recogido: " + recolectable.datosItem.nombreItem);
                        }
                        else
                        {
                            Debug.Log("Inventario LLenos");
                        }
                    }
                }
            }
            if (hit.collider.CompareTag("Door"))
            {
                //textInforDoor.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<DoorS>().ChangeDoorOnState();
                }
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            //textInfoMachete.SetActive(false);
            //textInforDoor.SetActive(false);
        }

    }

    private void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);
    }
}
