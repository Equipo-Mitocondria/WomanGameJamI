using UnityEngine;

public class SanityObject : MonoBehaviour, IInteractable
{
    [SerializeField] private SanityEffect _sanityEffect;

    public virtual void Interact()
    {
        Sanity.Instance.ApplySanityEffect(GetSanityEffect());
    }

    private SanityEffect GetSanityEffect()
    {
        return _sanityEffect;
    }
}
