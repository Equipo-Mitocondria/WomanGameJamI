using UnityEngine;

public class PlayGlitchSound : MonoBehaviour
{
    [SerializeField] private SoundEffect _soundEffect;
    private AudioSource _aS;

    void Start()
    {
        _aS = AudioManager.Instance.PlaySoundEffect(_soundEffect, gameObject);
    }

    private void Update()
    {
        if (_aS == null)
            _aS = AudioManager.Instance.PlaySoundEffect(_soundEffect, gameObject);
    }
}
