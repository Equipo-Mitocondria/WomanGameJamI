using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private RenderTexture _targetTextureForVideo;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Canvas _canvas;

    [Header("Title Screen")]
    [SerializeField] private GameObject _titlePrefab;
    [Space]
    [SerializeField] private VideoClip _titleGoodClip;
    [SerializeField] private Color _titleGoodColor;
    [Space]
    [SerializeField] private VideoClip _titleBadClip;
    [SerializeField] private Color _titleBadColor;

    [Header("Credits")]
    [SerializeField] private GameObject _creditsPrefab;
    [SerializeField] private VideoClip _creditsClip;

    [Header("Thanks For Playing")]
    [SerializeField] private GameObject _thanksForPlayingPrefab;
    [SerializeField] private VideoClip _thanksForPlayingClip;

    [Header("Death")]
    [SerializeField] private GameObject _deathPrefab;
    [SerializeField] private VideoClip _deathClip;

    [Header("Victory")]
    [SerializeField] private GameObject _victoryPrefab;
    [SerializeField] private VideoClip _victoryClip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _videoPlayer.targetTexture = _targetTextureForVideo;

        switch (GameManager.Instance.CurrentAnimation)
        {
            case GameManager.Animations.TitleGood:
                GameObject titleScreenGood = Instantiate(_titlePrefab, _canvas.transform);
                _videoPlayer.clip = _titleGoodClip;
                titleScreenGood.GetComponentInChildren<RawImage>().texture = _targetTextureForVideo;

                TMP_Text[] textBoxesGood = titleScreenGood.GetComponentsInChildren<TMP_Text>();
                foreach(TMP_Text textBox in textBoxesGood)
                {
                    textBox.color = _titleGoodColor;
                }

                break;
            case GameManager.Animations.TitleBad:
                GameObject titleScreenBad = Instantiate(_titlePrefab, _canvas.transform);
                _videoPlayer.clip = _titleBadClip;
                titleScreenBad.GetComponentInChildren<RawImage>().texture = _targetTextureForVideo;

                TMP_Text[] textBoxesBad = titleScreenBad.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text textBox in textBoxesBad)
                {
                    textBox.color = _titleBadColor;
                }

                break;
            case GameManager.Animations.Credits:
                GameObject creditsScreen = Instantiate(_creditsPrefab, _canvas.transform);
                _videoPlayer.clip = _creditsClip;
                _videoPlayer.isLooping = false;
                creditsScreen.GetComponentInChildren<RawImage>().texture = _targetTextureForVideo;
                break;
            case GameManager.Animations.ThanksForPlaying:
                GameObject thanksForPlayingScreen = Instantiate(_thanksForPlayingPrefab, _canvas.transform);
                _videoPlayer.clip = _thanksForPlayingClip;
                _videoPlayer.isLooping = false;
                thanksForPlayingScreen.GetComponent<RawImage>().texture = _targetTextureForVideo;
                break;
            case GameManager.Animations.Death:
                GameObject deathScreen = Instantiate(_deathPrefab, _canvas.transform);
                _videoPlayer.clip = _deathClip;
                _videoPlayer.isLooping = false;
                deathScreen.GetComponentInChildren<RawImage>().texture = _targetTextureForVideo;
                break;
            case GameManager.Animations.Victory:
                GameObject winScreen = Instantiate(_victoryPrefab, _canvas.transform);
                _videoPlayer.clip = _victoryClip;
                winScreen.GetComponentInChildren<RawImage>().texture = _targetTextureForVideo;
                break;
        }
    }
}
