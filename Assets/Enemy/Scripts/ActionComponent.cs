using UnityEngine;

public class ActionComponent : MonoBehaviour, IEnemyTarget
{
    [SerializeField] private ActionType actionType;
    [SerializeField] public Action action;

    [SerializeField] private float beforeDelay;
    [SerializeField] private float afterDelay;

    private void Awake()
    {
        switch (actionType)
        {
            case ActionType.NoAction:
                action = null; break;
            case ActionType.WalkAction:
                action = new WalkAction(beforeDelay, afterDelay, this); break;
        }
        transform.parent = null;
    }

    public Vector3 Position() => transform.position;

    public Action GetAction { get { return action; } }
}

enum ActionType
{
    NoAction = 0,
    WalkAction = 1
}