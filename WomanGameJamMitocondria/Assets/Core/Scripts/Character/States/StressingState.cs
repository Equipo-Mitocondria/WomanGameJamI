using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StressingState : FSMTemplateState
{
    public StressingState(FSMTemplateMachine fsm) : base(fsm) { }

    private InputActions _inputActions;
    private InputAction _movement;

    private Rigidbody _rb;

    private Vector2 _movementV2;

    public override void Enter()
    {
        _rb = ((Character)_fsm).Rigidbody;
        _inputActions = InputManager.Instance.InputActions;

        _movement = _inputActions.Exploring.Move;
        _inputActions.Exploring.Interact.performed += Interact;

        _inputActions.Exploring.Enable();

        AudioManager.Instance.PlaySoundEffect(SoundEffect.Heartbeat);
        AudioManager.Instance.PlaySoundEffect(SoundEffect.EarRing);

        if (((Character)_fsm).Sanity != null)
        {
            ((Character)_fsm).Sanity.IsDeathCountdown = true;
            ((Character)_fsm).Sanity.DeathCountdownRemainingTime = GameManager.Instance.DeathCountdownTime;
            ((Character)_fsm).Sanity.DeathCountdownRemainingTimeForMuttingMusic = GameManager.Instance.DeathCountdownTime * .75f;
        }

        GameManager.Instance.StartDeath();
    }

    public override void UpdateLogic()
    {
        if (((Character)_fsm).Sanity != null)
        {
            if (((Character)_fsm).Sanity.SanityPercentage < 1f)
            {
                ((Character)_fsm).ChangeState(((Character)_fsm).exploringState);
                return;
            }
        }

        if (((Character)_fsm).IsDead)
        {
            ((Character)_fsm).ChangeState(((Character)_fsm).dyingState);
            return;
        }

        if (((Character)_fsm).IsWorking)
            ((Character)_fsm).IsWorking = false;
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

        if (((Character)_fsm).Sanity != null)
        {
            ((Character)_fsm).Sanity.IsDeathCountdown = false;
        }

        AudioManager.Instance.StopHeartBeat();
        AudioManager.Instance.StopEarRing();
        GameManager.Instance.ContinueDeathCountdownCoroutineFlag = false;
        GameManager.Instance.StopDeath();
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
        else if (rb.linearVelocity.magnitude < Mathf.Epsilon)
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
        {
            ((Character)_fsm).Animator.SetBool("isWalking", false);
            return;
        }

        ((Character)_fsm).Animator.SetBool("isWalking", true);

        // North, South, East & West rotations
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            if (horizontalInput < 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(-right);
            else if (horizontalInput > 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(right);
        }
        else if (Mathf.Abs(horizontalInput) < Mathf.Abs(verticalInput))
        {
            if (verticalInput < 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(-forward);
            else if (verticalInput > 0f)
                _fsm.transform.rotation = Quaternion.LookRotation(forward);
        }
        else if (Mathf.Abs(horizontalInput) == Mathf.Abs(verticalInput))
        {
            //S
            if (verticalInput < 0f)
            {
                //SW
                if (horizontalInput < 0f)
                    _fsm.transform.rotation = Quaternion.LookRotation((-right - forward).normalized);
                else if (horizontalInput > 0f) //SE
                    _fsm.transform.rotation = Quaternion.LookRotation((right - forward).normalized);
            }
            else if (verticalInput > 0f)
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
