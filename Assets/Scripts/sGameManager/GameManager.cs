//using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
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

    AudioSource audioSource;
    public AudioClip meGameClear;
    public AudioClip meGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    private void LateUpdate()
    {
        if (gameState == GameState.GameClear)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(meGameClear);
            gameState = GameState.GameEnd;
        }
        else if (gameState == GameState.GameOver)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(meGameOver);
            gameState = GameState.GameEnd;
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
