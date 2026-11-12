using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    InputSystem_Actions input;

    Vector2 movementInput;

    public float speed = 500f;
    public float jumpHeight = 3f;
    public int maxJumps = 5;

    private int jumpCount = 0;

    private void OnEnable()
    {
        input = new InputSystem_Actions();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;

        input.Player.Jump.started += OnJump;

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Jump.started -= OnJump;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            movementInput = Vector2.zero;
            Debug.Log($"Movement Input: 0");
            return;
        }
        movementInput = context.ReadValue<Vector2>();
        Debug.Log($"Movement Input: {movementInput}");
    }

    void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!rb) return;

        if (jumpCount != maxJumps)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumpCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb) return;

        Vector2 moveWithSpeed = movementInput * speed * Time.deltaTime;

        rb.linearVelocity = new Vector3(moveWithSpeed.x, rb.linearVelocity.y, moveWithSpeed.y);
    }
}
