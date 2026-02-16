using Unity.VisualScripting;
using UnityEngine;

public class DecisionGround : MonoBehaviour
{
    private Rigidbody2D decisionItem;

    float time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decisionItem = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("地形"))
        {
            if (transform.parent != null)
            {
                Debug.Log("Decision Ground");
                transform.parent.gameObject.GetComponent<EnemyController>().returnDirect();
            }
        }
    }
}
