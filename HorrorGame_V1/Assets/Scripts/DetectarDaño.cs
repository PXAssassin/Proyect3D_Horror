using UnityEngine;
public class DetectarDaño : MonoBehaviour
{
    public int dañoRecibido = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ataque"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RecibirDaño(dañoRecibido);
            }
        }
    }
}
