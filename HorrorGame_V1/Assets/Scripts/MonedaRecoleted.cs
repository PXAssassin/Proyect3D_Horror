using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaRecoleted : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)//
    {
        if (collision.CompareTag("Player1")) // Cambiar a 2D
        {
            GameManager.Instance.sumValues(1); // Sumar 1 al puntaje
            Destroy(gameObject); // Destruir el objeto recolectado
        }
    }
}
