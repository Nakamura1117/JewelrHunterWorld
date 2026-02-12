//using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    InGame,
    GameClear,
    GameOver,
    GameEnd,
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState;
    public string nextSceneName;
    //public string nextGameState;

    public static int totalScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.InGame;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameState.InGame)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (gameState != GameState.InGame)
        {
            return;
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void Next()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
