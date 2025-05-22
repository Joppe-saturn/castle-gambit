using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiecesBase
{
    [SerializeField] private GameObject _attackObjects;
    private List<GameObject> _attackList = new();
    private DataManager _dataManager;
    [SerializeField] private float _attackTime;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackDistace;
    [SerializeField] private float _attackHoldTime;
    [SerializeField] private Vector3 _attackOffset;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        for (int i = 0; i < 4; i++)
        {
            _attackList.Add(Instantiate(_attackObjects, transform.position, Quaternion.identity));
            _attackList[i].transform.Rotate(0, 90 * i + 45, 0);
            _attackList[i].SetActive(false);
        }

        StartCoroutine(AttackCycle());
    }

    private void Update()
    {
        CheckForRook(_dataManager);
        RookModifiers();
    }

    private void RookModifiers()
    {
        if (closeToRook == true)
        {

        }
        else
        {

        }
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackTime);

            for (int i = 0; i < _attackList.Count; i++)
            {
                _attackList[i].SetActive(true);
                _attackList[i].transform.position = transform.position + _attackOffset;
            }


            for (float i = 0; i < _attackSpeed; i += 0.02f)
            {
                for (int j = 0; j < _attackList.Count; j++)
                {
                    _attackList[j].transform.position += _attackList[j].transform.forward * _attackDistace / _attackSpeed * 0.01f;
                }
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(_attackHoldTime);

            for (float i = 0; i < _attackSpeed; i += 0.02f)
            {
                for (int j = 0; j < _attackList.Count; j++)
                {
                    _attackList[j].transform.position -= _attackList[j].transform.forward * _attackDistace / _attackSpeed * 0.01f;
                }
                yield return new WaitForSeconds(0.02f);
            }

            for (int i = 0; i < _attackList.Count; i++)
            {
                _attackList[i].SetActive(false);
            }
        }
    }
}