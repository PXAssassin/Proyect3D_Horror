using UnityEngine;

/// <summary>
/// Controla la apertura y cierre de una puerta rotando alrededor de un pivote personalizado,
/// incluyendo sonidos y visualizaci�n en el editor.
/// </summary>
public class DoorS : MonoBehaviour
{
    [Header("Configuraci�n B�sica")]
    /// <summary>
    /// Indica si la puerta est� abierta o cerrada.
    /// </summary>
    public bool doorOpen = false;

    /// <summary>
    /// �ngulo de rotaci�n de la puerta cuando est� abierta.
    /// </summary>
    public float doorOpenAngle = 90f;

    /// <summary>
    /// �ngulo de rotaci�n de la puerta cuando est� cerrada.
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
    /// Posici�n inicial de la puerta.
    /// </summary>
    private Vector3 initialPosition;

    /// <summary>
    /// Rotaci�n inicial de la puerta.
    /// </summary>
    private Quaternion initialRotation;

    /// <summary>
    /// Guarda la posici�n y rotaci�n inicial de la puerta al iniciar.
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
    /// Llama a la rotaci�n en cada frame.
    /// </summary>
    void Update()
    {
        RotateAroundPivot();
    }

    /// <summary>
    /// Aplica la rotaci�n de la puerta alrededor del pivote definido, hacia el �ngulo objetivo.
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
    /// Reproduce el sonido de abrir o cerrar puerta en la posici�n actual del objeto.
    /// </summary>
    void PlayDoorSound()
    {
        AudioClip clip = doorOpen ? openDoor : closeDoor;
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// Dibuja una esfera y una l�nea en la escena para visualizar el pivote de rotaci�n de la puerta.
    /// Solo se muestra cuando el objeto est� seleccionado en el editor.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pivotWorldPos = transform.TransformPoint(pivotOffset);
        Gizmos.DrawSphere(pivotWorldPos, 0.1f);
        Gizmos.DrawLine(pivotWorldPos, transform.position);
    }
}
