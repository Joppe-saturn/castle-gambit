using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

public class Pawn : ChessPiecesBase
{
    private DataManager _dataManager;
    private List<GameObject> _tilesToAttack = new List<GameObject>();
    private Vector3 _startPos;
    private bool _isAttacking;

    [Header("General settings")]
    [SerializeField] private bool _isWhite;

    [Header("Attack settings")]
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _cooldownTime;

    [Header("Rook settings")]
    [SerializeField] private float _rookCooldownTime;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _startPos = transform.position;
    }

    private void Update()
    {
        Attack();
        CheckForRook(_dataManager, _startPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PathTile")) // Kijkt welk stuk pad in de buurt is, en kiest die om aan te vallen.
        {
            _tilesToAttack.Add(other.gameObject);
            for (int i = 0; i < _colliders.Length; i++)
            {
                Destroy(_colliders[i]); // Vernitiegt de colliders, want hierna worden ze niet meer gebruikt.
            }
        }
    }

    private void Attack()
    {
        if (_tilesToAttack.Count != 0)
        {
            if (_isAttacking == false)
            {
                if (_isWhite == true)
                {
                    for (int i = 0; i < _dataManager.WhiteCheckers.Count; i++)
                    {
                        for (int j = 0; j < _tilesToAttack.Count; j++)
                        {
                            if (Mathf.Abs((_tilesToAttack[j].transform.position - _dataManager.WhiteCheckers[i].transform.GetChild(0).position).magnitude) < 0.2f)
                            {
                                StartCoroutine(StartAttack(_tilesToAttack[j]));
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _dataManager.BlackCheckers.Count; i++)
                    {
                        for (int j = 0; j < _tilesToAttack.Count; j++)
                        {
                            if (Mathf.Abs((_tilesToAttack[j].transform.position - _dataManager.BlackCheckers[i].transform.GetChild(0).position).magnitude) < 0.2f)
                            {
                                StartCoroutine(StartAttack(_tilesToAttack[j]));
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator StartAttack(GameObject pTileToAttack)
    {
        float _timer = 0;
        float endTime = 1f;
        _isAttacking = true;

        while (_timer <= endTime)
        {
            _timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pTileToAttack.transform.position, _timer); // Beweegt hem rustig naar voren.
            transform.position = new Vector3(transform.position.x, _startPos.y, transform.position.z); // Houd de y-axis stabiel.
            yield return new WaitForEndOfFrame();
        }

        _timer = 0;

        while (_timer <= endTime)
        {
            _timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, _startPos, _timer); // Beweegt hem rustig naar achter.
            transform.position = new Vector3(transform.position.x, _startPos.y, transform.position.z); // Houd de y-axis stabiel.
            yield return new WaitForEndOfFrame();
        }

        if(closeToRook == true)
        {
            yield return new WaitForSeconds(_rookCooldownTime);
        }
        else
        {
            yield return new WaitForSeconds(_cooldownTime);
        }

        _isAttacking = false;
    }
}
