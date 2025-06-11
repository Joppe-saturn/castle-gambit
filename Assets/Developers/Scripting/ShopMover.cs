using System.Collections;
using UnityEngine;

public class ShopMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 openPos;
    [SerializeField] private Vector3 closePos;

    public bool isOpen = true;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        openPos = rectTransform.localPosition;
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
