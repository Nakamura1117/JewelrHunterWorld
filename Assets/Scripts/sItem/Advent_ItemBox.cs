using UnityEngine;
using UnityEngine.SceneManagement;

public class Advent_ItemBox : MonoBehaviour
{

    public Sprite openImage;
    public GameObject itemPrefab;
    public bool isClosed = true;
    public Advent_Item.AdventItemType type = Advent_Item.AdventItemType.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(type == Advent_Item.AdventItemType.Key)
        {
            if (GameManager.keyGot[SceneManager.GetActiveScene().name])
            {
                GetComponent<SpriteRenderer>().sprite = openImage;
                isClosed = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isClosed && collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false;
            if (itemPrefab != null)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
