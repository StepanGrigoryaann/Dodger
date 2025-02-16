using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IStickmanController))]
public class StickmanMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    public Animator animator;
    private IStickmanController stickmanController;

    private float horizontalInput = 0;
    private float verticalInput = 0;

    private bool isCrouching = false;
    private bool canRun = true;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
    }
    public void SetupController(IStickmanController controller)
    {
        if (stickmanController == null)
            stickmanController = controller;
        else
            Debug.LogError("Stickman controller already setuped");
    }
    void Update()
    {
        stickmanController.Update();
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        bool isRunning = inputMagnitude > 0.7f;
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetBool("IsRunning", isRunning && canRun);
        
        movementDirection.Normalize();

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
        if(!canRun)
        {
            inputMagnitude = 0;
        }
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        stickmanController.OnAnimatorMove();
    }

    public void Move(Vector2 input, bool canRun = true)
    {
        horizontalInput = input.x;
        verticalInput = input.y;
        this.canRun = canRun;
    }

    public void ToggleCrouching()
    {
        isCrouching = !isCrouching;
    }
}