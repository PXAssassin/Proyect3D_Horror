using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public TMP_Text textoVida;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AsignarTextoVida(textoVida);
        }
    }
}

