using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [Header ("Configuración")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    public LayerMask floorMask;
    Vector3 groundCheckPosition; 

    [SerializeField] private Rigidbody playerRigidbody;
    //[SerializeField] private Animator playerAnimControl;
    private Vector3 moveDirection;

    public bool isGrounded;
    float groundCheckDistance;
    float bufferCheckDistance = 0.1f;

    [Header("Camara")]
    public Transform cameraTransform; //Referencia a la camara principal
    
    void Start()
    {
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2);

        playerRigidbody = GetComponent<Rigidbody>();
        //playerAnimControl = GetComponent<Animator>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

   
    void Update()
    {
        HandleInput ();



        if (moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
        }


    }

    void FixedUpdate()
    {
        CheckGrounded();
        ApplyPhysicsMovement();
    }

    void HandleInput()
    {
        //Entrada de movimiento
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Se convierte la dirección de movimiento a coordenadas relativas la cámara
        Vector3 forward = cameraTransform.forward;  
        Vector3 right = cameraTransform.right;

        //Se evita el movimiento en el eje Y para que el player no se incline
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        //Salto

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            ApplyJump();
        }
    }

    void ApplyPhysicsMovement()
    {
        //Movimiento con fuerzas fisicas por medio de metodo MovePosition
        playerRigidbody.MovePosition(transform.localPosition + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void CheckGrounded()
    {
        groundCheckPosition = new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z);

        isGrounded = Physics.OverlapSphere(groundCheckPosition, 0.3f, floorMask).Length > 0;
    }

    void ApplyJump()
    {
        //Usamos metodo AddForce en Rigidbody para aplicar una fuerza vertical con modo de Impulso
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void RotateTowardsMovement()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
            );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckPosition, 0.3f);
    }
}
