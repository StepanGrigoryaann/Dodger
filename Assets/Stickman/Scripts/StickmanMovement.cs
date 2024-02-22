using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

[RequireComponent(typeof(Animator))]
public class StickmanMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;


    [SerializeField]
    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;

    private float horizontalInput = 0;
    private float verticalInput = 0;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        OnScreenStick stick;
        stick.
    }

    void Update()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        bool sprint = inputMagnitude > 0.7;

        animator.SetBool("Sprint", sprint);
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            ySpeed = 0f;
        }

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = ySpeed * Time.deltaTime;

        characterController.Move(velocity);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Move(InputAction.CallbackContext callback)
    {
        Vector2 input =  callback.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }
}
