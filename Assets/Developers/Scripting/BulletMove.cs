using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float sizeSpeed;
    private float timeALive = 0;
    private Vector3 scaleAtStart;

    private void Start()
    {
        scaleAtStart = transform.localScale;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;

        timeALive += Time.deltaTime;
        transform.localScale = scaleAtStart * (1f - Mathf.Pow(1f / lifeTime * timeALive, sizeSpeed));
        
        if (timeALive > lifeTime)
        {
            timeALive = 0;
            gameObject.SetActive(false);
        }
    }
}
