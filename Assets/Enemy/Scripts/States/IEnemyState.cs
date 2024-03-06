using UnityEngine.Events;

public interface IEnemyState
{
    void Start();

    void Update();

    void OnAnimatorMove();
}