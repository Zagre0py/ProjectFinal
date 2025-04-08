using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Combo Settings")]
    [SerializeField] private int maxComboHits = 3;
    [SerializeField] private float comboWindow = 0.5f;
    [SerializeField] private float inputBufferTime = 0.2f;

    // Component references
    private Animator animator;
    private PlayerInput playerInput;
    
    // State variables
    private int currentCombo = 0;
    private bool isAttacking = false;
    private bool canAcceptComboInput = false;
    private bool inputBuffered = false;
    private float lastInputTime = 0;

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