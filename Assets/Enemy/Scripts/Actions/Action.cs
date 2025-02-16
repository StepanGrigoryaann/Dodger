using System.Collections;
using UnityEngine;

public abstract class Action
{
    protected float beforeDelay;
    protected float afterDelay;
    public virtual IEnumerator PerformAction(EnemyController controller)
    {
        yield return new WaitForSeconds(beforeDelay);
        yield return new WaitForSeconds(afterDelay);
    }
}
