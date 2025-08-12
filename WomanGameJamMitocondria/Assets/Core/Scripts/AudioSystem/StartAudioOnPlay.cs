using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioOnPlay : MonoBehaviour
{
    [SerializeField] private SoundEffect _soundEffect;
    private AudioSource _aS;

    void Start()
    {
        _aS = AudioManager.Instance.PlaySoundEffect(_soundEffect, gameObject);     
    }
}
