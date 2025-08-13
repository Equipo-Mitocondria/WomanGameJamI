using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private float _timeWaitForEndPlay;

    private SceneManager.Scenes _currentPhase;
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
        if (_currentPhase == SceneManager.Scenes.Tutorial)
            DialogueManager.Instance.TriggerDialogue(1);
    }

    public void EndTask()
    {
        switch (_currentPhase)
        {
            case SceneManager.Scenes.Tutorial:
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Phase1));
                break;
            case SceneManager.Scenes.Phase1:
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Phase2));
                break;
            case SceneManager.Scenes.Phase2:
                SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Phase3));
                break;
            case SceneManager.Scenes.Phase3:
                Win();
                break;
        }
    }

    public void BeginPlay()
    {
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Tutorial));
    }

    public void EndPlay()
    {
        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.TitleScreen));
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

        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Credits));
    }

    public void GameOver()
    {
        EndPlay();
    }

    public IEnumerator FinishCredits()
    {
        yield return new WaitForSeconds(14f);

        SceneManager.Instance.LoadScene(ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.TitleScreen));
    }

    public SceneManager.Scenes ConvertUnitySceneToSceneManagerScene(int value)
    {
        switch (value) 
        {
            case (0):
                return SceneManager.Scenes.TitleScreen;
            case (1):
                return SceneManager.Scenes.Phase1;
            case (2):
                return SceneManager.Scenes.Phase2;
            case (3):
                return SceneManager.Scenes.Phase3;
            case (4):
                return SceneManager.Scenes.Credits;
            case (5):
                return SceneManager.Scenes.Tutorial;
            default:
                throw new Exception($"Unable to load scene with build index {value}.");
        }
    }

    public int ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes value)
    {
        switch (value)
        {
            case SceneManager.Scenes.TitleScreen:
                return 0;
            case SceneManager.Scenes.Phase1:
                return 1;
            case SceneManager.Scenes.Phase2:
                return 2;
            case SceneManager.Scenes.Phase3:
                return 3;
            case SceneManager.Scenes.Credits:
                return 4;
            case SceneManager.Scenes.Tutorial:
                return 5;
            default:
                throw new Exception($"Unable to find build index for scene {value}.");
        }
    }
}
