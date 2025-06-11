using System.Collections;
using UnityEngine;

public class ShopMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 openPos;
    [SerializeField] private float closePosY;
    [SerializeField] private GameObject shopBox;
    private Vector3 closePos;
    [SerializeField] private RectTransform arrow;

    private Coroutine moveShop;
    private Coroutine rotateArrow;

    protected bool isOpen = true;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        openPos = rectTransform.localPosition;
        closePos = new Vector3(openPos.x, closePosY, 0);
    }

    public void ChangeShopPos()
    {
        isOpen = !isOpen;
        shopBox.SetActive(isOpen);

        if (moveShop != null && rotateArrow != null)
        {
            StopCoroutine(moveShop);
            StopCoroutine(rotateArrow);
        }

        moveShop = StartCoroutine(LerpShop());
        rotateArrow = StartCoroutine(RotateArrow());
    }

    private IEnumerator RotateArrow()
    {
        if (isOpen)
        {
            while (arrow.localRotation.eulerAngles.z > 0)
            {
                arrow.localRotation = Quaternion.Euler(0, 0, arrow.localRotation.eulerAngles.z + (0f - arrow.localRotation.eulerAngles.z) / speed);
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            while (arrow.localRotation.eulerAngles.z < 180)
            {
                arrow.localRotation = Quaternion.Euler(0, 0, arrow.localRotation.eulerAngles.z + (180f - arrow.localRotation.eulerAngles.z) / speed);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    private IEnumerator LerpShop()
    {
        if (isOpen)
        {
            while (rectTransform.localPosition != openPos && isOpen)
            {
                rectTransform.localPosition += (openPos - rectTransform.localPosition) / speed;
                yield return new WaitForSeconds(0.02f);
            }
        } 
        else
        {
            while (rectTransform.localPosition != closePos && !isOpen)
            {
                rectTransform.localPosition += (closePos - rectTransform.localPosition) / speed;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
