using UnityEngine;

public class Sanity : MonoBehaviour
{
    public static Sanity Instance;

    private float _sanity;
    [SerializeField] private float _startingSanity;
    [SerializeField] private float _maxSanity;

    public float CurrentSanityAmount { get { return _sanity; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ApplySanityEffect(SanityEffect sanityEffect, bool overTime = false)
    {
        switch (sanityEffect.Effect)
        {
            case (SanityChange.Add):
                if(overTime)
                    _sanity += sanityEffect.Amount * Time.deltaTime;
                else
                    _sanity += sanityEffect.Amount;
                break;
            case (SanityChange.Substract):
                if (overTime)
                    _sanity -= sanityEffect.Amount * Time.deltaTime;
                else
                    _sanity -= sanityEffect.Amount;
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
