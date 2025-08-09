using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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

    public override void UpdateLogic() 
    {
        if(((Character)_fsm).IsWorking)
            ((Character)_fsm).ChangeState(((Character)_fsm).workingState);
    }

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

        Vector3 projectedForward = ProjectVectorOnXZPlane(CameraManager.Instance.ActiveCamera.transform.forward);
        Vector3 projectedRight = ProjectVectorOnXZPlane(CameraManager.Instance.ActiveCamera.transform.right);

        RotatePlayer(projectedForward, projectedRight, _movementV2.x, _movementV2.y);

        _rb.AddForce((projectedForward * _movementV2.y + projectedRight * _movementV2.x) * ((Character)_fsm).MovementForce * Time.deltaTime, ForceMode.Impulse);

        CapVelocity(_rb);
    }

    private void CapVelocity(Rigidbody rb)
    {
        if (rb.linearVelocity.magnitude > ((Character)_fsm).MaxVelocity)
            rb.linearVelocity = ((Character)_fsm).MaxVelocity * rb.linearVelocity.normalized;
        else if(rb.linearVelocity.magnitude < Mathf.Epsilon)
            rb.linearVelocity = Vector3.zero;
    }

    private Vector3 ProjectVectorOnXZPlane(Vector3 vector)
    {
        Vector3 vectorProjected = new Vector3(vector.x, 0f, vector.z);
        return vectorProjected.normalized;
    }

    private void RotatePlayer(Vector3 forward, Vector3 right, float horizontalInput, float verticalInput)
    {
        if (!(Mathf.Abs(horizontalInput) > Mathf.Epsilon || Mathf.Abs(verticalInput) > Mathf.Epsilon))
            return;

        // North, South, East & West rotations
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            if (horizontalInput < 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(-right);
            else if (horizontalInput > 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(right);
        }
        else if(Mathf.Abs(horizontalInput) < Mathf.Abs(verticalInput))
        {
            if (verticalInput < 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(-forward);
            else if (verticalInput > 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(forward);
        }
        else if(Mathf.Abs(horizontalInput) == Mathf.Abs(verticalInput))
        {
            //S
            if (verticalInput < 0f)
            {
                //SW
                if(horizontalInput < 0f)
                    _fsm.transform.rotation = Quaternion.LookRotation((-right - forward).normalized);
                else if (horizontalInput > 0f) //SE
                    _fsm.transform.rotation = Quaternion.LookRotation((right - forward).normalized);
            }
            else if(verticalInput > 0f)
            {
                //NW
                if (horizontalInput < 0f)
                    _fsm.transform.rotation = Quaternion.LookRotation((-right + forward).normalized);
                else if (horizontalInput > 0f) //NE
                    _fsm.transform.rotation = Quaternion.LookRotation((right + forward).normalized);
            }
        }
    }
}
