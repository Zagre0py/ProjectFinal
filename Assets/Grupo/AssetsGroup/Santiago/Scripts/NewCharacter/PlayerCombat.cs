using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combo Settings")]
    [SerializeField] private float comboWindow = 0.5f; // Tiempo entre ataques para combo
    [SerializeField] private int maxComboHits = 3;     // Máximo de golpes en combo
    [SerializeField] private float inputBufferTime = 0.3f; // Tiempo para almacenar inputs

    // Componentes
    private Animator animator;
    private PlayerInput playerInput;
    private InputAction attackAction;

    // Variables de estado
    private int currentCombo = 0;
    private float lastAttackTime = 0;
    private float lastInputTime = 0;
    private bool isAttacking = false;
    private bool bufferedInput = false;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }

    private void OnEnable()
    {
        attackAction.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        attackAction.performed -= OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        lastInputTime = Time.time;

        if (animator.GetBool("CanCombo") || !isAttacking)
        {
            TryAttack();
        }
        else
        {
            StartCoroutine(BufferInput());
        }
    }

    private void Update()
    {
        // Input buffer para combos fluidos
        if (bufferedInput && animator.GetBool("CanCombo"))
        {
            TryAttack();
            bufferedInput = false;
        }
    }

    private IEnumerator BufferInput()
    {
        bufferedInput = true;
        yield return new WaitForSeconds(inputBufferTime);
        bufferedInput = false;
    }

    private void TryAttack()
    {
        // Cancelar reset pendiente
        StopAllCoroutines();

        // Actualizar estado del combo
        lastAttackTime = Time.time;
        currentCombo = Mathf.Clamp(currentCombo + 1, 1, maxComboHits);

        // Disparar animación
        string triggerName = "Attack" + currentCombo;
        animator.ResetTrigger("Attack" + (currentCombo - 1)); // Limpiar trigger anterior

        animator.SetTrigger(triggerName);
        animator.SetInteger("ComboPhase", currentCombo);

        Debug.Log($"Ejecutando: {triggerName}");

        // Iniciar corrutina para resetear combo si no hay input
        StartCoroutine(ComboResetCoroutine());
    }

    private IEnumerator ComboResetCoroutine()
    {
        yield return new WaitForSeconds(comboWindow);

        // Solo resetear si no hubo nuevos ataques
        if (Time.time - lastAttackTime >= comboWindow)
        {
            currentCombo = 0;
            animator.SetInteger("ComboPhase", 0);
            Debug.Log("Combo reseteado");
        }
    }

    // Llamado desde Animation Event al 80% de cada animación
    public void EnableComboWindow()
    {
        animator.SetBool("CanCombo", true);
        Debug.Log("Ventana de combo activada");
    }

    // Llamado desde Animation Event al final de cada animación
    public void FinishAttack()
    {
        animator.SetBool("CanCombo", false);
        isAttacking = false;

        // Resetear si es el último ataque del combo
        if (currentCombo == maxComboHits)
        {
            currentCombo = 0;
            animator.SetInteger("ComboPhase", 0);
        }
    }
}