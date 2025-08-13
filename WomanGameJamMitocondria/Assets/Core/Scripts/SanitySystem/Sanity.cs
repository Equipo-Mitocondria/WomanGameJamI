using System.Collections;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    public static Sanity Instance;

    [Header("Sanity Parameters")]
    [SerializeField] private float _startingSanity;
    [SerializeField] private float _maxSanity;
    [SerializeField] private float _sanityTweeningTime;
    [SerializeField] private float _deathCountdownTime;
    [SerializeField] private float _resetCountdownTime;

    private bool _isDying;
    public bool IsDying{ get { return _isDying; } set { _isDying = value; } }

    private float _sanity;
    float _sanityPercentage;
    float _lastSanityPercentage = -1; // Used only to avoid applying changes if there's no change
    private Coroutine _deathCountdownCoroutine;
    private bool _isDeathCountdown = false;
    private bool _continueDeathCountdownCoroutineFlag = false;
    private bool _updateMusic = true;

    private float _deathCountdownRemainingTime;
    private float _deathCountdownRemainingTimeForMuttingMusic;

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
                    Tween.Instance.TweenSanity(_sanity, CapSanityValue(_sanity + sanityEffect.Amount), _sanityTweeningTime, value => _sanity = value);
                break;
            case (SanityChange.Substract):
                if (overTime)
                    _sanity -= sanityEffect.Amount * Time.deltaTime;
                else
                    Tween.Instance.TweenSanity(_sanity, CapSanityValue(_sanity - sanityEffect.Amount), _sanityTweeningTime, value => _sanity = value);
                break;
            case (SanityChange.Set):
                Tween.Instance.TweenSanity(_sanity, CapSanityValue(sanityEffect.Amount), _sanityTweeningTime, value => _sanity = value);
                break;
            default:
                throw new System.Exception("Unable to get SanityChange value.");
        }

        if(_sanity < 0)
            _sanity = 0;
        
        if (_sanity >= _maxSanity)
            _sanity = _maxSanity;
    }

    private float CapSanityValue(float modifiedSanityValue)
    {
        if (modifiedSanityValue < 0)
            return 0;
        else if (modifiedSanityValue > _maxSanity)
            return _maxSanity;
        else
            return modifiedSanityValue;
    }

    private void UpdateSanity()
    {
        UpdateDeathCountdownSFX();

        if (_sanityPercentage == _lastSanityPercentage)
            return;
        else
            _lastSanityPercentage = _sanityPercentage;

        PostProcessingManager.Instance.SetPostProductionIntensity(_sanityPercentage);

        //if (_sanityPercentage == 0f)
        //    return;

        if (_sanityPercentage >= 1f)
        {
            if (!_isDeathCountdown)
            {
                _isDeathCountdown = true;
                AudioManager.Instance.PlaySoundEffect(SoundEffect.Heartbeat);
                AudioManager.Instance.PlaySoundEffect(SoundEffect.EarRing);
                _deathCountdownRemainingTime = _deathCountdownTime;
                _deathCountdownRemainingTimeForMuttingMusic = _deathCountdownTime * .75f;
                _deathCountdownCoroutine = StartCoroutine(DeathCountdown());
            }
        }
        else
        {
            if (_deathCountdownCoroutine != null )
            {
                _isDeathCountdown = false;
                AudioManager.Instance.StopHeartBeat();
                AudioManager.Instance.StopEarRing();
                _continueDeathCountdownCoroutineFlag = false;
                StopCoroutine(_deathCountdownCoroutine);
                _deathCountdownCoroutine = null;
            }

        }
        
        UpdateMusic();
    }

    // Method based on Event

    //private void UpdateMusic()
    //{
    //    if (!_isDeathCountdown)
    //    {
    //        if (_sanityPercentage > 0f && _sanityPercentage <= 0.33f)
    //        {
    //            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Insane2));
    //        }
    //        else if(_sanityPercentage > 0.33f && _sanityPercentage <= 0.66f)
    //            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Insane1));
    //        else if(_sanityPercentage > 0.66f)
    //            AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.Natural));
    //    }
    //    else
    //        AudioManager.Instance.OnMusicLoopPointReached.AddListener(() => AudioManager.Instance.ChangeMusic(MusicPiece.DeathCountdown));
    //}
    
    // Direct volume change method

    private void UpdateMusic()
    {
        if (!_isDeathCountdown && _updateMusic)
        {
            if (_sanityPercentage <= 0.33f)
                AudioManager.Instance.ChangeMusic(MusicPiece.Natural);
            else if(_sanityPercentage > 0.33f && _sanityPercentage <= 0.66f)
                AudioManager.Instance.ChangeMusic(MusicPiece.Insane1);
            else if(_sanityPercentage > 0.66f)
                AudioManager.Instance.ChangeMusic(MusicPiece.Insane2);
        }
    }

    private void UpdateDeathCountdownSFX()
    {
        if (_isDeathCountdown) 
        {
            _deathCountdownRemainingTime -= Time.deltaTime;
            _deathCountdownRemainingTimeForMuttingMusic -= Time.deltaTime;
            AudioManager.Instance.UpdateHeartBeatSpeed(1 - _deathCountdownRemainingTime / _deathCountdownTime);
            AudioManager.Instance.UpdateEarRingVolume(1 - _deathCountdownRemainingTime / _deathCountdownTime);
            AudioManager.Instance.UpdateMusicVolumesBasedOnDeathCountdownPercentage(_deathCountdownRemainingTimeForMuttingMusic / _deathCountdownTime);

            if (_deathCountdownRemainingTime <= 0)
            {
                AudioManager.Instance.UpdateHeartBeatSpeed(1);
                AudioManager.Instance.UpdateEarRingVolume(1);
                AudioManager.Instance.UpdateMusicVolumesBasedOnDeathCountdownPercentage(0);

                _continueDeathCountdownCoroutineFlag = true;
            }
        }
    }

    private IEnumerator DeathCountdown()
    {
        yield return new WaitUntil(() => _continueDeathCountdownCoroutineFlag);

        _isDying = true;
        _updateMusic = false;
        AudioManager.Instance.StopHeartBeat();
        AudioManager.Instance.StopEarRing();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Die();
        //AudioManager.Instance.ChangeMusic(MusicPiece.DefinitiveDeath);
        StartCoroutine(ResetCountdown());
    }

    private IEnumerator ResetCountdown()
    {
        yield return new WaitForSeconds(_resetCountdownTime);
        GameManager.Instance.GameOver();
    }

    public void StopDeathCoroutine()
    {
        if (_deathCountdownCoroutine != null)
        {
            StopCoroutine(_deathCountdownCoroutine);
            _deathCountdownCoroutine = null;
        }
    }
}
