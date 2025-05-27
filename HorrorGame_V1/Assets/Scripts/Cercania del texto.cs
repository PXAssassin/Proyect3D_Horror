using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cercaniadeltexto : MonoBehaviour
{
    public Transform jugador; // Arrastra el jugador aquí en el Inspector
    public float distanciaActivacion = 5f;

    private GameObject canvasGO;

    void Start()
    {
        canvasGO = GetComponentInChildren<Canvas>(true)?.gameObject;
        if (canvasGO == null)
        {
            Debug.LogError("No se encontró un Canvas hijo");
        }
    }

    void Update()
    {
        if (jugador == null || canvasGO == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);
        canvasGO.SetActive(distancia <= distanciaActivacion);
    }
}
