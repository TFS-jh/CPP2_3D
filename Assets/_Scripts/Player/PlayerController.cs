using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private Camera mainCamera;

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
        if (InputManager.IsInitialized) 
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
        mainCamera = Camera.main;

        if (cc == null)
            throw new MissingComponentException("CharacterController component is missing from this GameObject!");

        if (mainCamera == null)
            throw new MissingComponentException("Main Camera cannot be found!");

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
        Vector3 projectedMoveDirection = ProjectedMoveDirection();
        UpdatePlayerVelocity(projectedMoveDirection);
        UpdatePlayerRotation(projectedMoveDirection);

        cc.Move(velocity * Time.fixedDeltaTime);
    }

    // Used for adjusting movement with Freelook Cam
    private Vector3 ProjectedMoveDirection()
    {
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * movementInput.y + right * movementInput.x;
    }

    void UpdatePlayerVelocity(Vector3 projectedMoveDirection)
    {
        // Updates player velocity based on input
        velocity.x = projectedMoveDirection.x * movementSpeed;
        velocity.z = projectedMoveDirection.z * movementSpeed;

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

    void UpdatePlayerRotation(Vector3 projectedMoveDirection)
    {
        if (projectedMoveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(projectedMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        }
    }

    private void Update()
    {
        //Ray newRay = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        //if (Physics.Raycast(newRay, out RaycastHit hit, 30f))
        //{
        //    // Debug.Log(hit.collider.name + " gameObject detected in front of player!");
        //}

        //Debug.DrawRay(newRay.origin, newRay.direction * 30, Color.red);
    }


    // Useful for detecting what objects the player collides with
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Player has hit: " +  hit.transform.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered trigger: " + other.transform.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player entered collision: " + collision.gameObject.name);
    }
}
