using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    //AudioSource audioSource;
    //public AudioClip meGameClear;
    //public AudioClip meGameOver;

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

    public static long cnt = 0;

  
    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
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
        //arrows = 10;
        //keys = 1;
        PendingItems();

        string currentSceneName = SceneManager.GetActiveScene().name;

        //SoundManager.currentSoundManager.StopBGM();
        //switch (currentSceneName)
        //{
        //    case "WorldMap":
        //        break;

        //    case "Boss":
        //        break;

        //    default:
        //        break;
        //}

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

    // Update is called once per frame
    void Update()
    {
        cnt++;
        //Debug.Log("GameManager.Update>> " + gameState + "  " + cnt);
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
        //Debug.Log("GameManager.LateUpdate>> " + gameState + "  ;" + GameManager.cnt);
        if (gameState == GameState.GameClear)
        {
            //audioSource.Stop();
            //audioSource.PlayOneShot(meGameClear);
            SoundManager.currentSoundManager.StopBGM();
            SoundManager.currentSoundManager.PlayBGM(BGMType.GameClear);
            Invoke("GameStatusChange", 0.02f);
            isGameClear = true;
        }
        else if (gameState == GameState.GameOver)
        {
            //audioSource.Stop();
            //audioSource.PlayOneShot(meGameOver);
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
            else if (isGameOver) {
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

        pendingArrows = 0;
        pendingKeys = 0;
        pendingKeyGot.Clear();

    }
}
