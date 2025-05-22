using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float _speed;
    [Header("Health settings")]
    [SerializeField] private int _health;

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
        if(MathF.Abs((transform.position - _tileToMoveTo.transform.position).magnitude) < 0.1f) // Berekent of de checker in een bepaalde afstand van de tile af is, zodat hij door kan bewegen.
        {
            _index++;
        }
    }

    private void Movement()
    {
        transform.position = Vector3.Lerp(transform.position, _tileToMoveTo.transform.position , _speed);
        transform.position = new Vector3(transform.position.x, _yAxis, transform.position.z); // Zorgt ervoor dat hij op de juiste Y axis bijft.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChessPiece"))
        {
            _health--;
        }

        if(_health >= 0) 
        { 
            Destroy(gameObject);        
        }
    }
}

