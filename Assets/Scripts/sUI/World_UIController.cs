using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World_UIController : MonoBehaviour
{

    public static Dictionary<int, bool> keyOpened;

    public TextMeshProUGUI keyText;
    int currentKeys;
    public TextMeshProUGUI arrowText;
    int currentArrows;
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Entrance");

        if (keyOpened == null)
        {
            keyOpened = new Dictionary<int, bool>();
            for (int i = 0; i < obj.Length; i++)
            {
                EntranceContrroler entranceController = obj[i].GetComponent<EntranceContrroler>();
                if (entranceController != null)
                {
                    keyOpened.Add(
                        entranceController.doorNumber,
                        entranceController.opened
                    );
                }
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 currentPlayerPos = Vector2.zero;
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].GetComponent<EntranceContrroler>().doorNumber == GameManager.currentDoorNumber)
            {
                currentPlayerPos = obj[i].transform.position;
            }
        }
        player.transform.position = currentPlayerPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentKeys != GameManager.keys)
        {
            currentKeys = GameManager.keys;
            keyText.text = currentKeys.ToString();
        }
        if (currentArrows != GameManager.arrows)
        {
            currentArrows = GameManager.arrows;
            arrowText.text = currentArrows.ToString();
        }

    }
}
