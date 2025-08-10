using System.Collections;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    public static Sanity Instance;

    [Header("Sanity Parameters")]
    [SerializeField] private float _startingSanity;
    [SerializeField] private float _maxSanity;
    [SerializeField] private float _deathCountdownTime;

    [Header("Music Levels")]
    private float _naturalToInsane1;
    private float _insane1ToInsane2;
    private float _insane2ToDeathCountdown;
    private float _deathCountdownToDefinitiveDeath;

    private float _sanity;
    private AudioSource _aS;
    private Coroutine _deathCountdownCoroutine;

    public float CurrentSanityAmount { get { return _sanity; } }

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

        UIManager.Instance.UpdateSanityProgress(_sanity.ToString("0.00"));
        
        if (_sanity >= _maxSanity)
        {
            _sanity = _maxSanity;
            GameManager.Instance.GameOver();
        }

        //UpdateMusic();
    }

    private void UpdateMusic()
    {
        if (_maxSanity == 0)
            return;

        float sanityPercentage = _sanity / _maxSanity;

        if(sanityPercentage == 0f)
        {
            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.DeathCountdown, _aS));
            StartCoroutine(DeathCountdown());
        }
        else
        {
            if(_deathCountdownCoroutine != null)
                StopCoroutine(_deathCountdownCoroutine);

            if (sanityPercentage > 0f && sanityPercentage <= 0.33f)
            {
                AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Insane2, _aS));
            }
            else if(sanityPercentage > 0.33f && sanityPercentage <= 0.66f)
                AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Insane1, _aS));
            else if(sanityPercentage > 0.66f)
                AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Natural, _aS));
        }
    }

    private IEnumerator DeathCountdown()
    {
        yield return new WaitForSeconds(_deathCountdownTime);
        AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.DefinitiveDeath, _aS));
    }
}
