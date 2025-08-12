using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    [Header("SFX")]
    public AudioClip catAC;
    [Range(0, 1)] public float catVolume;
    public float catMinDistance;
    public float catMaxDistance;
    [Range(0, 1)] public float catPitchSwift;
    [NonSerialized] public float catCurrentVolume;

    [Space]

    public AudioClip plushieAC;
    [Range(0, 1)] public float plushieVolume;
    public float plushieMinDistance;
    public float plushieMaxDistance;
    [Range(0, 1)] public float plushiePitchSwift;
    [NonSerialized] public float plushieCurrentVolume;

    [Space]

    public AudioClip workAC;
    [Range(0, 1)] public float workVolume;
    public float workMinDistance;
    public float workMaxDistance;
    [Range(0, 1)] public float workPitchSwift;
    [NonSerialized] public float workCurrentVolume;

    [Space]

    public AudioClip cupAC;
    [Range(0, 1)] public float cupVolume;
    public float cupMinDistance;
    public float cupMaxDistance;
    [Range(0, 1)] public float cupPitchSwift;
    [NonSerialized] public float cupCurrentVolume;

    [Space]

    public AudioClip phoneBuzzAC;
    [Range(0, 1)] public float phoneBuzzVolume;
    public float phoneBuzzMinDistance;
    public float phoneBuzzMaxDistance;
    [Range(0, 1)] public float phoneBuzzPitchSwift;
    [NonSerialized] public float phoneBuzzCurrentVolume;

    [Space]

    public AudioClip notificationAC;
    [Range(0, 1)] public float notificationVolume;
    public float notificationMinDistance;
    public float notificationMaxDistance;
    [Range(0, 1)] public float notificationPitchSwift;
    [NonSerialized] public float notificationCurrentVolume;

    [Space]

    public AudioClip waterBottleAC;
    [Range(0, 1)] public float waterBottleVolume;
    public float waterBottleMinDistance;
    public float waterBottleMaxDistance;
    [Range(0, 1)] public float waterBottlePitchSwift;
    [NonSerialized] public float waterBottleCurrentVolume;

    [Space]

    public AudioClip waterStreamAC;
    [Range(0, 1)] public float waterStreamVolume;
    public float waterStreamMinDistance;
    public float waterStreamMaxDistance;
    [Range(0, 1)] public float waterStreamPitchSwift;
    [NonSerialized] public float waterStreamCurrentVolume;

    [Space]

    public AudioClip tapAC;
    [Range(0, 1)] public float tapVolume;
    public float tapMinDistance;
    public float tapMaxDistance;
    [Range(0, 1)] public float tapPitchSwift;
    [NonSerialized] public float tapCurrentVolume;

    [Space]

    public AudioClip plantAC;
    [Range(0, 1)] public float plantVolume;
    public float plantMinDistance;
    public float plantMaxDistance;
    [Range(0, 1)] public float plantPitchSwift;
    [NonSerialized] public float plantCurrentVolume;

    [Space]

    public AudioClip fridgeAC;
    [Range(0, 1)] public float fridgeVolume;
    public float fridgeMinDistance;
    public float fridgeMaxDistance;
    [Range(0, 1)] public float fridgePitchSwift;
    [NonSerialized] public float fridgeCurrentVolume;

    [Space]

    public AudioClip glitchAC;
    [Range(0, 1)] public float glitchVolume;
    public float glitchMinDistance;
    public float glitchMaxDistance;
    [Range(0, 1)] public float glitchPitchSwift;
    [NonSerialized] public float glitchCurrentVolume;


    [Header("Music")]
    public AudioClip naturalMusicAC;
    public AudioClip insane1MusicAC;
    public AudioClip insane2MusicAC;
    public AudioClip deathCountdownMusicAC;
    public AudioClip definitiveDeathMusicAC;
    [Range(0, 1)] public float musicVolume;
    public float musicMinDistance;
    public float musicMaxDistance;
    [NonSerialized] public float musicCurrentVolume;
}