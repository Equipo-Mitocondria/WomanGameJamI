using System;
using System.Collections;
using UnityEngine;

public class Tween : MonoBehaviour
{
    public static Tween Instance;

    private Coroutine _tweenSanityCoroutine; // Like Crash Bandicoot :D
    private float _lastEndValue; // Used only to store hipothetical end value on stopping a coroutine

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TweenSanity(float startValue, float endValue, float duration, Action<float> onUpdate)
    {
        if (_tweenSanityCoroutine != null)
        {
            StopCoroutine(_tweenSanityCoroutine);
            _tweenSanityCoroutine = null;
        }

        _tweenSanityCoroutine = StartCoroutine(TweenNumberCoroutine(startValue, endValue, duration, onUpdate));
    }

    public void TweenVolume(float startValue, float endValue, float duration, Action<float> onUpdate)
    {
        StartCoroutine(TweenNumberCoroutine(startValue, endValue, duration, onUpdate));
    }

    private IEnumerator TweenNumberCoroutine(float startValue, float endValue, float duration, Action<float> onUpdate)
    {
        if (duration <= 0f || startValue == endValue)
        {
            onUpdate?.Invoke(endValue);
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            onUpdate?.Invoke(currentValue);
            yield return null;
        }

        onUpdate?.Invoke(endValue);
    }
}
