using UnityEngine;

public class Work : MonoBehaviour
{
    [SerializeField] private float _workIncreaseSpeed;
    [SerializeField] private float _maxWork;
    private float _currentWork;

    private bool _isWorking;
    public bool IsWorking {  get { return _isWorking; } set { _isWorking = value; } }

    private void Start()
    {
        _currentWork = 0;
    }

    private void Update()
    {
        if (_isWorking)
        {
            _currentWork += _workIncreaseSpeed * Time.deltaTime;

            if(_currentWork >= _maxWork)
                GameManager.Instance.EndTask();
        }
    }
}
