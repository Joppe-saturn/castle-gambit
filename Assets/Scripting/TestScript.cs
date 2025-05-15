using UnityEngine;

public class TestScript : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        Destroy(gameObject);
    }
}
