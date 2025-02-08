using UnityEngine;

public class TpsCameraController : MonoBehaviour
{
    public Transform target; // Jugador
    public float distance = 3.0f; // Distancia de la cámara al jugador
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float zoomSpeed = 2f;

    public float rotationSpeed = 200f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;

    public Transform aimTarget; // Objeto invisible para apuntar

    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        if (target != null)
        {
            Vector3 angles = transform.eulerAngles;
            yaw = angles.y;
            pitch = angles.x;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        transform.position = position;
        transform.LookAt(target.position);

        // Actualizar posición del objetivo de disparo
        UpdateAimTarget();
    }

    void UpdateAimTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            aimTarget.position = hit.point; // Si colisiona con algo, mover el objetivo
        }
        else
        {
            aimTarget.position = transform.position + transform.forward * 50f; // Si no, proyectar lejos
        }
    }
}
