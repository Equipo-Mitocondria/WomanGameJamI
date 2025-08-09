using UnityEngine;

public class SanityObject : MonoBehaviour
{
    [SerializeField] private SanityEffect _sanityEffect;

    public SanityEffect GetSanityEffect()
    {
        return _sanityEffect;
    }
}
