//using JetBrains.Annotations;
using System.Collections.Generic;
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

    public bool isGameClear = false;
    public bool isGameOver = false;

    public static int currentDoorNumber = 0;
    public static int keys = 1;
    public static Dictionary<string, bool> keyGot;
    public static int arrows = 10;

    public static int defaultPlayerLife = 10;

    static int pendingArrows;
    static int pendingKeys;
    static Dictionary<string, bool> pendingKeyGot = new Dictionary<string, bool>();


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameState = GameState.InGame;

        if (keyGot == null)
        {
            keyGot = new Dictionary<string, bool>();
        }

        if (!(keyGot.ContainsKey(SceneManager.GetActiveScene().name)))
        {
            keyGot.Add(SceneManager.GetActiveScene().name, false);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrows = 10;
        keys = 1;

        PendingItems();
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
            isGameClear = true;
        }
        else if (gameState == GameState.GameOver)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(meGameOver);
            gameState = GameState.GameEnd;
            isGameOver = true;
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

    public void GameEnd()
    {
        if (gameState == GameState.GameEnd)
        {
            if (isGameClear) 
            {
                Next(); 
            }
            else if (isGameOver) {
                Restart(); 
            }
        }
    }

    public static void PendingItems()
    {
        pendingArrows = arrows;
        pendingKeys = keys;
        foreach(var k in keyGot)
        {
            pendingKeyGot[k.Key] = k.Value;
        }
    }

    public static void ReturnPending()
    {
        arrows = pendingArrows;
        keys = pendingKeys;

        foreach(var k in pendingKeyGot)
        {
            if(k.Value != keyGot[k.Key])
            {
                keyGot[k.Key] = k.Value;
            }
        }
    }
}
