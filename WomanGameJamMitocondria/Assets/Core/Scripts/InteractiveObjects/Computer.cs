using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private Work _work;
    [SerializeField] private GameObject _progressPrompt;

    private void Start()
    {
        _progressPrompt.SetActive(false);
    }

    public void Interact()
    {
        // It would probably change once we have the interaction working properly
        // For early-testing pourposes, it's implemented like this
        _work.IsWorking = !_work.IsWorking;
    }
    public void EnterInteractState()
    {
        _progressPrompt.SetActive(true);
    }

    public void ExitInteractState()
    {
        _progressPrompt.SetActive(false);
    }

}
