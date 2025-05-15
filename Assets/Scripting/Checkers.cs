using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float speed;
    private DataManager _dataManager;
    private List<Transform> _transformList = new();
    private Transform _tileToMoveTo;
    private int _index;
    private float _yAxis;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _transformList = _dataManager.Paths(0).ToList();
        _yAxis = transform.position.y;
        GetData();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        GetData();
        CheckTileToMoveTo();
    }

    private void GetData()
    {
        _tileToMoveTo = _transformList[_index];
        if (_tileToMoveTo.CompareTag("EndTile"))
        {
            Destroy(gameObject);
        }
    }

    private void CheckTileToMoveTo()
    {
        if(MathF.Abs((transform.position - _tileToMoveTo.transform.position).magnitude) < 0.1f) 
        {
            _index++;
        }
    }

    private void Movement()
    {
        transform.position = Vector3.Lerp(transform.position, _tileToMoveTo.transform.position , speed);
        transform.position = new Vector3(transform.position.x, _yAxis, transform.position.z);
    }
}

