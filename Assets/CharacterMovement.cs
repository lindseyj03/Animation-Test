using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    // private CharacterController characterController;
    // public float Speed = 5f;

    void Awake ()
    {
        input = new PlayerInput();

        input.CharacterControls.Movement.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        input.CharacterControls.Movement.canceled += ctx => {
            currentMovement = Vector2.zero; // Reset movement when joystick is released
            movementPressed = false; // Stop walking when joystick is released
        };
        
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        // characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleRotation();

        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // characterController.Move(move * Time.deltaTime * Speed);
    }

    void handleRotation()
    {
        if (movementPressed) // Only rotate when there's movement input
        {
        Vector3 currentPosition = transform.position; ;
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 positionToLookAt = currentPosition + newPosition;
        transform.LookAt(positionToLookAt);
        // Rotate smoothly
            Vector3 direction = positionToLookAt - currentPosition;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smoothing
            }
        }
    }
    

    void handleMovement () {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        if (movementPressed && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        }

        if (!movementPressed && isWalking) {
            animator.SetBool(isWalkingHash, false);
        }

        if ((movementPressed && runPressed) && !isRunning) {
            animator.SetBool(isRunningHash, true);
        }

        if ((!movementPressed && !runPressed) && isRunning) {
            animator.SetBool(isRunningHash, false);
        }

    }
    

    void OnEnable ()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable ()
    {
        input.CharacterControls.Disable();
    }
}

