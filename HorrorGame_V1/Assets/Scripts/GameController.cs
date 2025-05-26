using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Cambiado para TextMesh Pro

public class GameController : MonoBehaviour
{
    [SerializeField]

    public static int puntos = 0;
    public TMP_Text txtPuntos;  // Usando TMP_Text en lugar de Text

    // Start is called before the first frame update
    void Start()
    {
        txtPuntos.text = puntos.ToString();  // Inicializar el texto al inicio
    }

    // Update is called once per frame
    void Update()
    {

    }
}
