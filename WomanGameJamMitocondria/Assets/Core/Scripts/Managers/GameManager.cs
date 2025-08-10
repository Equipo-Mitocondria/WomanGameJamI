using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float _time;
    private SceneManager.Scenes _currentPhase;

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
                EndPlay();
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

    public void GameOver()
    {
        EndPlay();
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
            default:
                throw new Exception($"Unable to find build index for scene {value}.");
        }
    }
}
