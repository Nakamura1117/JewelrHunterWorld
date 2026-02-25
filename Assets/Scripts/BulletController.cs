using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float deleteTime = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
