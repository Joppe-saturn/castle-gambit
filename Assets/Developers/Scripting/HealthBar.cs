using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private GameObject king;

    private DataManager _dataManager;
    private Slider _slider;

    private float _lastHealth;
    private AudioSource _audioSource;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.IsDead = false;

        _dataManager.Health = _startHealth;
        _slider = GetComponent<Slider>();

        _lastHealth = _dataManager.Health;
        _slider.value = _lastHealth / _startHealth;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        float health = _dataManager.Health;

        if (_lastHealth != health)
        {
            _lastHealth = health;
            _slider.value = health / _startHealth;

            _audioSource.Play();
            
            if (health <= 0)
            {
                _dataManager.IsDead = true;
                king.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        
    }
}
