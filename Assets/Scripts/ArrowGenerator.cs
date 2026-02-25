using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    GameObject[] objects;
    GameObject player;
    public Sprite itemBoxClose;
    public Sprite itemBoxOpen;
    private bool isRecover;

    int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isRecover = false;
        objects = GameObject.FindGameObjectsWithTag("ItemBox");
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<SpriteRenderer>().sprite = itemBoxOpen;
            objects[i].GetComponent<Advent_ItemBox>().isClosed = false;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(i);
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].GetComponent<Advent_ItemBox>().isClosed)
            {
                return;
            }
        }

        if (GameManager.arrows <= 0 && !(isRecover))
        {
            isRecover = true;
            Debug.Log("rrr");
        }

        if (player != null && isRecover)
        {
            Debug.Log("re" + i);
            int index = Random.Range(0, objects.Length);
            objects[index].GetComponent<Advent_ItemBox>().isClosed = true;
            objects[index].GetComponent<SpriteRenderer>().sprite = itemBoxClose;
            
            isRecover = false;
        }



    }
}
