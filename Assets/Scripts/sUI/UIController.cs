
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;


public class UIController : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;
    
    public GameObject timeUI;
    public GameObject timeTxt;
    private TimeController timeController;
    private bool useTime = true;

    public GameObject scoreTxt;
    public int stageScore = 0;
    private int currentScore = 0;
    private int displayScore = 0;

    public TextMeshProUGUI keyText;
    int currentKeys;
    public TextMeshProUGUI arrowText;
    int currentArrows;
    public Slider lifeSlider;
    int currentLife;

    public GameObject scaleLine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.PendingItems();
        mainImage.transform.localPosition = Vector3.zero;
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

        currentKeys = GameManager.keys;
        keyText.text = currentKeys.ToString();

        currentArrows = GameManager.arrows;
        arrowText.text = currentArrows.ToString();

        currentLife = Player.playerLife;

        Vector2 rect = lifeSlider.GetComponent<RectTransform>().sizeDelta;
        float sliderWidth = rect.x;
        int lineSpace = (int)sliderWidth / Player.playerLife;
        GameObject[] scaleLines = new GameObject[currentLife];

        for (int i = 0; i < currentLife-1; i++) 
        {
            //Debug.Log(i);
            float p = (lineSpace * (i + 1)) - 1;
            scaleLines[i] = Instantiate(scaleLine, Vector3.zero, quaternion.identity, scaleLine.transform.parent);

            scaleLines[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(p, 0);
            scaleLines[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("UIController.Update>> " + GameManager.gameState + " cnt: "+GameManager.cnt);


        //if (GameManager.gameState == GameState.GameClear || gm.isGameClear)
        if (GameManager.gameState == GameState.GameClear)
        {
            mainImage.transform.localPosition = new Vector3(0, 120, 0);
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = restartButton.GetComponent<Button>();
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
        //else if (GameManager.gameState == GameState.GameOver || gm.isGameOver)
        else if (GameManager.gameState == GameState.GameOver )
        {
            mainImage.transform.localPosition = new Vector3(0, 120, 0);
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;

            GameManager.ReturnPending();

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
                if (currentLife != Player.playerLife)
                {
                    currentLife = Player.playerLife;
                    lifeSlider.value = currentLife;
                }

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
        //Debug.Log("image inactive");
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
