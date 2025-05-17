using UnityEngine;

public class DoorS : MonoBehaviour
{
    [Header("Configuración Básica")]
    public bool doorOpen = false;
    public float doorOpenAngle = 90f; // Ángulo cuando está abierta
    public float doorCloseAngle = 0f;  // Ángulo cuando está cerrada
    public float smoot = 3.0f;

    [Header("Punto de Pivote")]
    public Vector3 pivotOffset = new Vector3(0.5f, 0, 0); // Ajusta este valor según tu puerta

    [Header("Sonidos")]
    public AudioClip openDoor;
    public AudioClip closeDoor;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ChangeDoorOnState()
    {
        doorOpen = !doorOpen;
        PlayDoorSound();
    }

    void Update()
    {
        RotateAroundPivot();
    }

    void RotateAroundPivot()
    {
        float targetAngle = doorOpen ? doorOpenAngle : doorCloseAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        // Calculamos la posición del pivote en el mundo
        Vector3 worldPivot = transform.TransformPoint(pivotOffset);

        // Rotamos alrededor del pivote
        transform.RotateAround(worldPivot, Vector3.up,
                              (targetAngle - transform.eulerAngles.y) * smoot * Time.deltaTime);
    }

    void PlayDoorSound()
    {
        AudioClip clip = doorOpen ? openDoor : closeDoor;
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    // Visualización del pivote en el Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pivotWorldPos = transform.TransformPoint(pivotOffset);
        Gizmos.DrawSphere(pivotWorldPos, 0.1f);
        Gizmos.DrawLine(pivotWorldPos, transform.position);
    }
}
