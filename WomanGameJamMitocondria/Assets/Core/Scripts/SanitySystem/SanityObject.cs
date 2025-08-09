using UnityEngine;

public class SanityObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SanityEffect _sanityEffect;
    [SerializeField] private Sanity _sanity;

    public virtual void Interact()
    {
        _sanity.ApplySanityEffect(GetSanityEffect());
    }

    private SanityEffect GetSanityEffect()
    {
        return _sanityEffect;
    }
}
