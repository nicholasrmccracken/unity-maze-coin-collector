using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private float walkSpeed = 4.0f;
        [SerializeField] private float sprintMultiplier = 2.0f;

        [Header("ROTATION SETTINGS")]
        [SerializeField] private float maxRotationSpeed = 180f;
        [SerializeField] private float mouseSensitivty = 2.0f;

        [Header("REFERENCES")]
        [SerializeField] private Transform cameraTransform;

        private PlayerInput playerInput;
        private Vector2 moveInput;
        private Vector2 lookInput;
        private bool isSprinting = false;
        private float xRotation = 0f;
        private float yRotation = 0f;
        private Rigidbody rb;

        /* 
         * Initializes input actions, locks cursor, and sets up the Rigidbody.
         */
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            playerInput.actions["Move"].performed += context => moveInput = context.ReadValue<Vector2>();
            playerInput.actions["Move"].canceled += context => moveInput = Vector2.zero;
            playerInput.actions["Look"].performed += context => lookInput = context.ReadValue<Vector2>();
            playerInput.actions["Look"].canceled += context => lookInput = Vector2.zero;
            playerInput.actions["Sprint"].performed += _ => isSprinting = !isSprinting;

            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            Cursor.lockState = CursorLockMode.Locked;
        }

        /* 
         * Handles player movement logic based on input values.
         * Uses MoveTowards for smooth position transitions.
         */
        private void Update()
        {
            if (Mathf.Abs(moveInput.x) < 0.1f) moveInput.x = 0f;
            if (Mathf.Abs(moveInput.y) < 0.1f) moveInput.x = 0f;
    
            float movementSpeed = isSprinting ? walkSpeed * sprintMultiplier : walkSpeed;
            
            Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            moveDirection = transform.TransformDirection(moveDirection);

            if (moveDirection.magnitude > 0.1f) 
            {
                moveDirection = moveDirection.normalized;
                Vector3 targetPosition = transform.position + 
                    movementSpeed * Time.deltaTime * moveDirection;

                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    targetPosition, 
                    movementSpeed * Time.deltaTime);
            }

            rb.linearVelocity = Vector3.zero;
        }

        /* 
         * Handles player rotation based on mouse or joystick input.
         * Uses RotateTowards to smoothly rotate the player and camera.
         */
        private void LateUpdate()
        {   
            if (Mathf.Abs(lookInput.x) < 0.1f) lookInput.x = 0f;
            if (Mathf.Abs(lookInput.y) < 0.1f) lookInput.y = 0f;

            yRotation += lookInput.x * mouseSensitivty;
            xRotation -= lookInput.y * mouseSensitivty;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            Quaternion targetYawRotation = Quaternion.Euler(0f, yRotation, 0f);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                targetYawRotation,
                 maxRotationSpeed * Time.deltaTime);

            Quaternion targetPitchRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            cameraTransform.localRotation = Quaternion.RotateTowards(
                cameraTransform.localRotation, 
                targetPitchRotation, 
                maxRotationSpeed * Time.deltaTime);
            cameraTransform.position = transform.position;
        }
    }
}
