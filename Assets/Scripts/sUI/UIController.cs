
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartGutton;
    public GameObject nextButton;
    private GameState gamestate = GameState.InGame;

    public GameObject timeUI;
    public GameObject timeTxt;
    private TimeController timeController;
    private bool useTime = true;

    public GameObject scoreTxt;
    public int stageScore = 0;
    private int currentScore = 0;
    private int displayScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeController = GameObject.FindWithTag("GameManager").GetComponent<TimeController>();
        if (timeController != null)
        {
            if (timeController.gameTime == 0.0f)
            {
                timeUI.SetActive(false);
                useTime = false;
            }
        }

        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == GameState.GameClear)
        {
            gamestate = GameState.GameClear;
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = restartGutton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;

            if (timeController != null)
            {
                timeController.IsTimeOver();
                int time = (int)timeController.GetDisplayTime();

                GameManager.totalScore += time * 100;
            }

            GameManager.totalScore += stageScore;
            stageScore = 0;
            UpdateScore();


        }
        else if (GameManager.gameState == GameState.GameOver)
        {
            gamestate = GameState.GameOver;
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;

            if (timeController != null)
            {
                timeController.IsTimeOver();
            }

        }
        else if (GameManager.gameState == GameState.InGame)
        {
            if (timeController != null)
            {
                if (GameObject.FindWithTag("Player").GetComponent<Player>() == null) { return; }

                if (timeController.gameTime > 0.0f)
                {
                    int time = (int)timeController.GetDisplayTime();
                    timeTxt.GetComponent<TextMeshProUGUI>().text = time.ToString("D3");
                    if (useTime && timeController.isCountDown && time <= 0)
                    {
                        GameObject.FindWithTag("Player").GetComponent<Player>().Dead();

                    }
                    else if (useTime && !(timeController.isCountDown) && timeController.gameTime <= time)
                    {
                        GameObject.FindWithTag("Player").GetComponent<Player>().Dead();
                    }

                }
            }


        }

        Debug.Log("gamestate>> " + gamestate);
    }

    private void FixedUpdate()
    {
        if (GameManager.gameState == GameState.InGame)
        {
            //Debug.Log(currentScore + "  , " + displayScore);

            if (currentScore > displayScore)
            {
                displayScore += 1;
                scoreTxt.GetComponent<TextMeshProUGUI>().text = currentScore.ToString("N0");
            }
        }
    }
    private void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    void UpdateScore()
    {
        currentScore = stageScore + GameManager.totalScore;
        scoreTxt.GetComponent<TextMeshProUGUI>().text = currentScore.ToString("N0");
    }

    public void UpdateScore(int score)
    {
        stageScore += score;
        currentScore = stageScore + GameManager.totalScore;
    }
}
