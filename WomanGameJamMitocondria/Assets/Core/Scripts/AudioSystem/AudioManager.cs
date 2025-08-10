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

    public UnityEvent OnMusicLoopPointReached = new UnityEvent();

    //Private Variables
    private List<AudioSource> _aSList = new List<AudioSource>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    #region Play Methods
    public AudioSource PlayEffectSound(GameObject gameObject)
    {
        _db.effectCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.effectVolume);
        return CreateAudioSource(gameObject, _db.effectAC, _db.effectCurrentVolume, _db.effectPitchSwift, _db.effectMinDistance, _db.effectMaxDistance);
    }

    public AudioSource PlayNotificationPopUp()
    {
        //TO DO
        return new AudioSource();
    }
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