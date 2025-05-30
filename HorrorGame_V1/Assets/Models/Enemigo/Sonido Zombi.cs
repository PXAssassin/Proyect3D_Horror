using UnityEngine;

/// <summary>
/// Controla la reproducci�n de un sonido de zombi basado en la proximidad al jugador.
/// El sonido se activa cuando el jugador est� muy cerca y se desactiva al alejarse.
/// </summary>
public class SonidoZombi : MonoBehaviour
{
    [Tooltip("Fuente de audio que reproduce el sonido del zombi")]
    public AudioSource fuenteAudio;

    [Tooltip("Transform del jugador, asignar desde el Inspector")]
    public Transform player;

    [Tooltip("Distancia m�nima para activar el sonido (en unidades Unity)")]
    public float distanciaActivacion = 0.05f; // 5 cm

    [Tooltip("Distancia m�nima para desactivar el sonido (en unidades Unity)")]
    public float distanciaDesactivacion = 0.08f; // 8 cm

    private bool sonidoActivo = false;

    void Start()
    {
        // Si no se asign� la fuente de audio en el inspector, intentar obtenerla del mismo GameObject
        if (fuenteAudio == null)
        {
            fuenteAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Verificar que el Transform del jugador est� asignado para evitar errores
        if (player == null)
        {
            Debug.LogError("No se ha asignado el Transform del jugador.");
            return;
        }

        // Calcular la distancia entre el enemigo (zombi) y el jugador
        float distancia = Vector3.Distance(transform.position, player.position);

        // Si el sonido no est� activo y el jugador est� lo suficientemente cerca, reproducir sonido
        if (!sonidoActivo && distancia <= distanciaActivacion)
        {
            fuenteAudio.Play();
            sonidoActivo = true;
        }
        // Si el sonido est� activo y el jugador se aleja m�s all� de la distancia de desactivaci�n, detener sonido
        else if (sonidoActivo && distancia >= distanciaDesactivacion)
        {
            fuenteAudio.Stop();
            sonidoActivo = false;
        }
    }
}
