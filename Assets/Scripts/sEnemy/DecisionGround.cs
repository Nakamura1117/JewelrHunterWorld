using Unity.VisualScripting;
using UnityEngine;

public class DecisionGround : MonoBehaviour
{
    private Rigidbody2D decisionItem;

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
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (transform.parent != null)
            {
                Debug.Log("Decision Ground");
                transform.parent.gameObject.GetComponent<EnemyController>().returnDirect();
            }
        }
    }
}
