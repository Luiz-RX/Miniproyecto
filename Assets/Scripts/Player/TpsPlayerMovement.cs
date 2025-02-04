using UnityEngine;

public class TpsPlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 5f;
    public float backwardMultiplier = 0.6f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    public Transform cameraTransform; // Referencia a la cámara
    public float rotationSpeed = 10f; // Velocidad de rotación del personaje

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private bool isCrouching = false;
    private float originalHeight;
    private float crouchHeight = 1f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
        CheckGrounded();
        HandleRotation(); // 💡 Nueva función para hacer que siempre mire a la cámara
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (cameraTransform == null) return; // Evitar errores si la cámara no está asignada

        // Obtener la dirección hacia adelante y la derecha de la cámara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Asegurar que no afecte el movimiento vertical
        camForward.y = 0;
        camRight.y = 0;

        // Normalizar para evitar velocidades inconsistentes
        camForward.Normalize();
        camRight.Normalize();

        // Calcular dirección de movimiento con respecto a la cámara
        Vector3 moveDirection = (camForward * moveZ + camRight * moveX).normalized;

        // Aplicar multiplicador de velocidad si se mueve hacia atrás
        if (moveZ < 0)
        {
            moveDirection *= backwardMultiplier;
        }

        float currentSpeed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);

        Vector3 targetVelocity = moveDirection * currentSpeed;
        targetVelocity.y = rb.linearVelocity.y; // Mantener la velocidad vertical (gravedad y salto)
        rb.linearVelocity = targetVelocity;
    }

    void HandleRotation()
    {
        if (cameraTransform == null) return;

        // Obtener la rotación de la cámara pero sin inclinar el personaje en el eje X
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        // Aplicar la rotación suavemente con Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                capsuleCollider.height = crouchHeight;
                capsuleCollider.center = new Vector3(0, crouchHeight / 2, 0);
            }
            else
            {
                capsuleCollider.height = originalHeight;
                capsuleCollider.center = new Vector3(0, originalHeight / 2, 0);
            }
        }
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);
    }
}
