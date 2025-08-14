using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] protected Work _work;
    [SerializeField] private GameObject _interactionPrompt;

    private void Start()
    {
        _interactionPrompt.SetActive(false);
    }

    public virtual void Interact()
    {
        // It would probably change once we have the interaction working properly
        // For early-testing pourposes, it's implemented like this
        _work.IsWorking = !_work.IsWorking;
        _interactionPrompt.SetActive(!_work.IsWorking);
    }
    public void EnterInteractState()
    {
        _interactionPrompt.SetActive(true);
    }

    public void ExitInteractState()
    {
        _interactionPrompt.SetActive(false);
    }

}
