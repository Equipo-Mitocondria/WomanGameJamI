using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [Header("References")]
    [SerializeField] private AudioDatabase _db;
    [SerializeField] private GameObject _audioListenerGO;

    [Header("Parameters")]
    [SerializeField] private const float _musicTransitionTime = 2f;

    [NonSerialized] public UnityEvent OnMusicLoopPointReached = new UnityEvent();

    //Private Variables
    private List<AudioSource> _aSList = new List<AudioSource>();
    private Dictionary<MusicPiece, AudioSource> _musicDictionary = new Dictionary<MusicPiece, AudioSource>();
    private bool _firstMusicUpdate = true;
    private AudioSource _heartBeatAS;
    private AudioSource _earRingAS;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    #region Play
    public AudioSource PlaySoundEffect(SoundEffect soundEffect, GameObject gameObject = null)
    {
        if (gameObject == null)
            gameObject = _audioListenerGO;

        switch (soundEffect)
        {
            case SoundEffect.Cat:
                return PlayCatSound(gameObject);
            case SoundEffect.Plushie:
                return PlayPlushieSound(gameObject);
            case SoundEffect.Work:
                return PlayWorkSound(gameObject);
            case SoundEffect.Cup:
                return PlayCupSound(gameObject);
            case SoundEffect.PhoneBuzz:
                return PlayPhoneBuzzSound(gameObject);
            case SoundEffect.Notification:
                return PlayNotificationSound(gameObject);
            case SoundEffect.WaterBottle:
                return PlayWaterBottleSound(gameObject);
            case SoundEffect.Shower:
                return PlayShowerSound(gameObject);
            case SoundEffect.ShowerLoop:
                return PlayShowerLoopSound(gameObject);
            case SoundEffect.Tap:
                return PlayTapSound(gameObject);
            case SoundEffect.Plant:
                return PlayPlantSound(gameObject);
            case SoundEffect.Fridge:
                return PlayFridgeSound(gameObject);
            case SoundEffect.Glitch:
                return PlayGlitchSound(gameObject);
            case SoundEffect.Mirror:
                return PlayMirrorSound(gameObject);
            case SoundEffect.Bookshelf:
                return PlayBookshelfSound(gameObject);
            case SoundEffect.FridgeLoop:
                return PlayFridgeLoopSound(gameObject);
            case SoundEffect.Heartbeat:
                return PlayHeartBeatLoopSound(gameObject);
            case SoundEffect.EarRing:
                return PlayEarRingSound(gameObject);
            case SoundEffect.Step:
                return PlayStepSound(gameObject);
            default:
                throw new Exception($"Unable to find {soundEffect.ToString()} audio.");
        }
    }

    #region Individual Play Methods
    private AudioSource PlayCatSound(GameObject gameObject)
    {
        _db.catCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.catVolume);
        return CreateAudioSource(gameObject, _db.catAC, _db.catCurrentVolume, _db.catPitchSwift, _db.catMinDistance, _db.catMaxDistance);
    }

    private AudioSource PlayPlushieSound(GameObject gameObject)
    {
        _db.plushieCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.plushieVolume);
        return CreateAudioSource(gameObject, _db.plushieAC, _db.plushieCurrentVolume, _db.plushiePitchSwift, _db.plushieMinDistance, _db.plushieMaxDistance);
    }

    private AudioSource PlayWorkSound(GameObject gameObject)
    {
        _db.workCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.workVolume);
        return CreateAudioSource(gameObject, _db.workAC, _db.workCurrentVolume, _db.workPitchSwift, _db.workMinDistance, _db.workMaxDistance, true);
    }

    private AudioSource PlayCupSound(GameObject gameObject)
    {
        _db.cupCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.cupVolume);
        return CreateAudioSource(gameObject, _db.cupAC, _db.cupCurrentVolume, _db.cupPitchSwift, _db.cupMinDistance, _db.cupMaxDistance);
    }

    private AudioSource PlayPhoneBuzzSound(GameObject gameObject)
    {
        _db.phoneBuzzCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.phoneBuzzVolume);
        return CreateAudioSource(gameObject, _db.phoneBuzzAC, _db.phoneBuzzCurrentVolume, _db.phoneBuzzPitchSwift, _db.phoneBuzzMinDistance, _db.phoneBuzzMaxDistance);
    }

    private AudioSource PlayNotificationSound(GameObject gameObject)
    {
        _db.notificationCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.notificationVolume);
        return CreateAudioSource(gameObject, _db.notificationAC, _db.notificationCurrentVolume, _db.notificationPitchSwift, _db.notificationMinDistance, _db.notificationMaxDistance);
    }

    private AudioSource PlayWaterBottleSound(GameObject gameObject)
    {
        _db.waterBottleCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.waterBottleVolume);
        return CreateAudioSource(gameObject, _db.waterBottleAC, _db.waterBottleCurrentVolume, _db.waterBottlePitchSwift, _db.waterBottleMinDistance, _db.waterBottleMaxDistance);
    }

    private AudioSource PlayShowerSound(GameObject gameObject)
    {
        _db.showerCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.showerVolume);
        return CreateAudioSource(gameObject, _db.showerAC, _db.showerCurrentVolume, _db.showerPitchSwift, _db.showerMinDistance, _db.showerMaxDistance);
    }

    private AudioSource PlayShowerLoopSound(GameObject gameObject)
    {
        _db.showerLoopCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.showerLoopVolume);
        return CreateAudioSource(gameObject, _db.showerLoopAC, _db.showerLoopCurrentVolume, _db.showerLoopPitchSwift, _db.showerLoopMinDistance, _db.showerLoopMaxDistance, true);
    }

    private AudioSource PlayTapSound(GameObject gameObject)
    {
        _db.tapCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.tapVolume);
        return CreateAudioSource(gameObject, _db.tapAC, _db.tapCurrentVolume, _db.tapPitchSwift, _db.tapMinDistance, _db.tapMaxDistance);
    }

    private AudioSource PlayPlantSound(GameObject gameObject)
    {
        _db.plantCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.plantVolume);
        return CreateAudioSource(gameObject, _db.plantAC, _db.plantCurrentVolume, _db.plantPitchSwift, _db.plantMinDistance, _db.plantMaxDistance);
    }

    private AudioSource PlayFridgeSound(GameObject gameObject)
    {
        _db.fridgeCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.fridgeVolume);
        return CreateAudioSource(gameObject, _db.fridgeAC, _db.fridgeCurrentVolume, _db.fridgePitchSwift, _db.fridgeMinDistance, _db.fridgeMaxDistance);
    }

    private AudioSource PlayGlitchSound(GameObject gameObject)
    {
        _db.glitchCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.glitchVolume);
        return CreateAudioSource(gameObject, _db.glitchAC, _db.glitchCurrentVolume, _db.glitchPitchSwift, _db.glitchMinDistance, _db.glitchMaxDistance);
    }

    private AudioSource PlayMirrorSound(GameObject gameObject)
    {
        _db.mirrorCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.mirrorVolume);
        return CreateAudioSource(gameObject, _db.mirrorAC, _db.mirrorCurrentVolume, _db.mirrorPitchSwift, _db.mirrorMinDistance, _db.mirrorMaxDistance);
    }

    private AudioSource PlayBookshelfSound(GameObject gameObject)
    {
        _db.bookshelfCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.bookshelfVolume);
        return CreateAudioSource(gameObject, _db.bookshelfAC, _db.bookshelfCurrentVolume, _db.bookshelfPitchSwift, _db.bookshelfMinDistance, _db.bookshelfMaxDistance);
    }

    private AudioSource PlayFridgeLoopSound(GameObject gameObject)
    {
        _db.fridgeLoopCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.fridgeLoopVolume);
        return CreateAudioSource(gameObject, _db.fridgeLoopAC, _db.fridgeLoopCurrentVolume, _db.fridgeLoopPitchSwift, _db.fridgeLoopMinDistance, _db.fridgeLoopMaxDistance, true);
    }

    private AudioSource PlayStepSound(GameObject gameObject)
    {
        _db.stepCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.stepVolume);
        return CreateAudioSource(gameObject, _db.stepAC, _db.stepCurrentVolume, _db.stepPitchSwift, _db.stepMinDistance, _db.stepMaxDistance);
    }
    #endregion

    public AudioSource PlayUIHover()
    {
        _db.hoverCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.hoverVolume);
        return CreateAudioSource(gameObject, _db.hoverAC, _db.hoverCurrentVolume, 0, 1, 1);
    }

    public AudioSource PlayUIClick()
    {
        _db.clickCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.clickVolume);
        return CreateAudioSource(gameObject, _db.clickAC, _db.clickCurrentVolume, 0, 1, 1);
    }
    #endregion

    #region Music
    public void PlayMusic(MusicPiece musicPiece, GameObject gameObject)
    {
        AudioSource naturalAS = CreateMusicAudioSource(MusicPiece.Natural, gameObject);
        AudioSource insane1AS = CreateMusicAudioSource(MusicPiece.Insane1, gameObject);
        AudioSource insane2AS = CreateMusicAudioSource(MusicPiece.Insane2, gameObject);
        AudioSource deathCountdownAS = CreateMusicAudioSource(MusicPiece.DeathCountdown, gameObject);
        AudioSource definitiveDeathAS = CreateMusicAudioSource(MusicPiece.DefinitiveDeath, gameObject);
    }
    
    private AudioSource CreateMusicAudioSource(MusicPiece musicPiece, GameObject gameObject)
    {
        _db.musicCurrentVolume = ChangeMusicVolumeAsPerModifier(_db.musicVolume);

        switch (musicPiece)
        {
            case MusicPiece.Natural:
                return CreateMusicAudioSource(_db.naturalMusicAC, _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance, musicPiece, gameObject);
            case MusicPiece.Insane1:
                return CreateMusicAudioSource(_db.insane1MusicAC, _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance, musicPiece, gameObject);
            case MusicPiece.Insane2:
                return CreateMusicAudioSource(_db.insane2MusicAC, _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance, musicPiece, gameObject);
            case MusicPiece.DeathCountdown:
                return CreateMusicAudioSource(_db.deathCountdownMusicAC, _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance, musicPiece, gameObject);
            case MusicPiece.DefinitiveDeath:
                return CreateMusicAudioSource(_db.definitiveDeathMusicAC, _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance, musicPiece, gameObject);
            default:
                throw new Exception($"Cannot play {musicPiece.ToString()} music.");
        }

        //return CreateMusicAudioSource(_db.naturalMusicACs[0], _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance, musicPiece);
        
    }

    public void ChangeMusic(MusicPiece musicPiece, float duration = _musicTransitionTime)
    {
        if (_firstMusicUpdate)
        {
            ChangeMusicAudioSourcesVolumes(musicPiece, 0);
            _firstMusicUpdate = false;
        }
        else
            ChangeMusicAudioSourcesVolumes(musicPiece, _musicTransitionTime);
    }
    #endregion

    #region Death Countdown Sounds
    public void UpdateMusicVolumesBasedOnDeathCountdownPercentage(float percentage)
    {
        foreach (KeyValuePair<MusicPiece, AudioSource> keyPair in _musicDictionary)
        {
            if(keyPair.Value.volume == 0)
                continue;

            keyPair.Value.volume = percentage;
        }
    }

    #region HeartBeat Loop
    private AudioSource PlayHeartBeatLoopSound(GameObject gameObject)
    {
        StopHeartBeat();

        _db.heartBeatLoopCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.heartBeatLoopVolume);
        _heartBeatAS = CreateAudioSource(gameObject, _db.heartBeatLoopAC, _db.heartBeatLoopCurrentVolume, _db.heartBeatLoopPitchSwift, _db.heartBeatLoopMinDistance, _db.heartBeatLoopMaxDistance, true);
        return _heartBeatAS;
    }

    public void UpdateHeartBeatSpeed(float percentage)
    {
        if(_heartBeatAS != null)
            _heartBeatAS.pitch = ConvertHeartBeatPercentageToPitch(percentage);
    }

    public void StopHeartBeat(bool continueUntilEnd = false)
    {
        if (_heartBeatAS != null)
        {
            if(continueUntilEnd)
                StartCoroutine(StopHeartBeatOnAudioEnd());
            else
            {
                StopAudioSource(_heartBeatAS);
                _heartBeatAS = null;
            }
        }
    }

    private float ConvertHeartBeatPercentageToPitch(float percentage)
    {
        if (percentage < 0)
            percentage = 0;
        else if (percentage > 1)
            percentage = 1;
        return Mathf.Lerp(_db.heartBeatMinPitch, _db.heartBeatMaxPitch, percentage);
    }

    private IEnumerator StopHeartBeatOnAudioEnd()
    {
        int lastSample = 0;
        int currentSample = _heartBeatAS.timeSamples;

        while (currentSample <= lastSample)
        {
            lastSample = currentSample;
            yield return null;
        }

        StopAudioSource(_heartBeatAS);
    }
    #endregion

    #region Ear Ring
    private AudioSource PlayEarRingSound(GameObject gameObject)
    {
        StopEarRing();

        _db.earRingCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.earRingVolume);
        _earRingAS = CreateAudioSource(gameObject, _db.earRingAC, _db.earRingCurrentVolume, _db.earRingPitchSwift, _db.earRingMinDistance, _db.earRingMaxDistance, true);
        return _earRingAS;
    }

    public void UpdateEarRingVolume(float percentage)
    {
        if (_earRingAS != null)
            _earRingAS.volume = ConvertEarRingPercentageToVolume(percentage);
    }

    private float ConvertEarRingPercentageToVolume(float percentage)
    {
        if (percentage < 0)
            percentage = 0;
        else if (percentage > 1)
            percentage = 1;
        return Mathf.Lerp(_db.earRingMinVolume, _db.earRingCurrentVolume, percentage);
    }

    public void StopEarRing()
    {
        if (_earRingAS != null)
        {            
            StopAudioSource(_earRingAS);
            _earRingAS = null;
        }
    }
    #endregion
    #endregion


    #region Stop Methods
    public void StopAudioSource(AudioSource aS)
    {
        if (aS != null)
        {
            aS.Stop();
            Destroy(aS);
        }
    }

    public void DestroyAllAudioSourcesOnSceneChange()
    {
        foreach (AudioSource aS in _aSList)
        {
            Destroy(aS);
        }
    }
    #endregion

    #region Private Methods
    private AudioSource CreateAudioSource(GameObject go, AudioClip audioClip, float volume, float pitchSwift = 0, float minDistance = 1, float maxDistance = 500, bool loop = false)
    {
        if(go != null)
        {
            AudioSource aS = go.AddComponent<AudioSource>();
            aS.clip = audioClip;
            aS.volume = volume;
            aS.pitch = GetPitch(pitchSwift);
            aS.minDistance = minDistance;
            aS.maxDistance = maxDistance;
            aS.loop = loop;
            aS.spatialBlend = 1;

            aS.rolloffMode = AudioRolloffMode.Linear;
            
            //AnimationCurve rollOffCurve = new AnimationCurve();
            //rollOffCurve.AddKey(0f, 1f);
            //rollOffCurve.AddKey(minDistance, 1f);
            //rollOffCurve.AddKey(maxDistance, 0f);
            //aS.SetCustomCurve(AudioSourceCurveType.CustomRolloff, rollOffCurve);

            aS.Play();
            _aSList.Add(aS);

            if (!loop)
            {
                StartCoroutine(DestroyAudioSourceWhenFinish(aS));
            }
            return aS;
        }
        return null;
    }

    private AudioSource CreateMusicAudioSource(AudioClip audioclip, float volume, float minDistance, float maxDistance, MusicPiece musicPiece, GameObject go)
    {
        AudioSource aS = go.AddComponent<AudioSource>();

        aS.clip = audioclip;
        aS.ignoreListenerPause = true;
        aS.volume = volume;
        aS.minDistance = minDistance;
        aS.maxDistance = maxDistance;
        aS.loop = true;
        aS.spatialBlend = 1;
        aS.rolloffMode = AudioRolloffMode.Linear;
        aS.Play();

        _musicDictionary.Add(musicPiece, aS);

        return aS;
    }

    IEnumerator CheckMusicLoopPointReached(AudioSource aS)
    {
        int lastSample = 0;
        int currentSample = aS.timeSamples;

        while(currentSample <= lastSample)
        {
            lastSample = currentSample;
            yield return null;
        }

        OnMusicLoopPointReached?.Invoke();
        
        StartCoroutine(CheckMusicLoopPointReached(aS));
    }


    IEnumerator DestroyAudioSourceWhenFinish(AudioSource audioSource)
    {
        bool isPlaying = true;
        while (isPlaying)
        {
            if (audioSource != null)
            {
                if (!audioSource.isPlaying)
                {
                    isPlaying = false;
                }
            }

            yield return null;
        }
        try
        {
            _aSList.Remove(audioSource);
            Destroy(audioSource);
        }
        catch (Exception e)
        {
            Debug.Log(audioSource.name + " cannot be found and cannot be destroyed. " + e.Message);
        }
    }

    private float GetPitch(float pitchSwift)
    {
        float minPitch = 1 - pitchSwift;
        float maxPitch = 1 + pitchSwift;

        return UnityEngine.Random.Range(minPitch, maxPitch);
    }

    private float ChangeSFXVolumeAsPerModifier(float originalVolume)
    {
        if (SettingsManager.Instance.Database.AreSFXEnabled)
            return originalVolume * SettingsManager.Instance.Database.SFXVolumeModifier;
        else
            return 0f;
    }

    private float ChangeMusicVolumeAsPerModifier(float originalVolume)
    {
        if (SettingsManager.Instance.Database.IsMusicEnabled)
            return originalVolume * SettingsManager.Instance.Database.MusicVolumeModifier;
        else
            return 0f;
    }

    private void ChangeMusicAudioSourcesVolumes(MusicPiece musicPiece, float transitionDuration)
    {
        foreach (KeyValuePair<MusicPiece, AudioSource> keyPair in _musicDictionary)
        {
            if (keyPair.Key == musicPiece)
                Tween.Instance.TweenVolume(keyPair.Value.volume, _db.musicCurrentVolume, transitionDuration, value => keyPair.Value.volume = value);
            else
                Tween.Instance.TweenVolume(keyPair.Value.volume, 0, transitionDuration, value => keyPair.Value.volume = value);
        }
    }

    //private void QueueMusic()
    //{
    //    QueueNaturalMusic();
    //    QueueInsane1Music();
    //    QueueInsane2Music();
    //    QueueDeathCountdownMusic();
    //    QueueDefinitiveDeathMusic();
    //}

    //private void QueueNaturalMusic()
    //{
    //    _naturalIndex++;
    //    if(_naturalIndex >= _db.naturalMusicACs.Length)
    //        _naturalIndex = 0;

    //    _musicDictionary[MusicPiece.Natural].clip = _db.naturalMusicACs[_naturalIndex];
    //}

    //private void QueueInsane1Music()
    //{
    //    _insane1Index++;
    //    if (_insane1Index >= _db.insane1MusicACs.Length)
    //        _insane1Index = 0;

    //    _musicDictionary[MusicPiece.Insane1].clip = _db.insane1MusicACs[_insane1Index];
    //}

    //private void QueueInsane2Music()
    //{
    //    _insane2Index++;
    //    if (_insane2Index >= _db.insane2MusicACs.Length)
    //        _insane2Index = 0;

    //    _musicDictionary[MusicPiece.Insane2].clip = _db.insane2MusicACs[_insane2Index];
    //}

    //private void QueueDeathCountdownMusic()
    //{
    //    _deathCountdownIndex++;
    //    if (_deathCountdownIndex >= _db.deathCountdownMusicACs.Length)
    //        _deathCountdownIndex = 0;

    //    _musicDictionary[MusicPiece.DeathCountdown].clip = _db.deathCountdownMusicACs[_deathCountdownIndex];
    //}

    //private void QueueDefinitiveDeathMusic()
    //{
    //    _definitiveDeathIndex++;
    //    if (_definitiveDeathIndex >= _db.definitiveDeathMusicACs.Length)
    //        _definitiveDeathIndex = 0;

    //    _musicDictionary[MusicPiece.DefinitiveDeath].clip = _db.definitiveDeathMusicACs[_definitiveDeathIndex];
    //}
    #endregion
}