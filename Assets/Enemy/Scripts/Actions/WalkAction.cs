using System.Collections;
using UnityEngine;

public class WalkAction : Action
{
    IEnemyTarget transform;
    public WalkAction(IEnemyTarget transform)
    {
        this.transform = transform;
    }
    public override IEnumerator PerformAction(EnemyController controller)
    {
        yield return new WaitForSeconds(BeforeDelay);
        controller.SetTarget(transform);
        OnActionFinished?.Invoke();
        yield return new WaitForSeconds(AfterDelay);
    }
}
