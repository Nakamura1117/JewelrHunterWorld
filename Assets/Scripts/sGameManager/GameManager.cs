using System.Collections.Generic;
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
    public static int totalScore;

    public bool isGameClear = false;
    public bool isGameOver = false;

    public static int currentDoorNumber = 0;
    public static int keys = 0;
    public static Dictionary<string, bool> keyGot;
    public static int arrows = 10;

    public static int defaultPlayerLife = 10;

    static int pendingArrows;
    static int pendingKeys;
    static Dictionary<string, bool> pendingKeyGot = new Dictionary<string, bool>();

    private void Awake()
    {
        gameState = GameState.InGame;

        if (keyGot == null)
        {
            keyGot = new Dictionary<string, bool>();
            keys = 0;
        }

        if (!(keyGot.ContainsKey(SceneManager.GetActiveScene().name)))
        {
            keyGot.Add(SceneManager.GetActiveScene().name, false);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PendingItems();
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName != "WorldMap")
        {
            SoundManager.currentSoundManager.restartBGM = true;

            if (currentSceneName == "Boss")
            {
                SoundManager.currentSoundManager.StopBGM();
                SoundManager.currentSoundManager.PlayBGM(BGMType.InBoss);
            }
            else
            {
                SoundManager.currentSoundManager.StopBGM();
                SoundManager.currentSoundManager.PlayBGM(BGMType.InGame);
            }
        }
        else if (SoundManager.currentSoundManager.restartBGM)
        {
            SoundManager.currentSoundManager.StopBGM();
            SoundManager.currentSoundManager.PlayBGM(BGMType.Title);
        }
    }

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
            SoundManager.currentSoundManager.StopBGM();
            SoundManager.currentSoundManager.PlayBGM(BGMType.GameClear);
            Invoke("GameStatusChange", 0.02f);
            isGameClear = true;
        }
        else if (gameState == GameState.GameOver)
        {
            SoundManager.currentSoundManager.StopBGM();
            SoundManager.currentSoundManager.PlayBGM(BGMType.GameOver);
            Invoke("GameStatusChange", 0.02f);
            isGameOver = true;
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Next()
    {
        SaveDataManager.SaveGamedata();
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
            else if (isGameOver)
            {
                Restart();
            }
        }
    }

    private void GameStatusChange()
    {
        // 引数なしの場合は「GameEnd」に変更
        GameManager.gameState = GameState.GameEnd;
    }

    private void GameStatusChange(GameState status)
    {
        GameManager.gameState = status;
    }

    public static void PendingItems()
    {
        pendingArrows = arrows;
        pendingKeys = keys;
        foreach (var k in keyGot)
        {
            pendingKeyGot[k.Key] = k.Value;
        }
    }

    public static void ReturnPending()
    {
        arrows = pendingArrows;
        keys = pendingKeys;

        foreach (var k in pendingKeyGot)
        {
            if (k.Value != keyGot[k.Key])
            {
                keyGot[k.Key] = k.Value;
            }
        }

        pendingArrows = 0;
        pendingKeys = 0;
        pendingKeyGot.Clear();

    }

    public static void GameExit()
    {
        SaveDataManager.SaveGamedata();
        Application.Quit();
    }
}
