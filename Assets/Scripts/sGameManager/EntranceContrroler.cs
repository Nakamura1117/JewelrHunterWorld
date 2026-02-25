using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Analytics;

public class EntranceContrroler : MonoBehaviour
{
    public int doorNumber;
    public string sceneName;
    public bool opened;
    bool isPlayerTouch;
    bool announcement;
    GameObject worldUI;
    GameObject talkPanel;
    TextMeshProUGUI messageText;
    World_Player worldPlayerCnt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldPlayerCnt = GameObject.FindGameObjectWithTag("Player").GetComponent<World_Player>();
        worldUI = GameObject.FindGameObjectWithTag("WorldUI");
        talkPanel = worldUI.transform.Find("TalkPanel").gameObject;
        messageText = talkPanel.transform.Find("MessageText").gameObject.GetComponent<TextMeshProUGUI>();

        if(World_UIController.keyOpened != null)
        {
            opened = World_UIController.keyOpened[doorNumber];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTouch && worldPlayerCnt.IsActionButtonPressed)
        {
            if (!announcement)
            {
                Time.timeScale = 0;

                if (opened)
                {
                    Time.timeScale = 1;
                    GameManager.currentDoorNumber = doorNumber;
                    SceneManager.LoadScene(sceneName);
                    return;
                }
                else if (GameManager.keys > 0)
                {
                    messageText.text = "新たなステージへの扉を開けた！";

                    SoundManager.currentSoundManager.PlaySE(SEType.DoorOpen);

                    GameManager.keys--;
                    opened = true;
                    World_UIController.keyOpened[doorNumber] = true;
                    announcement = true;

                    SaveDataManager.SaveGamedata();
                }
                else
                {
                    messageText.text = "鍵が足りません！";

                    SoundManager.currentSoundManager.PlaySE(SEType.DoorClosed);

                    announcement = true;
                }
            }
            else
            {
                Time.timeScale = 1;
                string s = "";
                if (!opened)
                {
                    s = "ロック";
                }
                else
                {
                    s = "解放";
                }
                messageText.text = string.Concat(sceneName + " への扉は " + s + " されている！");
                announcement = false;
            }

            worldPlayerCnt.IsActionButtonPressed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerTouch = true;
            talkPanel.SetActive(true);
            string s = "";
            if (!opened)
            {
                s = "ロック";
            }
            else
            {
                s = "解放";
            }
            messageText.text = string.Concat(sceneName + " への扉は " + s + " されている！");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerTouch = false;
            if (messageText != null)
            {
                talkPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
