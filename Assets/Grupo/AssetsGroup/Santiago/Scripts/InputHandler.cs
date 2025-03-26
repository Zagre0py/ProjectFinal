using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        // [Header("Movement Input")]
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        // [Header("Action Inputs")]
        public bool b_Input;
        public bool rollflag;
        public bool isInteracting;
        public bool rb_Input;
        public bool rt_Input;

        private PlayerControls inputActions;
        private PlayerAttacker playerAttacker;
        private PlayerInventory playerInventory;
        private Vector2 movementInput;
        private Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            // HandleAttackInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }

        private void HandleRollInput(float delta)
        {
            bool previousBInput = b_Input;
            b_Input = inputActions.PLayerActions.Roll.ReadValue<float>() > 0.5f;

            // Edge detection - solo activa en el frame de presión
            rollflag = b_Input && !previousBInput;
        }

        private void HandleAttackInput(float delta)
        {
            rb_Input = inputActions.PLayerActions.RB.triggered;
            rt_Input = inputActions.PLayerActions.RT.triggered;

            if (rb_Input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }

            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }
    }
}