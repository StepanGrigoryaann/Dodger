using UnityEngine;

public class ActionComponent : MonoBehaviour, IEnemyTarget
{
    [SerializeField] private ActionType actionType;
    [SerializeField] private Action action;
    private void OnValidate()
    {
        switch (actionType)
        {
            case ActionType.NoAction:
                action = new Action(); break;
            case ActionType.WalkAction:
                action = new WalkAction(this); break;
        }
    }
    private void Awake()
    {
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