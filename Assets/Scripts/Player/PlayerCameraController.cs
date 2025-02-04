using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float rotationSpeed = 10f; // Velocidad de rotación del personaje
    public Transform cameraTransform; // Referencia a la transformación de la cámara

    void Update()
    {
        RotatePlayerWithCamera();
    }

    void RotatePlayerWithCamera()
    {
        // Obtener la dirección hacia la que apunta la cámara (ignorando la inclinación vertical)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ignorar la componente vertical
        cameraForward.Normalize();

        // Rotar el personaje hacia la dirección de la cámara
        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
