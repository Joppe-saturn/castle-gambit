using System.Collections;
using UnityEngine;

public class Pawn : ChessPiecesBase
{
    [SerializeField] private GameObject _tileToAttack;
    private DataManager _instance;
    private Vector3 _startPos;
    private bool _attack;
    private float _actualAttackSpeed;

    [Header("Attack settings")]
    [SerializeField] Vector3 _offSet;
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _rookModifier;

    private void Start()
    {
        _instance = DataManager.GetInstance();
        _startPos = transform.position;
        StartCoroutine(StartAttack());
    }

    private void Update()
    {
        CheckForRook(_instance);
        RookModifiers();
    }

    private void FixedUpdate()
    {
        Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PathTile")) // Kijkt welk stuk pad in de buurt is, en kiest die om aan te vallen.
        {
            _tileToAttack = other.gameObject;
            for (int i = 0; i < _colliders.Length; i++)
            {
                Destroy(_colliders[i]); // Vernitiegt de colliders, want hierna worden ze niet meer gebruikt.
            }
        }
    }

    private void RookModifiers()
    {
        if(closeToRook == true)
        {
            _actualAttackSpeed = _attackSpeed * _rookModifier;
        }
        else
        {
            _actualAttackSpeed = _attackSpeed;
        }
    }

    private void Attack()
    {
        if (_attack == true)
        {
            transform.position = Vector3.Lerp(transform.position, _tileToAttack.transform.position - _offSet, 0.1f); // Beweegt hem rustig naar voren.
            transform.position = new Vector3(transform.position.x, _startPos.y, transform.position.z); // Houd de y-axis stabiel.
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _startPos, 0.1f); // Beweegt hem rustig naar achter.
            transform.position = new Vector3(transform.position.x, _startPos.y, transform.position.z); // Houd de y-axis stabiel.
        }
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(_actualAttackSpeed);
        _attack = true;
        yield return new WaitForSeconds(1f);
        _attack = false;
        StartCoroutine(StartAttack());
    }
}
