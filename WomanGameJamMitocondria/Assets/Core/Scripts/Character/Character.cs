using UnityEngine;

public class Character : FSMTemplateMachine
{
    //States
    public WorkingState workingState;
    public ExploringState exploringState;
    public DyingState dyingState;

    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Parameters")]
    [SerializeField] private float _movementForce;
    [SerializeField] private float _maxVelocity;
    [Space]
    [SerializeField] private Vector3 _workingPosition;
    [SerializeField] private bool _startsWorking;

    private Work _work;
    private Sanity _sanity;

    private Rigidbody _rigidbody;
    private IInteractable _interactiveObject;
    private Vector3 _lastPositionBeforeWork;
    private Quaternion _lastRotationBeforeWork;

    public bool IsWorking { get { return _work.IsWorking; } set { _work.IsWorking = value; } }
    public bool IsDying { get { return _sanity.IsDying; } set { _sanity.IsDying = value; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public Animator Animator { get { return _animator; } }
    public float MovementForce { get { return _movementForce; } }
    public float MaxVelocity { get { return _maxVelocity; } }
    public IInteractable InteractiveObject { get { return _interactiveObject; } set { _interactiveObject = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        _work = GetComponent<Work>();
        _sanity= GetComponent<Sanity>();
        _rigidbody = GetComponent<Rigidbody>();

        _interactiveObject = null;
        //Initialize states (this must be done before initializing the FSM)
        workingState = new WorkingState(this);
        exploringState = new ExploringState(this);
        dyingState = new DyingState(this);

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

    public void MoveToWorkingPosition()
    {
        _lastPositionBeforeWork = transform.position;
        _lastRotationBeforeWork = transform.rotation;
        transform.position = _workingPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void MoveToPreWorkingPosition()
    {
        transform.position = _lastPositionBeforeWork;
        transform.rotation = _lastRotationBeforeWork;
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
    }
}
