using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IStickmanController, IEnemyTarget
{
    public StickmanMovement stickmanMovement { get; private set; }
    private CharacterController characterController;

    private Vector3 velocity;
    private void Awake()
    {
        stickmanMovement = GetComponent<StickmanMovement>();
        characterController = GetComponent<CharacterController>();
        stickmanMovement.SetupController(this);
    }
    private void Start()
    {
        stickmanMovement.ToggleCrouching();
    }
    public void Move(InputAction.CallbackContext callback)
    {
        Vector2 input = callback.ReadValue<Vector2>();
        stickmanMovement.Move(input);
    }
    void IStickmanController.OnAnimatorMove()
    {
        velocity = stickmanMovement.animator.deltaPosition;
        float ySpeed = 0.0f;
        if (!characterController.isGrounded)
        {
            ySpeed = characterController.velocity.y +
                Physics.gravity.y * Time.deltaTime;
        }
        velocity.y = ySpeed * Time.deltaTime;
        characterController.Move(velocity);
    }
    void IStickmanController.Update()
    {

    }

    public Vector3 Position() => transform.position;
}
