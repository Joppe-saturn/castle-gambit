using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindFirstObjectByType<InputManager>();
        }
        return _instance;
    }

    private void OnEnable()
    {
        _toutchPos.Enable();
        _toutchPrimary.Enable();
    }

    private void OnDisable()
    {
        _toutchPos.Disable();
        _toutchPrimary.Disable();
    }

    private void Update()
    {
        ProcesInputs();
    }

    private void ProcesInputs()
    {
        _toutchPosInput = _toutchPos.ReadValue<Vector2>();
        _toutchPrimaryInput = _toutchPrimary.ReadValue<float>();
    }

    [SerializeField] private InputAction _toutchPos;

    private Vector2 _toutchPosInput;
    public Vector2 ToutchPosInput
    {
        get
        {
            return _toutchPosInput;
        }
    }

    [SerializeField] private InputAction _toutchPrimary;

    private float _toutchPrimaryInput;
    public float ToutchPrimaryInput
    {
        get
        {
            return _toutchPrimaryInput;
        }
    }
}
