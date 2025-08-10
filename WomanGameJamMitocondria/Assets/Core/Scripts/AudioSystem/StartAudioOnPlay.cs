using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioOnPlay : MonoBehaviour
{
    [SerializeField] private SoundEffect _soundEffect;
    private AudioSource _aS;

    void Start()
    {
        switch (_soundEffect) 
        {
            case (SoundEffect.Effect):
                _aS = AudioManager.Instance.PlayEffectSound(gameObject);
                break;
            default:
                throw new System.Exception($"Sound effect in {gameObject.name} not set.");
        }      
    }

    private enum SoundEffect
    {
        Effect
    }
}
