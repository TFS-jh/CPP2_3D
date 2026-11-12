using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions input;
    Vector2 movementInput;

    Rigidbody rb;

    public float speed = 50f;

    private void OnEnable()
    {
        input = new InputSystem_Actions();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();

        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
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

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb) return;

        Vector2 moveWithSpeed = movementInput * 5f * Time.deltaTime;

        rb.linearVelocity = new Vector3(movementInput.x, rb.linearVelocity.y, movementInput.y);
    }
}
