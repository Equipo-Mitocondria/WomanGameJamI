using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/SettingsDatabase")]
public class SettingsDatabase : ScriptableObject
{
    [SerializeField] private bool _areSoundsEnabled = true;
    [SerializeField] private bool _isMusicEnabled = true;
    [SerializeField] private float _sfxVolumeModifier = 1.0f;
    [SerializeField] private float _musicVolumeModifier = 1.0f;

    public bool AreSFXEnabled { get { return _areSoundsEnabled; } set { _areSoundsEnabled = value; } }
    public bool IsMusicEnabled { get { return _isMusicEnabled; } set { _isMusicEnabled = value; } }
    public float SFXVolumeModifier { get { return _sfxVolumeModifier; } set { _sfxVolumeModifier = value; } }
    public float MusicVolumeModifier { get { return _musicVolumeModifier; } set { _musicVolumeModifier = value; } }
}
