
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartGutton;
    public GameObject nextButton;
    private GameState gamestate = GameState.InGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
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
            GameManager.gameState = GameState.GameEnd;
        }
        else if (GameManager.gameState == GameState.GameOver)
        {
            gamestate = GameState.GameOver;
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            GameManager.gameState = GameState.GameEnd;
        }
        else if (GameManager.gameState == GameState.InGame)
        {

        }

        Debug.Log("gamestate>> " + gamestate);
    }
        private void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
