using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2.0f;
    public int attackPower = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.transform);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

    }
}
