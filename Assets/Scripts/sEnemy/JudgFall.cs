using Unity.VisualScripting;
using UnityEngine;

public class JudgFall : MonoBehaviour
{
    GameObject judgObj;
    Rigidbody2D judgFall;

    float time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        judgFall = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        //if (time >= 5)
        //{
        //    time = 0;
        //    transform.parent.gameObject.GetComponent<EnemyController>().returnDirect();
        //    Debug.Log("Judg return");
        //}
        return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("地形"))
        {
            if (transform.parent != null)
            {
                Debug.Log("Judg return");
                transform.parent.gameObject.GetComponent<EnemyController>().returnDirect();
            }
        }
    }
}
