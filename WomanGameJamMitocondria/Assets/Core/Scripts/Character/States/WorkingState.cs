using System;
using UnityEngine;

public class WorkingState : FSMTemplateState
{
    private InputActions _inputActions;

    public WorkingState(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter() 
    {
        _inputActions = InputManager.Instance.InputActions;

        _inputActions.Working.Leave.performed += Leave;

        _inputActions.Working.Enable();
    }

    public override void UpdateLogic() 
    {
        if(!((Character)_fsm).IsWorking)
            ((Character)_fsm).ChangeState(((Character)_fsm).exploringState);
    }

    public override void Exit() 
    {
        _inputActions.Working.Disable();

        _inputActions.Working.Leave.performed -= Leave;

        _inputActions = null;
    }

    private void Leave(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        ((Character)_fsm).IsWorking = false;
    }
}
