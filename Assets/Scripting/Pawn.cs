using System.Collections;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private GameObject _tileToAttack;
    private Vector3 _startPos;
    private bool _attack;
    [SerializeField] Vector3 _offSet;
    [SerializeField] private Collider[] _colliders;

    private void Start()
    {
        _startPos = transform.position;
        StartCoroutine(StartAttack());
    }

    private void FixedUpdate()
    {
        Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PathTile"))
        {
            _tileToAttack = other.gameObject;
            for (int i = 0; i < _colliders.Length; i++)
            {
                Destroy(_colliders[i]);
            }
        }
    }

    private void Attack()
    {
        if (_attack == true)
        {
            transform.position = Vector3.Lerp(transform.position, _tileToAttack.transform.position - _offSet, 0.1f);
            transform.position = new Vector3(transform.position.x, _startPos.y, transform.position.z);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _startPos, 0.1f);
            transform.position = new Vector3(transform.position.x, _startPos.y, transform.position.z);
        }
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1.5f);
        _attack = true;
        yield return new WaitForSeconds(1f);
        _attack = false;
        StartCoroutine(StartAttack());
    }
}
