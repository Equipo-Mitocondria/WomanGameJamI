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
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        CurrentPhase = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    public void EndTask()
    {
        switch (_currentPhase)
        {
            case SceneManager.Scenes.Phase1:
                CurrentPhase = ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Phase2);
                SceneManager.Instance.LoadScene(CurrentPhase);
                break;
            case SceneManager.Scenes.Phase2:
                CurrentPhase = ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Phase3);
                SceneManager.Instance.LoadScene(CurrentPhase);
                break;
            case SceneManager.Scenes.Phase3:
                Win();
                break;
        }
    }

    public void BeginPlay()
    {
        CurrentPhase = ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Phase1);
        SceneManager.Instance.LoadScene(CurrentPhase);
    }

    public void EndPlay()
    {
        CurrentPhase = ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.TitleScreen);
        SceneManager.Instance.LoadScene(CurrentPhase);
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

        CurrentPhase = ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes.Credits);
        SceneManager.Instance.LoadScene(4);
    }

    public void GameOver()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Die();
        EndPlay();
    }

    public IEnumerator FinishCredits()
    {
        yield return new WaitForSeconds(14f);

        SceneManager.Instance.LoadScene(0);
    }

    private SceneManager.Scenes ConvertUnitySceneToSceneManagerScene(int value)
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
            default:
                throw new Exception($"Unable to load scene with build index {value}.");
        }
    }

    private int ConvertSceneManagerSceneToUnityBuildIndex(SceneManager.Scenes value)
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
            default:
                throw new Exception($"Unable to find build index for scene {value}.");
        }
    }
}
