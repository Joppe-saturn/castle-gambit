using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    private DataManager _dataManager;
    private Slider _slider;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.IsDead = false;

        _dataManager.Health = _startHealth;
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        _slider.value = _dataManager.Health / _startHealth;
        if(_dataManager.Health <= 0) 
        {
            _dataManager.IsDead = true;
        }
    }

}
