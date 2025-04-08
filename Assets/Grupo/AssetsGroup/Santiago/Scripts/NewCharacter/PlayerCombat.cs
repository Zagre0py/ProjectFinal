using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Combo Settings")]
    [SerializeField] private int maxComboHits = 4; // Máximo número de golpes en el combo
    [SerializeField] private float comboWindow = 0.5f; // Tiempo para continuar el combo
    [SerializeField] private float inputBufferTime = 0.2f; // Tiempo que guarda el input

    // Referencias a componentes
    private Animator animator; // Controlador de animaciones
    private PlayerInput playerInput; // Sistema de input

    // Variables de estado
    private int currentCombo = 0; // Fase actual del combo (1-3)
    private bool isAttacking = false; // ¿Está atacando?
    private bool canAcceptComboInput = false; // ¿Acepta inputs para combo?
    private bool inputBuffered = false; // Input almacenado
    private float lastInputTime = 0; // Momento del último input

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Attack"].started += OnAttackInput;
    }

    private void OnDisable()
    {
        playerInput.actions["Attack"].started -= OnAttackInput;
    }

    private void Update()
    {
        HandleBufferedInput();
        CheckComboReset();
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        lastInputTime = Time.time;

        if (!isAttacking)
        {
            StartCombo();
        }
        else if (canAcceptComboInput)
        {
            ExecuteNextCombo();
        }
        else
        {
            BufferInput();
        }
    }

    private void StartCombo()
    {
        currentCombo = 1;
        isAttacking = true;

        animator.ResetTrigger("AnyAttack");
        animator.SetTrigger("Attack1");
        animator.SetInteger("ComboPhase", currentCombo);
    }

    private void ExecuteNextCombo()
    {
        currentCombo++;
        inputBuffered = false;

        animator.ResetTrigger("AnyAttack");
        animator.SetTrigger("Attack" + currentCombo);
        animator.SetInteger("ComboPhase", currentCombo);
    }

    private void BufferInput()
    {
        if (currentCombo < maxComboHits)
        {
            inputBuffered = true;
            Invoke(nameof(ClearBufferedInput), inputBufferTime);
        }
    }

    private void ClearBufferedInput()
    {
        inputBuffered = false;
    }

    private void HandleBufferedInput()
    {
        if (inputBuffered && canAcceptComboInput)
        {
            ExecuteNextCombo();
        }
    }

    private void CheckComboReset()
    {
        if (isAttacking && Time.time - lastInputTime > comboWindow)
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        currentCombo = 0;
        isAttacking = false;
        inputBuffered = false;
        animator.SetInteger("ComboPhase", 0);
    }

    // Animation Events
    public void OpenComboWindow()
    {
        canAcceptComboInput = true;

        if (inputBuffered)
        {
            ExecuteNextCombo();
        }
    }

    public void CloseComboWindow()
    {
        canAcceptComboInput = false;
    }

    public void OnAttackEnd()
    {
        if (currentCombo >= maxComboHits)
        {
            ResetCombo();
        }
    }
}