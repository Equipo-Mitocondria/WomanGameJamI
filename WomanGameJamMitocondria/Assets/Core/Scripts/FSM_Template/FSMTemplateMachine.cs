using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTemplateMachine : MonoBehaviour
{
    protected FSMTemplateState _currentState;

    protected virtual void Start()
    {
        GetInitialState(out _currentState);
        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }

    protected virtual void GetInitialState(out FSMTemplateState initialState)
    {
        initialState = null;
    }

    void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateLogic();
        }
    }

    void LateUpdate()
    {
        if (_currentState != null)
        {
            _currentState.UpdatePhysics();
        }
    }

    public void ChangeState(FSMTemplateState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void OnDisable()
    {
        _currentState.Exit();
    }
}
