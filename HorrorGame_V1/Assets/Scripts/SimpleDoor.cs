using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public Transform puerta;             // Referencia al objeto de la puerta que rota (ej: Door_01_C)
    public float anguloAbierto = 90f;    // Ángulo de apertura
    public float velocidad = 3f;         // Velocidad de rotación
    public KeyCode teclaAbrir = KeyCode.E; // Tecla para abrir/cerrar
    private bool abierta = false;        // Estado actual
    private bool jugadorCerca = false;   // Si el jugador está en la zona

    private Quaternion rotacionInicial;
    private Quaternion rotacionAbierta;

    void Start()
    {
        if (puerta == null)
        {
            puerta = this.transform; // Por si no se asigna manualmente
        }

        rotacionInicial = puerta.localRotation;
        rotacionAbierta = Quaternion.Euler(0, anguloAbierto, 0) * rotacionInicial;
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(teclaAbrir))
        {
            abierta = !abierta;
        }

        Quaternion rotacionObjetivo = abierta ? rotacionAbierta : rotacionInicial;
        puerta.localRotation = Quaternion.Lerp(puerta.localRotation, rotacionObjetivo, Time.deltaTime * velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            jugadorCerca = false;
        }
    }
}
