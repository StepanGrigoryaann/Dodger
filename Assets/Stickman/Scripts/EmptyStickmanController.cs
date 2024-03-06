using UnityEngine;

[RequireComponent(typeof(StickmanMovement))]
public class EmptyStickmanController : MonoBehaviour, IStickmanController
{
    private StickmanMovement stickmanMovement;

    private void Awake()
    {
        stickmanMovement = GetComponent<StickmanMovement>();
        stickmanMovement.SetupController(this);
    }
    void IStickmanController.OnAnimatorMove()
    {
        
    }
    void IStickmanController.Update()
    {
        
    }
}
