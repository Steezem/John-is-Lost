using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private bool shouldFaceMoveDirection = false;

    private CharacterController controller;
    private Vector3 moveInput;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // raycast hit is being used to check whether player is on more or less even ground
        // climbing the terrain around the map is being made harder like this
        // Player is not supposed to get outside of the mountain range 
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 3f);
        if (context.performed && controller.isGrounded && hit.normal.x < 0.1 && hit.normal.y > 0.8 && hit.normal.z < 0.1)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed *= 2;
        }
        else if (context.canceled)
        {
            speed /= 2;
        }
    }

    void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
