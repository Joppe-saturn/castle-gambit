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

    [SerializeField] private bool _developerMode;

    private void OnEnable()
    {
        _toutchPos.Enable();
        _toutchPrimary.Enable();
        _mousePos.Enable();
        _click.Enable();
    }

    private void OnDisable()
    {
        _toutchPos.Disable();
        _toutchPrimary.Disable();
        _mousePos.Disable();
        _click.Disable();
    }

    private void Update()
    {
        ProcesInputs();
    }

    private void ProcesInputs()
    {
        if (_developerMode == false)
        {
            _toutchPosInput = _toutchPos.ReadValue<Vector2>();
            _toutchPrimaryInput = _toutchPrimary.ReadValue<float>();
        }
        else
        {
            _toutchPosInput = _mousePos.ReadValue<Vector2>();
            _toutchPrimaryInput = _click.ReadValue<float>();
        }
    }

    [SerializeField] private InputAction _toutchPos;
    [SerializeField] private InputAction _mousePos;

    private Vector2 _toutchPosInput;
    public Vector2 ToutchPosInput
    {
        get
        {
            return _toutchPosInput;
        }
    }

    [SerializeField] private InputAction _toutchPrimary;
    [SerializeField] private InputAction _click;

    private float _toutchPrimaryInput;
    public float ToutchPrimaryInput
    {
        get
        {
            return _toutchPrimaryInput;
        }
    }
}
