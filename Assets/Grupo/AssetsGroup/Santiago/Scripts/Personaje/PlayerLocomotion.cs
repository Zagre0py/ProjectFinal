using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace SG
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandle animatorHandler;



        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandle>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }
        private void Update()
        {
            float delta = Time.deltaTime;
            inputHandler.TickInput(delta);
            //HandleRollingAndSprinting(delta);
            HandleMovement(delta);

        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleMovement(float delta)
        {

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if (animatorHandler.canRotate)
            {

                HandleRotation(delta);
            }
        }

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);
            myTransform.rotation = targetRotation;
        }

        private void HandleRollingAndSprinting(float delta)
{
    if (animatorHandler.anim.GetBool("isInteracting"))
        return;

    if(inputHandler.rollflag)
    {
        // Resetear el flag inmediatamente
        inputHandler.rollflag = false;
        
        // Calcular dirección
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        
        if(inputHandler.moveAmount > 0.1f) // Usar un umbral pequeño
        {
            animatorHandler.PlayTargetAnimation("Rolling", true);
            moveDirection.y = 0;
            
            // Normalizar y suavizar la rotación
            if(moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = Quaternion.Slerp(
                    myTransform.rotation,
                    targetRotation,
                    rotationSpeed * delta * 10f // Rotación más rápida para el roll
                );
            }
            
            // Aplicar movimiento durante el roll
            float rollSpeed = movementSpeed * 1.5f;
            Vector3 rollVelocity = moveDirection.normalized * rollSpeed;
            rigidbody.velocity = new Vector3(rollVelocity.x, rigidbody.velocity.y, rollVelocity.z);
        }
        else
        {
            animatorHandler.PlayTargetAnimation("BackStep", true);
        }
    }
}
        #endregion
    }
}