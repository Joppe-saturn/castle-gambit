using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Queen : ChessPiecesBase
{
    [SerializeField] private float rotationSpeed;
    [Header("bullets")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootTime;
    [SerializeField] private float waitTime;
    [SerializeField] private int amountOfBullets;
    [SerializeField] private int rookMoreBullets;

    private DataManager _dataManager;
    private int shootableBullets;

    private readonly List<GameObject> bulletInstances = new();

    private void Start()
    {
        _dataManager = DataManager.GetInstance();

        for (int i = 0; i < amountOfBullets + rookMoreBullets; i++)
        {
            bulletInstances.Add(Instantiate(bullet));
            bulletInstances[i].SetActive(false);
        }

        StartCoroutine(Attack());
    }

    private void Update()
    {
        CheckForRook(_dataManager, transform.position);
        RookModifiers();
    }

    private IEnumerator Attack()
    {
        float timePassed = 0;

        int bulletCount = 0;
        while (true)
        {
            transform.GetChild(0).Rotate(0, rotationSpeed * Time.deltaTime, 0);

            timePassed += Time.deltaTime;

            if (timePassed > shootTime)
            {
                timePassed = 0;

                for (int i = 0; i < shootableBullets; i++)
                {
                    GameObject currentBullet = bulletInstances[i];

                    if (!currentBullet.activeSelf)
                    {
                        currentBullet.SetActive(true);
                        currentBullet.transform.SetPositionAndRotation(transform.GetChild(0).position, transform.GetChild(0).rotation);
                        break;
                    }
                }

                bulletCount++;

                if (bulletCount == shootableBullets)
                {
                    bulletCount = 0;
                    timePassed -= waitTime;
                }
            }

            yield return null;
        }
    }

    private void RookModifiers()
    {
        if (closeToRook == true)
        {
            shootableBullets = amountOfBullets + rookMoreBullets;
        }
        else
        {
            shootableBullets = amountOfBullets;
        }
    }
}
