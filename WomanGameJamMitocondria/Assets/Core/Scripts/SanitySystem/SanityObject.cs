using UnityEngine;

public class SanityObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SanityEffect _sanityEffect;
    [SerializeField] private GameObject _interactionPrompt;
    [SerializeField] private bool _isOneUse;

    private bool _available = true;

    private void Start()
    {
        _interactionPrompt.SetActive(false);
    }

    public virtual void Interact()
    {
        if(_available)
            Sanity.Instance.ApplySanityEffect(GetSanityEffect());

        if (_isOneUse)
        {
            _available = false;
            _interactionPrompt.SetActive(false);
        }
    }

    private SanityEffect GetSanityEffect()
    {
        return _sanityEffect;
    }
    public void EnterInteractState()
    {
        if(_available)
            _interactionPrompt.SetActive(true);
    }

    public void ExitInteractState()
    {
        _interactionPrompt.SetActive(false);
    }
}
