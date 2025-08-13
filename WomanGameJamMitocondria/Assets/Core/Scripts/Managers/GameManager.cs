using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Scenes { TitleScreen = 0, Phase1 = 1, Phase2 = 2, Phase3 = 3, Credits = 4, Tutorial = 5, TitleScreenBad = 6 }

    public static GameManager Instance;
    
    [SerializeField] private float _timeWaitForEndPlay;
    [Space]
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _deathScreenPrefab;

    private Scenes _currentPhase;
    private float _time;
    private bool _hasWin = false;

    public float Time {  get { return _time; } }
    public int CurrentPhase { get { return ConvertSceneManagerSceneToUnityBuildIndex(_currentPhase); } set { _currentPhase = ConvertUnitySceneToSceneManagerScene(value); } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        CurrentPhase = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        if (_currentPhase == Scenes.Tutorial)
            DialogueManager.Instance.TriggerDialogue(1);
    }

    public void EndTask()
    {
        switch (_currentPhase)
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
                Win();
                break;
        }
    }

    public void Continue()
    {
        SceneManager.Instance.LoadScene(CurrentPhase);
    }

    public void BeginPlay()
    {
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Tutorial));
    }

    public void EndPlay()
    {
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.TitleScreenBad));
    }

    public void Win()
    {
        if (!_hasWin)
        {
            _hasWin = true;

            NotificationsManager.Instance.NotificationSpawnWithID(0);
            NotificationsManager.Instance.StopNotificationCoroutines();

            Sanity.Instance.StopDeathCoroutine();

            StartCoroutine(WaitForEndPlay());
        }
    }

    private IEnumerator WaitForEndPlay()
    {
        yield return new WaitForSeconds(_timeWaitForEndPlay);

        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.Credits));
    }

    public void DeathScreen()
    {
        _mainCanvas.gameObject.SetActive(false);
        NotificationsManager.Instance.gameObject.SetActive(false);
        Instantiate(_deathScreenPrefab);
    }

    public void GameOver()
    {
        EndPlay();
    }

    public IEnumerator FinishCredits()
    {
        yield return new WaitForSeconds(14f);

        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(Scenes.TitleScreenBad));
    }

    public Scenes ConvertUnitySceneToSceneManagerScene(int value)
    {
        switch (value) 
        {
            case (0):
                return Scenes.TitleScreen;
            case (1):
                return Scenes.Phase1;
            case (2):
                return Scenes.Phase2;
            case (3):
                return Scenes.Phase3;
            case (4):
                return Scenes.Credits;
            case (5):
                return Scenes.Tutorial;
            case (6):
                return Scenes.TitleScreenBad;
            default:
                throw new Exception($"Unable to load scene with build index {value}.");
        }
    }

    public int ConvertSceneManagerSceneToUnityBuildIndex(Scenes value)
    {
        switch (value)
        {
            case Scenes.TitleScreen:
                return 0;
            case Scenes.Phase1:
                return 1;
            case Scenes.Phase2:
                return 2;
            case Scenes.Phase3:
                return 3;
            case Scenes.Credits:
                return 4;
            case Scenes.Tutorial:
                return 5;
            case Scenes.TitleScreenBad:
                return 6;
            default:
                throw new Exception($"Unable to find build index for scene {value}.");
        }
    }
}
