using System.Collections;
using UnityEngine;

public class ShopMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 openPos;
    [SerializeField] private float closePosY;
    private Vector3 closePos;

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

        StartCoroutine(LerpShop());
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
