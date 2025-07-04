using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private Rigidbody king;
    private DataManager _dataManager;
    private Slider _slider;

    private float _lastUpdatedhealth;
    private AudioSource _audioSource;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.IsDead = false;

        _dataManager.Health = _startHealth;
        _slider = GetComponent<Slider>();

        _audioSource = GetComponent<AudioSource>();
        _lastUpdatedhealth = _startHealth;
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        float health = _dataManager.Health;
        
        _slider.value = health / _startHealth;

        if (health != _lastUpdatedhealth)
        {
            _lastUpdatedhealth = health;

            _audioSource.Play();

            if (health <= 0)
            {
                _dataManager.IsDead = true;
                king.isKinematic = false;
            }
        }
    }

}
