using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance;

    [SerializeField] private Volume _volume;

    [Header("Vignette")]
    [SerializeField] private float _minVignette;
    [SerializeField] private float _maxVignette;

    [Header("Color Adjustment")]
    [SerializeField] private float _minColorAdjustment;
    [SerializeField] private float _maxColorAdjustment;

    [Header("Chromatic Aberration")]
    [SerializeField] private float _minChromaticAberration;
    [SerializeField] private float _maxChromaticAberration;

    [Header("Lens Distortion")]
    [SerializeField] private float _minLensDistortion;
    [SerializeField] private float _maxLensDistortion;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetPostProductionIntensity(float intensity)
    {
        SetVignetteIntensity(intensity);
        SetColorAdjustmentSaturation(intensity * 100);
        SetChromaticAberrationIntensity(intensity);
        SetLensDistortionIntensity(intensity);
    }

    private void SetVignetteIntensity(float intensity)
    {
        float intensityScaling = _maxVignette - _minVignette;
        float intensityScaled = intensity * intensityScaling + _minVignette;

        _volume.sharedProfile.TryGet(out Vignette vignette);
        vignette.intensity.Override(intensityScaled);
    }

    private void SetColorAdjustmentSaturation(float intensity)
    {
        float intensityScaling = _maxColorAdjustment - _minColorAdjustment;
        float intensityScaled = intensity * intensityScaling + _minColorAdjustment;

        _volume.sharedProfile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.Override(intensityScaled);
    }

    private void SetChromaticAberrationIntensity(float intensity)
    {
        float intensityScaling = _maxChromaticAberration - _minChromaticAberration;
        float intensityScaled = intensity * intensityScaling + _minChromaticAberration;

        _volume.sharedProfile.TryGet(out ChromaticAberration chromaticAberration);
        chromaticAberration.intensity.Override(intensityScaled);
    }

    private void SetLensDistortionIntensity(float intensity)
    {
        float intensityScaling = _maxLensDistortion - _minLensDistortion;
        float intensityScaled = intensity * intensityScaling + _minLensDistortion;

        _volume.sharedProfile.TryGet(out LensDistortion lensDistortion);
        lensDistortion.intensity.Override(intensityScaled);
    }

}
