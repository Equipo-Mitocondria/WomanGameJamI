using UnityEngine;

public class Character : FSMTemplateMachine
{
    //States
    public WorkingState workingState;
    public ExploringState exploringState;

    [SerializeField] private float _movementForce;
    [SerializeField] private float _maxVelocity;
    [Space]
    [SerializeField] private bool _startsWorking;

    private Work _work;

    private Rigidbody _rigidbody;
    private IInteractable _interactiveObject;

    public bool IsWorking { get { return _work.IsWorking; } set { _work.IsWorking = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public float MovementForce { get { return _movementForce; } }
    public float MaxVelocity { get { return _maxVelocity; } }
    public IInteractable InteractiveObject { get { return _interactiveObject; } set { _interactiveObject = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        _work = GetComponent<Work>();
        _rigidbody = GetComponent<Rigidbody>();

        _interactiveObject = null;
        //Initialize states (this must be done before initializing the FSM)
        workingState = new WorkingState(this);
        exploringState = new ExploringState(this);

        // We must call base.Start() to initalize the FSM
        base.Start();
    }

    protected override void GetInitialState(out FSMTemplateState initialState)
    {
        if(_startsWorking)
            initialState = workingState;
        else
            initialState = exploringState;
    }

    //private void Update()
    //{
    //    Debug.Log(_currentState);
    //    //Debug.Log($"Work: {_work.CurrentWorkAmount}\nSanity: {_sanity.CurrentSanityAmount}");
    //}
}
