using UnityEngine;

public class PlayStepSound : MonoBehaviour
{
    public void PlayStepSFX()
    {
        AudioManager.Instance.PlaySoundEffect(SoundEffect.Step, gameObject);
    }
}
