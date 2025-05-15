using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSelecting : MonoBehaviour
{
    private InputManager _inputManager;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _touchPos;
    private float _touchPress;

    [Header("Gebruikt voor interactions")]
    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
    }

    private void Update()
    {
        GetData();
        ShootRay(_touchPos);
    }

    private void GetData()
    {
        _touchPos = new Vector3(_inputManager.ToutchPosInput.x, _inputManager.ToutchPosInput.y, 0);
        _touchPress = _inputManager.ToutchPrimaryInput;
    }

    private void ShootRay(Vector3 pMyInput)
    {
        if (_touchPress != 0) //Checkt of er getikt ingedrukt, omdat als het niet zo is is het nutteloos om de rest van de code te runnen.
        {
            _ray = _mainCamera.ScreenPointToRay(pMyInput); //Schiet raycast, om te kijken of de speler ergens zijn vinger op zit.
            if (Physics.Raycast(_ray, out _hit))
            {
                Interaction(_hit.transform.gameObject); //Start funcitie en geeft het gameobject mee waar de vinger over staat, zodat we later een interface kunnen uitvoeren.
            }
        }
    }

    private void Interaction(GameObject pSelectedObject)
    {
        if (pSelectedObject.TryGetComponent<IClickable>(out IClickable pClick))
        {
            pClick.OnClick(); //Als de gameobject de OnClick functie heeft word deze uitgevoerd.
        }
    }
}