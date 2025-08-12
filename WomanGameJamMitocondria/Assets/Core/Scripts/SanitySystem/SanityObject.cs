using UnityEngine;

public class SanityObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SanityEffect _sanityEffect;
    [SerializeField] private GameObject _interactionPrompt;
    [SerializeField] private bool _isOneUse;
    [Space]
    [SerializeField] private bool _hasSound;
    [SerializeField] private SoundEffect _soundEffect;
    [Space]
    [SerializeField] private bool _hasDialogue;
    [SerializeField] private int _dialogueID;

    private bool _available = true;

    private void Start()
    {
        _interactionPrompt.SetActive(false);
    }

    public virtual void Interact()
    {
        if (!_available)
            return;
        
        Sanity.Instance.ApplySanityEffect(GetSanityEffect());

        if(_hasSound)
            AudioManager.Instance.PlaySoundEffect(_soundEffect, gameObject);

        if (_hasDialogue)
            DialogueManager.Instance.TriggerDialogue(_dialogueID);

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
