using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance;

    [SerializeField] private Volume _volume;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetVignetteIntensity(float intensity)
    {
        _volume.sharedProfile.TryGet(out Vignette vignette);
        vignette.intensity.Override(intensity);
    }
}
