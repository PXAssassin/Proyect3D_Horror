using UnityEngine;

/// <summary>
/// Controla la reproducción de un sonido de zombi basado en la proximidad al jugador.
/// El sonido se activa cuando el jugador está muy cerca y se desactiva al alejarse.
/// </summary>
public class SonidoZombi : MonoBehaviour
{
    [Tooltip("Fuente de audio que reproduce el sonido del zombi")]
    public AudioSource fuenteAudio;

    [Tooltip("Transform del jugador, asignar desde el Inspector")]
    public Transform player;

    [Tooltip("Distancia mínima para activar el sonido (en unidades Unity)")]
    public float distanciaActivacion = 0.05f; // 5 cm

    [Tooltip("Distancia mínima para desactivar el sonido (en unidades Unity)")]
    public float distanciaDesactivacion = 0.08f; // 8 cm

    private bool sonidoActivo = false;

    void Start()
    {
        // Si no se asignó la fuente de audio en el inspector, intentar obtenerla del mismo GameObject
        if (fuenteAudio == null)
        {
            fuenteAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Verificar que el Transform del jugador esté asignado para evitar errores
        if (player == null)
        {
            Debug.LogError("No se ha asignado el Transform del jugador.");
            return;
        }

        // Calcular la distancia entre el enemigo (zombi) y el jugador
        float distancia = Vector3.Distance(transform.position, player.position);

        // Si el sonido no está activo y el jugador está lo suficientemente cerca, reproducir sonido
        if (!sonidoActivo && distancia <= distanciaActivacion)
        {
            fuenteAudio.Play();
            sonidoActivo = true;
        }
        // Si el sonido está activo y el jugador se aleja más allá de la distancia de desactivación, detener sonido
        else if (sonidoActivo && distancia >= distanciaDesactivacion)
        {
            fuenteAudio.Stop();
            sonidoActivo = false;
        }
    }
}
