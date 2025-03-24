using GameAssets;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Salto")]
    public float jumpHeight = 1.2f;
    public float gravity = -13f;
    public float groundCheckDistance = 0.03f;
    public LayerMask groundMask;

    private CharacterController controller;
    
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private Camera mainCamera;

    private Vector2 moveInput;
    private bool isSprinting;
    private bool jumpRequested;

    [SerializeField] AudioClip stepSound;
    private AudioSource audioSource;
    

    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        UpdateAnimator();
        
    }

    // Detección de suelo usando Raycast
    private void CheckGround()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // Evita acumulación de gravedad
    }

    // Movimiento basado en la cámara
    private void HandleMovement()
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float currentSpeed = isSprinting ? runSpeed : walkSpeed;
            controller.Move(moveDir * currentSpeed * Time.deltaTime);
        }
    }

    // Manejo del salto
    private void HandleJump()
    {
        if (jumpRequested && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
         
            jumpRequested = false;
        }
    }

    // Aplicar gravedad
    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Actualizar Animator
    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", moveInput.magnitude * (isSprinting ? 2f : 1f));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("Sprint", isSprinting);
    }

    
    // Eventos de Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<float>() > 0;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) jumpRequested = true;
    }

    public void PlayOnStep()
    {
        audioSource.PlayOneShot(stepSound);
    }
}
