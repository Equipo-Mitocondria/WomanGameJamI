using UnityEngine;

public class Work : MonoBehaviour
{
    [SerializeField] private SanityEffect _sanityEffect;
    [SerializeField] private float _workIncreaseSpeed;
    [SerializeField] private float _maxWork;
    private float _currentWork;
    private bool _isWorking;

    public float CurrentWorkAmount { get { return _currentWork; } }
    public bool IsWorking {  get { return _isWorking; } set { _isWorking = value; } }

    private void Start()
    {
        _currentWork = 0;
    }

    private void Update()
    {
        if (_isWorking)
        {
            Sanity.Instance.ApplySanityEffect(_sanityEffect, true);

            _currentWork += _workIncreaseSpeed * Time.deltaTime;

            UIManager.Instance.UpdateWorkProgress(_currentWork.ToString("0.00"));

            if(_currentWork >= _maxWork)
                GameManager.Instance.EndTask();
        }
    }
}
