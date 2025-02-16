using System.Collections;
using UnityEngine;

public class WalkAction : Action
{
    public IEnemyTarget transform;
    [SerializeField] private int test;
    public WalkAction(float beforeDelay, float afterDelay, IEnemyTarget transform)
    {
        this.afterDelay = afterDelay;
        this.beforeDelay = beforeDelay;
        this.transform = transform;
    }
    public override IEnumerator PerformAction(EnemyController controller)
    {
        Debug.Log("PerformActionStart");
        yield return new WaitForSeconds(beforeDelay);
        Debug.Log("PerformAction2");
        controller.SetTarget(transform);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() =>
        controller.NavMeshAgent.remainingDistance <= controller.NavMeshAgent.stoppingDistance);
        Debug.Log("PerformAction3");
        yield return new WaitForSeconds(afterDelay);
        Debug.Log("PerformAction4");
    }
}
