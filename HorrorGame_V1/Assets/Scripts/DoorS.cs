using UnityEngine;

/// <summary>
/// Controla la apertura y cierre de una puerta rotando alrededor de un pivote personalizado,
/// incluyendo sonidos y visualización en el editor.
/// </summary>
public class DoorS : MonoBehaviour
{
    [Header("Configuración Básica")]
    /// <summary>
    /// Indica si la puerta está abierta o cerrada.
    /// </summary>
    public bool doorOpen = false;

    /// <summary>
    /// Ángulo de rotación de la puerta cuando está abierta.
    /// </summary>
    public float doorOpenAngle = 90f;

    /// <summary>
    /// Ángulo de rotación de la puerta cuando está cerrada.
    /// </summary>
    public float doorCloseAngle = 0f;

    /// <summary>
    /// Suavidad del movimiento de apertura/cierre.
    /// </summary>
    public float smoot = 3.0f;

    [Header("Punto de Pivote")]
    /// <summary>
    /// Desplazamiento del pivote respecto al centro del objeto puerta.
    /// </summary>
    public Vector3 pivotOffset = new Vector3(0.5f, 0, 0);

    [Header("Sonidos")]
    /// <summary>
    /// Sonido que se reproduce al abrir la puerta.
    /// </summary>
    public AudioClip openDoor;

    /// <summary>
    /// Sonido que se reproduce al cerrar la puerta.
    /// </summary>
    public AudioClip closeDoor;

    /// <summary>
    /// Posición inicial de la puerta.
    /// </summary>
    private Vector3 initialPosition;

    /// <summary>
    /// Rotación inicial de la puerta.
    /// </summary>
    private Quaternion initialRotation;

    /// <summary>
    /// Guarda la posición y rotación inicial de la puerta al iniciar.
    /// </summary>
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    /// <summary>
    /// Cambia el estado de la puerta (abierta/cerrada) y reproduce el sonido correspondiente.
    /// </summary>
    public void ChangeDoorOnState()
    {
        doorOpen = !doorOpen;
        PlayDoorSound();
    }

    /// <summary>
    /// Llama a la rotación en cada frame.
    /// </summary>
    void Update()
    {
        RotateAroundPivot();
    }

    /// <summary>
    /// Aplica la rotación de la puerta alrededor del pivote definido, hacia el ángulo objetivo.
    /// </summary>
    void RotateAroundPivot()
    {
        float targetAngle = doorOpen ? doorOpenAngle : doorCloseAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        Vector3 worldPivot = transform.TransformPoint(pivotOffset);

        transform.RotateAround(worldPivot, Vector3.up,
                              (targetAngle - transform.eulerAngles.y) * smoot * Time.deltaTime);
    }

    /// <summary>
    /// Reproduce el sonido de abrir o cerrar puerta en la posición actual del objeto.
    /// </summary>
    void PlayDoorSound()
    {
        AudioClip clip = doorOpen ? openDoor : closeDoor;
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// Dibuja una esfera y una línea en la escena para visualizar el pivote de rotación de la puerta.
    /// Solo se muestra cuando el objeto está seleccionado en el editor.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pivotWorldPos = transform.TransformPoint(pivotOffset);
        Gizmos.DrawSphere(pivotWorldPos, 0.1f);
        Gizmos.DrawLine(pivotWorldPos, transform.position);
    }
}
