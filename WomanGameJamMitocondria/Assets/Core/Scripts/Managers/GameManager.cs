using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Scenes { Animation = 0, Phase1 = 1, Phase2 = 2, Phase3 = 3, Tutorial = 4 }
    public enum Animations { TitleGood = 0, TitleBad = 1, Credits = 2, ThanksForPlaying = 3, Death = 4, Victory = 5 }

    public static GameManager Instance;
    public float Time {  get { return _time; } }
    public Scenes CurrentPhase { get { return _continuePhase; } set { _continuePhase = value; } }
    public Scenes ContinuePhase { get { return _currentPhase; } set { _currentPhase = value; } }
    public Animations CurrentAnimation { get { return _currentAnimation; } set { _currentAnimation = value; } }
    
    [SerializeField] private float _timeWaitForEndPlay;
    [Space]
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _player;

    private Scenes _currentPhase;
    private Scenes _continuePhase;
    private Animations _currentAnimation;
    private float _time;
    private bool _hasWin = false;

    private Coroutine _deathCountdownCoroutine;
    private bool _continueDeathCountdownCoroutineFlag = false;
    public bool ContinueDeathCountdownCoroutineFlag { get { return _continueDeathCountdownCoroutineFlag; } set { _continueDeathCountdownCoroutineFlag = value; } }

    [SerializeField] private float _deathCountdownTime = 10f;
    public float DeathCountdownTime { get { return _deathCountdownTime; } }

    [SerializeField] private float _resetCountdownTime = 2f;
    public float ResetCountdownTime { get { return _resetCountdownTime; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);

        _currentPhase = ConvertUnitySceneToSceneManagerScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        _continuePhase = _currentPhase;

        _currentAnimation = Animations.TitleGood;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentPhase = ConvertUnitySceneToSceneManagerScene(scene.buildIndex);

        if (_currentPhase == Scenes.Tutorial)
        {
            DialogueManager.Instance.TriggerDialogue(1);
            _continuePhase = Scenes.Tutorial;
        }
    }

    public void EndTask()
    {
        if (_currentPhase == Scenes.Phase3)
            Win();
        else
            ShowVictoryScreen();
    }

    public void NextPhase()
    {
        switch (_continuePhase)
        {
            case Scenes.Tutorial:
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Phase1));
                break;
            case Scenes.Phase1:
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Phase2));
                break;
            case Scenes.Phase2:
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Phase3));
                break;
            case Scenes.Phase3:
                _currentAnimation = Animations.ThanksForPlaying;
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Animation));
                break;
        }
    }

    public void Continue()
    {
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(_continuePhase));
    }

    public void BeginTutorial()
    {
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Tutorial));
    }

    public void ReturnToTitleScreen()
    {
        _currentAnimation = Animations.TitleBad;
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Animation));
    }

    public void Win()
    {
        if (!_hasWin)
        {
            _hasWin = true;

            NotificationsManager.Instance.NotificationSpawnWithID(0);
            NotificationsManager.Instance.StopNotificationCoroutines();

            GameObject.FindAnyObjectByType<Character>().IsWorking = false;
            StopDeath();

            StartCoroutine(WaitForEndPlay());
        }
    }

    private IEnumerator WaitForEndPlay()
    {
        yield return new WaitForSeconds(_timeWaitForEndPlay);

        ShowVictoryScreen();
    }

    private void ShowVictoryScreen()
    {
        _continuePhase = _currentPhase;
        _currentPhase = Scenes.Animation;

        _currentAnimation = Animations.Victory;
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Animation));
    }

    public void ShowDeathScreen()
    {
        _continuePhase = _currentPhase;
        _currentPhase = Scenes.Animation;

        _currentAnimation = Animations.Death;
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Animation));
    }

    public void GameOver()
    {
        ReturnToTitleScreen();
    }

    public IEnumerator FinishCredits()
    {
        yield return new WaitForSeconds(14f);

        _currentAnimation = Animations.TitleBad;
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Animation));
    }

    public IEnumerator FinishThanksForPlaying()
    {
        yield return new WaitForSeconds(6f);

        _currentAnimation = Animations.Credits;
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Animation));
    }

    public Scenes ConvertUnitySceneToSceneManagerScene(int value)
    {
        switch (value) 
        {
            case (0):
                return Scenes.Animation;
            case (1):
                return Scenes.Phase1;
            case (2):
                return Scenes.Phase2;
            case (3):
                return Scenes.Phase3;
            case (4):
                return Scenes.Tutorial;
            default:
                throw new Exception($"Unable to load scene with build index {value}.");
        }
    }

    public int ConvertSceneManagerSceneToUnityBuildIndex(Scenes value)
    {
        switch (value)
        {
            case Scenes.Animation:
                return 0;
            case Scenes.Phase1:
                return 1;
            case Scenes.Phase2:
                return 2;
            case Scenes.Phase3:
                return 3;
            case Scenes.Tutorial:
                return 4;
            default:
                throw new Exception($"Unable to find build index for scene {value}.");
        }
    }

    public int GetCurrentSceneInt()
    {
        return ConvertSceneManagerSceneToUnityBuildIndex(_currentPhase);
    }

    public void StartDeath()
    {
        StartCoroutine(DeathCountdown());
    }

    private IEnumerator DeathCountdown()
    {
        yield return new WaitUntil(() => _continueDeathCountdownCoroutineFlag);

        _player.GetComponent<Character>().IsDead = true;
    }

    public void StartReset()
    {
        StartCoroutine(ResetCountdown());
    }
    public void StopDeath()
    {
        if (_deathCountdownCoroutine != null)
        {
            StopCoroutine(_deathCountdownCoroutine);
            _deathCountdownCoroutine = null;
        }
    }

    private IEnumerator ResetCountdown()
    {
        yield return new WaitForSeconds(_resetCountdownTime);
        GameManager.Instance.ShowDeathScreen();
    }

}
