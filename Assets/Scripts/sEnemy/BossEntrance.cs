using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEntrance : MonoBehaviour
{
    public static Dictionary<int, bool> stagesClear;
    public string sceneName;
    bool isOpened;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Entrance");

        if (stagesClear == null)
        {
            stagesClear = new Dictionary<int, bool>();

            for (int i = 0; i < obj.Length; i++)
            {
                EntranceContrroler entranceContrroler = obj[i].GetComponent<EntranceContrroler>();
                if (entranceContrroler != null)
                {
                    stagesClear.Add(
                        entranceContrroler.doorNumber,
                        false
                    );
                }
            }
        }
        else
        {
            int sum = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                if (stagesClear[i])
                {
                    sum++;
                }
            }

            if (sum >= obj.Length)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                isOpened = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && isOpened)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
