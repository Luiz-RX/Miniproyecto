using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float rotationSpeed = 10f; // Velocidad de rotaci�n del personaje
    public Transform cameraTransform; // Referencia a la transformaci�n de la c�mara

    void Update()
    {
        RotatePlayerWithCamera();
    }

    void RotatePlayerWithCamera()
    {
        // Obtener la direcci�n hacia la que apunta la c�mara (ignorando la inclinaci�n vertical)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ignorar la componente vertical
        cameraForward.Normalize();

        // Rotar el personaje hacia la direcci�n de la c�mara
        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
