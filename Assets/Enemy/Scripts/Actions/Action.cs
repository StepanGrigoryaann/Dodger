using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Action
{
    public UnityEvent OnActionFinished;
    [SerializeField] protected float BeforeDelay;
    [SerializeField] protected float AfterDelay;
    public virtual IEnumerator PerformAction(EnemyController controller)
    {
        yield return new WaitForSeconds(BeforeDelay);
        yield return new WaitForSeconds(AfterDelay);
        OnActionFinished?.Invoke();
    }
}
