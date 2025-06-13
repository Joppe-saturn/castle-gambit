using UnityEngine;
using UnityEngine.Rendering;

public class BishopProjectileSpin : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        float _rotationY = transform.rotation.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, _rotationY + _speed, 0f);
    }
}
