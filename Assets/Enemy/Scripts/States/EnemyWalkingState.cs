using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkingState : IEnemyState
{
    private EnemyController controller;
    public EnemyWalkingState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void OnAnimatorMove()
    {
        Vector3 rootPosition = controller.Animator.rootPosition;
        rootPosition.y = controller.NavMeshAgent.nextPosition.y;
        controller.transform.position = rootPosition;
        controller.NavMeshAgent.nextPosition = rootPosition;
    }

    public void Start()
    {

    }

    public void Update()
    {
        Vector3 worldDeltaPosition = controller.NavMeshAgent.nextPosition - controller.transform.position;
        worldDeltaPosition.y = worldDeltaPosition.z;
        Vector2 moveInput = worldDeltaPosition;
        if (controller.NavMeshAgent.remainingDistance <= controller.NavMeshAgent.stoppingDistance)
        {
            moveInput = Vector2.Lerp(
                Vector2.zero,
            moveInput,
                controller.NavMeshAgent.remainingDistance / controller.NavMeshAgent.stoppingDistance
            );
        }
        moveInput /= Time.deltaTime;
        Debug.Log(controller.NavMeshAgent.remainingDistance);
        Debug.Log(moveInput);
        controller.StickmanMovement.Move(moveInput, false);
    }
}
