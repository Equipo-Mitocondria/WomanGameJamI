using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    [Header("Effect")]
    public AudioClip effectAC;
    [Range(0, 1)] public float effectVolume;
    public float effectMinDistance;
    public float effectMaxDistance;
    [Range(0, 1)] public float effectPitchSwift;
    [NonSerialized] public float effectCurrentVolume;


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