using UnityEngine;

public class InteractiveObjectsDetector : MonoBehaviour
{
    [SerializeField] private Character _character;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IInteractable>() != null)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable.EnterInteractState();

            _character.InteractiveObject = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null && _character.InteractiveObject == other.GetComponent<IInteractable>())
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable.ExitInteractState();

            _character.InteractiveObject = null;
        }
    }
}
