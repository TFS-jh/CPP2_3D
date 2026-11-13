using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;

    [Header("Movement Speed")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float timeToJumpApex = 0.4f;

    private float gravity;
    private float initJumpVel;

    // Values filled by inputs
    private Vector2 movementInput;
    private Vector3 velocity;
    private bool jumpPressed = false;

    private void OnEnable() 
    {
        if (InputManager.IsInitialized) {
            InputManager.Instance.OnMoveEvent += OnMove;
            InputManager.Instance.OnJumpEvent += OnJump;
        }
    }

    private void OnDisable() 
    { 
        if (InputManager.Instance != null) 
        { 
            InputManager.Instance.OnMoveEvent -= OnMove; 
            InputManager.Instance.OnJumpEvent -= OnJump;
        }
    }

    void OnMove(Vector2 move) => movementInput = move;

    void OnJump(bool isPressed) => jumpPressed = isPressed;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        if (cc == null)
        {
            throw new MissingComponentException("CharacterController component is missing from this GameObject!");
        }

        CalculateJumpVars();
    }

    private void OnValidate() => CalculateJumpVars();

    private void CalculateJumpVars()
    {
        if (timeToJumpApex <= 0 || float.IsNaN(timeToJumpApex) || float.IsInfinity(timeToJumpApex))
        {
            Debug.LogWarning("timeToJumpApex must be greater than 0 and a valid number. Resetting to default (0.4f)...");
            timeToJumpApex = 0.4f;
        }

        if (jumpHeight <= 0 || float.IsNaN(jumpHeight) || float.IsInfinity(jumpHeight))
        {
            Debug.LogWarning("jumpHeight must be greater than 0 and a valid number. Resetting to default (5f)...");
            jumpHeight = 5f;
        }

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        initJumpVel = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // preferred over Update() when dealing with kinematic movement for consistency
    void FixedUpdate()
    {
        UpdatePlayerVelocity();
        cc.Move(velocity * Time.fixedDeltaTime);
    }

    void UpdatePlayerVelocity()
    {
        // Updates player velocity based on input
        velocity.x = movementInput.x * movementSpeed;
        velocity.z = movementInput.y * movementSpeed;

        // Checks if grounded and resets yVel to -skinWidth to keep player grounded
        if (cc.isGrounded)
        {
            velocity.y = -cc.skinWidth;
            
            // If jump is pressed, set yVel to to initJumpVel
            if (jumpPressed)
            {
                velocity.y = initJumpVel;
            }
        }
        else // Else apply gravity to yVel
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }
}
