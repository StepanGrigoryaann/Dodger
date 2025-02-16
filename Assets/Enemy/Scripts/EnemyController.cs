using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(StickmanMovement))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IStickmanController
{
    public StickmanMovement StickmanMovement => stickmanMovement;
    private StickmanMovement stickmanMovement;

    public NavMeshAgent NavMeshAgent => navMeshAgent;
    private NavMeshAgent navMeshAgent;

    public Animator Animator => animator;
    private Animator animator;

    [SerializeField] private float walkingStoppingDistance = 0.5f;
    [SerializeField] private float runningStoppingDistance = 1f;
    [SerializeField] private List<ActionComponent> actions = new();

    private IEnemyState currentState;
    private EnemyIdleState enemyIdleState;
    private EnemyWalkingState enemyWalkingState;

    private IEnemyTarget enemyTarget;

    private void OnValidate()
    {
        stickmanMovement ??= GetComponent<StickmanMovement>();
        animator ??= GetComponent<Animator>();
        navMeshAgent ??= GetComponent<NavMeshAgent>();
    }
    
    private void Awake()
    {
        animator.applyRootMotion = true;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updatePosition = false;

        stickmanMovement.SetupController(this);
    }
    private void Start()
    {
        StartCoroutine(DoActions());
        InitializeState();
    }
    private void InitializeState()
    {
        enemyIdleState = new(this);
        enemyWalkingState = new(this);
        SetCurrentState(enemyWalkingState);
    }
    public void SetCurrentState(IEnemyState state)
    {
        currentState = state;
    }
    public void SetTarget(IEnemyTarget target)
    {
        enemyTarget = target;
        navMeshAgent.SetDestination(enemyTarget.Position());
    }
    void IStickmanController.OnAnimatorMove()
    {
        currentState.OnAnimatorMove();
    }
    void IStickmanController.Update()
    {
        currentState.Update();
    }
    IEnumerator DoActions()
    {
        foreach (var action in actions)
        {
            yield return (action.GetAction.PerformAction(this));
            Debug.LogError("FINISHED");
        }
    }
}
