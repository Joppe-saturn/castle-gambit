using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bishop : MonoBehaviour
{
    [SerializeField] private GameObject attackObjects;
    private List<GameObject> attackList = new();

    [SerializeField] private float attackTime;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDistace;
    [SerializeField] private float attackHoldTime;
    [SerializeField] private Vector3 attackOffset;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            attackList.Add(Instantiate(attackObjects, transform.position, Quaternion.identity));
            attackList[i].transform.Rotate(0, 90 * i + 45, 0);
            attackList[i].SetActive(false);
        }

        StartCoroutine(AttackCycle());
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTime);

            for (int i = 0; i < attackList.Count; i++)
            {
                attackList[i].SetActive(true);
                attackList[i].transform.position = transform.position + attackOffset;
            }


            for (float i = 0; i < attackSpeed; i += 0.02f)
            {
                for (int j = 0; j < attackList.Count; j++)
                {
                    attackList[j].transform.position += attackList[j].transform.forward * attackDistace / attackSpeed * 0.01f;
                }
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(attackHoldTime);

            for (float i = 0; i < attackSpeed; i += 0.02f)
            {
                for (int j = 0; j < attackList.Count; j++)
                {
                    attackList[j].transform.position -= attackList[j].transform.forward * attackDistace / attackSpeed * 0.01f;
                }
                yield return new WaitForSeconds(0.02f);
            }

            for (int i = 0; i < attackList.Count; i++)
            {
                attackList[i].SetActive(false);
            }
        }
    }
}