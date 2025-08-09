using UnityEngine;

public class InteractiveObjectsDetector : MonoBehaviour
{
    [SerializeField] private Character _character;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IInteractable>() != null)
            _character.InteractiveObject = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null && _character.InteractiveObject == other.GetComponent<IInteractable>())
            _character.InteractiveObject = null;
    }
}
