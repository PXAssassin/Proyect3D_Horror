using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activa o desactiva un objeto Canvas dependiendo de la cercanía del jugador.
/// </summary>
public class Cercaniadeltexto : MonoBehaviour
{
    /// <summary>
    /// Referencia al transform del jugador. Debe asignarse desde el Inspector.
    /// </summary>
    public Transform jugador;

    /// <summary>
    /// Distancia mínima para activar el Canvas.
    /// </summary>
    public float distanciaActivacion = 5f;

    /// <summary>
    /// Referencia al objeto Canvas hijo.
    /// </summary>
    private GameObject canvasGO;

    /// <summary>
    /// Inicializa la referencia al Canvas hijo del objeto.
    /// </summary>
    void Start()
    {
        canvasGO = GetComponentInChildren<Canvas>(true)?.gameObject;
        if (canvasGO == null)
        {
            Debug.LogError("No se encontró un Canvas hijo");
        }
    }

    /// <summary>
    /// Verifica la distancia entre el jugador y el objeto. 
    /// Activa o desactiva el Canvas según la proximidad.
    /// </summary>
    void Update()
    {
        if (jugador == null || canvasGO == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);
        canvasGO.SetActive(distancia <= distanciaActivacion);
    }
}
