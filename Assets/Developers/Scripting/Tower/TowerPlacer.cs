using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacer : MonoBehaviour
{
    [System.Serializable]
    public class Tower
    {
        public string Name;
        public GameObject whiteObject;
        public GameObject blackObject;
        public Mesh PrelookMesh;
        public int Price;
    }

    [SerializeField] private Tower[] towers;

    [SerializeField] private DataManager _dataManager;

    [Header("ColorButton")]
    [SerializeField] private GameObject colorButton;
    private Image colorButtonImage;
    private TextMeshProUGUI colorButtonText;
    
    [SerializeField] private string[] colors;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed;
    
    private bool isLerping = false;

    private ShopMover shopMover;

    [SerializeField] private bool startColor;

    [SerializeField] private AudioSource buttonClick;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _dataManager.IsPlacingWhite = startColor;

        colorButtonImage = colorButton.GetComponent<Image>();
        colorButtonText = colorButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        shopMover = FindFirstObjectByType<ShopMover>();
    }

    public void LoadTower(int tower)
    {
        Tower currentTower = towers[tower];
        
        buttonClick.Play();

        if (_dataManager.Money >= currentTower.Price)
        {
            shopMover.CloseShop();
            _dataManager.CurrentTower = currentTower;
        }
    }

    public void ChangeColor()
    {
        if (!isLerping)
        {
            buttonClick.Play();
            _dataManager.IsPlacingWhite = !_dataManager.IsPlacingWhite;
            StartCoroutine(ColorLerp());
        }
    }

    private IEnumerator ColorLerp()
    {
        isLerping = true;

        int color = 1;

        if(_dataManager.IsPlacingWhite)
        {
            color = 0;
        }

        while(colorButtonImage.color.a > 0)
        {
            colorButtonImage.color -= new Color(0, 0, 0, speed * 0.02f);
            colorButtonText.color -= new Color(0, 0, 0, speed * 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        
        colorButtonImage.sprite = sprites[color];
        colorButtonText.text = colors[color];
        colorButtonText.color = new Color(color * 255, color * 255, color * 255, 0);

        while (colorButtonImage.color.a < 1)
        {
            colorButtonImage.color += new Color(0, 0, 0, speed * 0.02f);
            colorButtonText.color += new Color(0, 0, 0, speed * 0.02f);
            yield return new WaitForSeconds(0.02f);
        }

        isLerping = false;
    }
}