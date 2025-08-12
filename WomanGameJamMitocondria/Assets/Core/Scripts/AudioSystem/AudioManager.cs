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

    //References
    [SerializeField] private AudioDatabase _db;
    [SerializeField] private GameObject _audioListenerGO;

    [NonSerialized] public UnityEvent OnMusicLoopPointReached = new UnityEvent();

    //Private Variables
    private List<AudioSource> _aSList = new List<AudioSource>();

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
            case SoundEffect.WaterStream:
                return PlayWaterStreamSound(gameObject);
            case SoundEffect.Tap:
                return PlayTapSound(gameObject);
            case SoundEffect.Plant:
                return PlayPlantSound(gameObject);
            case SoundEffect.Fridge:
                return PlayFridgeSound(gameObject);
            case SoundEffect.Glitch:
                return PlayGlitchSound(gameObject);
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

    private AudioSource PlayWaterStreamSound(GameObject gameObject)
    {
        _db.waterStreamCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.waterStreamVolume);
        return CreateAudioSource(gameObject, _db.waterStreamAC, _db.waterStreamCurrentVolume, _db.waterStreamPitchSwift, _db.waterStreamMinDistance, _db.waterStreamMaxDistance);
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

    public AudioSource PlayNotificationPopUp()
    {
        //TO DO
        return new AudioSource();
    }
    #endregion
    
    #endregion

    #region Music
    public AudioSource PlayMusic(GameObject gameObject)
    {
        _db.musicCurrentVolume = ChangeMusicVolumeAsPerModifier(_db.musicVolume);
        return CreateMusicAudioSource(_db.naturalMusicAC, _db.musicCurrentVolume, _db.musicMinDistance, _db.musicMaxDistance);
    }
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

    public void ChangeMusic(MusicPiece musicPiece, AudioSource aS)
    {
        switch (musicPiece)
        {
            case MusicPiece.Natural:
                aS.clip = _db.naturalMusicAC;
                break;
            case MusicPiece.Insane1:
                aS.clip = _db.insane1MusicAC;
                break;
            case MusicPiece.Insane2:
                aS.clip = _db.insane2MusicAC;
                break;
            case MusicPiece.DeathCountdown:
                aS.clip = _db.deathCountdownMusicAC;
                break;
            case MusicPiece.DefinitiveDeath:
                aS.clip = _db.definitiveDeathMusicAC;
                break;
            default:
                throw new Exception($"Cannot find {musicPiece.ToString()} music piece.");
        }
    }

    private AudioSource CreateMusicAudioSource(AudioClip audioclip, float volume, float minDistance, float maxDistance)
    {
        AudioSource aS = gameObject.AddComponent<AudioSource>();

        aS.clip = audioclip;
        aS.ignoreListenerPause = true;
        aS.volume = volume;
        aS.minDistance = minDistance;
        aS.maxDistance = maxDistance;
        aS.loop = true;
        aS.Play();
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
    #endregion
}