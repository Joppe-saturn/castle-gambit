using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckerMovement : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private bool isWhite;

    [Header("Movement settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _bishopFreezeTime;
    [SerializeField] private float _bishopFreezeStrenght;
    
    [Header("Health settings")]
    [SerializeField] private Mesh _singleCheckerMesh;
    [SerializeField] private int _health;
    [SerializeField] private Material _slimeMaterial;
    private Material _material;

    [Header("Money settings")]
    [SerializeField] private int _moneyOnDeath;

    private DataManager _dataManager;
    private List<Transform> _transformList = new();
    private Transform _tileToMoveTo;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private float _actualSpeed;
    private int _index;
    private float _yAxis;

    private AudioSource _audioSource;
    private bool isAlive;

    private void Start()
    {
        _meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        _meshFilter = transform.GetChild(0).GetComponent<MeshFilter>();
        _actualSpeed = _speed;
        _material = _meshRenderer.material;
        _dataManager = DataManager.GetInstance();
        _transformList = _dataManager.Paths(0).ToList();
        _yAxis = transform.position.y;

        _audioSource = GetComponent<AudioSource>();
        isAlive = true;

        if(isWhite == true)
        {
            _dataManager.WhiteCheckers.Add(gameObject);
        }
        else
        {
            _dataManager.BlackCheckers.Add(gameObject);
        }

        GetData();
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Movement();
        }
    }

    private void Update()
    {
        GetData();
        CheckTileToMoveTo();
    }

    private void OnDestroy()
    {
        if (isWhite == true)
        {
            _dataManager.WhiteCheckers.Remove(gameObject);
        }
        else
        {
            _dataManager.BlackCheckers.Remove(gameObject);
        }
    }

    private void GetData()
    {
        _tileToMoveTo = _transformList[_index];
        if (_tileToMoveTo.CompareTag("EndTile"))
        {
            _dataManager.Health--;
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
        transform.position = Vector3.Lerp(transform.position, _tileToMoveTo.transform.position , _actualSpeed);
        transform.position = new Vector3(transform.position.x, _yAxis, transform.position.z); // Zorgt ervoor dat hij op de juiste Y axis bijft.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChessPiece"))
        {
            _audioSource.Play();
            _health--;
        }
        else if (other.CompareTag("SlimeBullet"))
        {
            StartCoroutine(Slimed());
        }

        if(_health == 1)
        {
            _meshFilter.mesh = _singleCheckerMesh;
        }

        if (_health <= 0 && isAlive)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isAlive = false;
        _dataManager.Money += _moneyOnDeath;
        transform.position = new Vector3(0, -2, 0);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private IEnumerator Slimed() // Gebeurt als hij word geraakt met een wapen van de bishop.
    {
        _meshRenderer.material = _slimeMaterial; // Vernadert de kleur om te visualiseren dat hij geraakt is.
        _actualSpeed = _speed * _bishopFreezeStrenght;
        yield return new WaitForSeconds(_bishopFreezeTime);
        _actualSpeed = _speed;
        _meshRenderer.material = _material; // Verandert hem weer terug
    }
}

