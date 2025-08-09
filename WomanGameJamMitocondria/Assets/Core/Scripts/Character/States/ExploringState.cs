using UnityEngine;
using UnityEngine.InputSystem;

public class ExploringState : FSMTemplateState
{
    private InputActions _inputActions;
    private InputAction _movement;

    private Rigidbody _rb;

    private Vector2 _movementV2;

    public ExploringState(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        _rb = ((Character)_fsm).Rigidbody;
        _inputActions = InputManager.Instance.InputActions;

        _movement = _inputActions.Exploring.Move;
        _inputActions.Exploring.Interact.performed += Interact;

        _inputActions.Exploring.Enable();
    }

    public override void UpdateLogic() { }

    public override void UpdatePhysics() 
    {
        Move();
    }

    public override void Exit()
    {
        _inputActions.Exploring.Disable();

        _inputActions.Exploring.Interact.performed -= Interact;
        _movement = null;

        _inputActions = null;
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (((Character)_fsm).InteractiveObject != null)
            ((Character)_fsm).InteractiveObject.Interact();
    }

    private void Move()
    {
        _movementV2 = _movement.ReadValue<Vector2>();

        _rb.AddForce(CameraManager.Instance.ActiveCamera.transform.forward * ((Character)_fsm).MovementForce, ForceMode.Impulse);

        CapVelocity(_rb);
    }

    private void CapVelocity(Rigidbody rb)
    {
        if (rb.linearVelocity.magnitude > ((Character)_fsm).MaxVelocity)
        {
            rb.linearVelocity.Normalize();
            rb.linearVelocity *= ((Character)_fsm).MaxVelocity;
        }
        else if(rb.linearVelocity.magnitude < Mathf.Epsilon)
            rb.linearVelocity = Vector3.zero;
    }
}
