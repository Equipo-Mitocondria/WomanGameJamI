using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private InputActions _inputActions;

    public InputActions InputActions { get { return _inputActions; } }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _inputActions = new InputActions();
    }
}
