using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemDoor : MonoBehaviour
{
    public bool doorOpen = false; //Verificar si la puerta esta abierta o cerrada
    public float doorOpenAngle = 90f; //Angulo de la puerta al estar abierta
    public float doorCloseAngle = 0.0f; //Angulo de la puerta al estar cerrada
    public float smoot = 3.0f;                 //Velocidad con la que rotara la puerta

    public AudioClip openDoor;
    public AudioClip closeDoor;

    public void ChangeDoorOnState()
    {
        doorOpen = !doorOpen;
    }
    void Update()
    {
        if (doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0,doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(closeDoor, transform.position, 0.8f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(openDoor, transform.position, 0.8f);
        }
    }
}
