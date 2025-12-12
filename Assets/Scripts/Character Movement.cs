using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;

    private Vector2 m_moveAmount;
    private Vector2 m_lookAmount;

    private Animator m_animator;
    private Rigidbody m_rigidbody;

    public float WalkSpeed = 5;
    public float JumpSpeed = 5;
    public float RotateSpeed = 5;

    private void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable() {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake() {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        m_moveAmount = m_moveAction.ReadValue<Vector2>();
        m_lookAmount = m_lookAction.ReadValue<Vector2>();
        
        if (m_jumpAction.WasPressedThisFrame()) {
            Jump();
        }
    }

    public void Jump() {
        m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
        m_animator.SetTrigger("Jump");
    }

    private void FixedUpdate() {
        Walking();
        Rotating();
    }

    private void Walking() {
        m_animator.SetFloat("Speed", m_moveAmount.y);
        m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmount.y * WalkSpeed * Time.deltaTime);
    }

    private void Rotating() {
        if (m_moveAmount.y != 0) {
            float rotationAmount = m_lookAmount.x * RotateSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
        }
    }
}
