using System.Collections;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    public static Sanity Instance;

    [Header("Sanity Parameters")]
    [SerializeField] private float _startingSanity;
    [SerializeField] private float _maxSanity;
    [SerializeField] private float _deathCountdownTime;
    [SerializeField] private float _resetCountdownTime;

    [Header("Music Levels")]
    private float _naturalToInsane1;
    private float _insane1ToInsane2;
    private float _insane2ToDeathCountdown;
    private float _deathCountdownToDefinitiveDeath;

    private bool _isDying;
    public bool IsDying{ get { return _isDying; } set { _isDying = value; } }

    private float _sanity;
    float _sanityPercentage;
    private AudioSource _aS;
    private Coroutine _deathCountdownCoroutine;
    private bool _isDeathCountdown = false;

    public float CurrentSanityAmount { get { return _sanity; } }
    public float SanityPercentage { get { return _sanityPercentage; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _sanity = _startingSanity;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _deathCountdownCoroutine = null;
    }

    private void Update()
    {
        if (_maxSanity == 0)
            return;

        _sanityPercentage = _sanity / _maxSanity;

        UpdateSanity();
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
        
        if (_sanity >= _maxSanity)
            _sanity = _maxSanity;
    }

    private void UpdateSanity()
    {
        UIManager.Instance.UpdateSanityProgress(_sanity.ToString("0.00"));
        PostProcessingManager.Instance.SetVignetteIntensity(_sanityPercentage);

        if (_sanityPercentage == 0f)
            return;

        if (_sanityPercentage >= 1f)
        {
            if (!_isDeathCountdown)
            {
                _isDeathCountdown = true;
                _deathCountdownCoroutine = StartCoroutine(DeathCountdown());
            }
        }
        else
        {
            if (_deathCountdownCoroutine != null )
            {
                _isDeathCountdown = false;
                StopCoroutine(_deathCountdownCoroutine);
                _deathCountdownCoroutine = null;
            }

            //UpdateMusic();
        }
    }

    private void UpdateMusic()
    {
        if (_sanityPercentage > 0f && _sanityPercentage <= 0.33f)
        {
            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Insane2, _aS));
        }
        else if(_sanityPercentage > 0.33f && _sanityPercentage <= 0.66f)
            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Insane1, _aS));
        else if(_sanityPercentage > 0.66f)
            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Natural, _aS));
    }

    private IEnumerator DeathCountdown()
    {
        yield return new WaitForSeconds(_deathCountdownTime);

        _isDying = true;
        //AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.DefinitiveDeath, _aS));
        StartCoroutine(ResetCountdown());
    }

    private IEnumerator ResetCountdown()
    {
        yield return new WaitForSeconds(_resetCountdownTime);
        GameManager.Instance.GameOver();
    }
}
