using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para objetos interactivos que pueden ser activados,
/// en este caso, se destruyen al activarse.
/// </summary>
public class ObjInteractivoo : MonoBehaviour
{
    /// <summary>
    /// Método para activar el objeto, que en este caso destruye el GameObject.
    /// </summary>
    public void ActivarObjeto()
    {
        Destroy(gameObject);
    }
}
