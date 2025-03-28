using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [Header ("Configuración")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [SerializeField] private Rigidbody playerRigidbody;
    //[SerializeField] private Animator playerAnimControl;
    private Vector3 moveDirection;
    public bool isGrounded;

    [Header("Camara")]
    public Transform cameraTransform; //Referencia a la camara principal
    
    void Start()
    {
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

        //Animaciones

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            ApplyJump();
            //playerAnimControl.SetBool("isJumping", true);
        }
        //else if (isGrounded)
        //{
        //    playerAnimControl.SetBool("isJumping", false);
        //}

        //if (moveDirection != Vector3.zero && isGrounded)
        //{
        //    playerAnimControl.SetBool("isRunning", true);
        //}
        //else
        //{
        //    playerAnimControl.SetBool("isRunning", false);
        //}
    }

    void ApplyPhysicsMovement()
    {
        //Movimiento con fuerzas fisicas por medio de metodo MovePosition
        playerRigidbody.MovePosition(transform.localPosition + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void ApplyJump()
    {
        //Usamos metodo AddForce en Rigidbody para aplicar una fuerza vertical con modo de Impulso
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        //else if (playerRigidbody.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        //{
        //    playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        //}
    }

    //Esto nos comunica cuando colisiona con un objeto que tenga el tag "Floor"
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            //playerAnimControl.SetBool("isGrounded", isGrounded);
        }
    }

    //Esto nos comunica cuando deja de colisionar con un objeto con el tag "Floor"
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
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

}
