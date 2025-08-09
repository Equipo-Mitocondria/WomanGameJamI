using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private Work _work;

    public void Interact()
    {
        // It would probably change once we have the interaction working properly
        // For early-testing pourposes, it's implemented like this
        _work.IsWorking = !_work.IsWorking;
    }
}
