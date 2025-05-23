using UnityEngine;
using System.Collections;


public class Knight : ChessPiecesBase
{
    [SerializeField] private GameObject horseBullet;
    private GameObject horseBulletInstance;
    private DataManager _dataManager;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float rotationSpeed;
    private float currentDistance = 0;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        horseBulletInstance = Instantiate(horseBullet, transform.position, Quaternion.identity);
        StartCoroutine(SummonBullet());
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
        float currentTime = 0;
        while (true)
        {
            currentTime += 0.02f * orbitSpeed;
            horseBulletInstance.transform.Rotate(new Vector3(0, rotationSpeed, 0));
            horseBulletInstance.transform.position = transform.position + new Vector3(Mathf.Sin(currentTime) * currentDistance, 0, Mathf.Cos(currentTime) * currentDistance);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator SummonBullet()
    {
        while (currentDistance < distance)
        {
            currentDistance += speed * 0.02f;
            horseBulletInstance.transform.position += new Vector3(currentDistance - Vector3.Distance(horseBulletInstance.transform.position, transform.position), 0, 0);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
