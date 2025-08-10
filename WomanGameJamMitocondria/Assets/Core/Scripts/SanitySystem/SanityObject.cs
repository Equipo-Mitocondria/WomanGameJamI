using UnityEngine;

public class SanityObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SanityEffect _sanityEffect;
    [SerializeField] private GameObject _interactionPrompt;

    private void Start()
    {
        _interactionPrompt.SetActive(false);
    }

    public virtual void Interact()
    {
        Sanity.Instance.ApplySanityEffect(GetSanityEffect());
    }

    private SanityEffect GetSanityEffect()
    {
        return _sanityEffect;
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
