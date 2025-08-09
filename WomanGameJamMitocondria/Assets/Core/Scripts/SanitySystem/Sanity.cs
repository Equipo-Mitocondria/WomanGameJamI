using UnityEngine;

public class Sanity : MonoBehaviour
{
    private float _sanity;
    [SerializeField] private float _startingSanity;
    [SerializeField] private float _maxSanity;

    public void ApplySanityEffect(SanityEffect sanityEffect)
    {
        switch (sanityEffect.Effect)
        {
            case (SanityChange.Add):
                _sanity += sanityEffect.Amount;
                break;
            case (SanityChange.Substract):
                _sanity -= -sanityEffect.Amount;
                break;
            case (SanityChange.Set):
                _sanity = sanityEffect.Amount;
                break;
            default:
                throw new System.Exception("Unable to get SanityChange value.");
        }

        if(_sanity < 0)
            _sanity = 0;
        else if (_sanity >= _maxSanity)
        {
            _sanity = _maxSanity;
            GameManager.Instance.GameOver();
        }
    }
}
