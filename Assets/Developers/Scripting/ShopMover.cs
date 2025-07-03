using System.Collections;
using UnityEngine;

public class ShopMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 openPos;
    [SerializeField] private float closePosY;
    [SerializeField] private GameObject shopBox;
    [SerializeField] private float boxWaitTime;
    private Vector3 closePos;
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform cancelButton;
    public Vector3 cancelButtonOpenPos;
    private Vector3 cancelButtonClosePos;
    private bool isPlacing = false;

    private Coroutine moveShop;
    private Coroutine rotateArrow;
    private Coroutine moveCancelButton;

    protected bool isOpen = true;

    private RectTransform rectTransform;

    private DataManager _dataManager;

    private AudioSource audioSource;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        openPos = rectTransform.localPosition;
        closePos = new Vector3(openPos.x, closePosY, 0);
        _dataManager = DataManager.GetInstance();
        audioSource = GetComponent<AudioSource>();
        
        cancelButtonOpenPos = cancelButton.localPosition;
        cancelButtonClosePos = new Vector3(cancelButtonOpenPos.x, closePosY, 0);
        cancelButton.localPosition = cancelButtonClosePos;
    }

    private void Update()
    {
        if (isPlacing && _dataManager.CurrentTower == null)
        {
            isPlacing = false;
            StartCoroutine(LerpCancelButton());
        }
    }

    public void CloseShop()
    {
        if (isOpen)
        {
            ChangeShopPos();
        }
    }

    public void OpenShop()
    {
        if(!isOpen)
        {
            ChangeShopPos();
        }
    }

    public void ChangeShopPos()
    {
        isOpen = !isOpen;
        StartCoroutine(DissapearBox());
        if (moveShop != null && rotateArrow != null)
        {
            StopCoroutine(moveShop);
            StopCoroutine(rotateArrow);
        }
        if (moveCancelButton != null)
        {
            StopCoroutine(moveCancelButton);
        }

        moveShop = StartCoroutine(LerpShop());
        rotateArrow = StartCoroutine(RotateArrow());

        moveCancelButton = StartCoroutine(LerpCancelButton());
    }

    private IEnumerator DissapearBox()
    {
        yield return new WaitForSeconds(boxWaitTime);
        shopBox.SetActive(isOpen);
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

    public void Cancel()
    {
        _dataManager.CurrentTower = null;
        isPlacing = false;

        audioSource.Play();

        OpenShop();
    }

    private IEnumerator LerpCancelButton()
    {
        yield return null;
        if (!isOpen && _dataManager.CurrentTower != null)
        {
            isPlacing = true;

            while (cancelButton.localPosition != cancelButtonOpenPos && !isOpen)
            {
                cancelButton.localPosition += (cancelButtonOpenPos - cancelButton.localPosition) / speed;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            while (cancelButton.localPosition != cancelButtonClosePos && isOpen)
            {
                cancelButton.localPosition += (cancelButtonClosePos - cancelButton.localPosition) / speed;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
