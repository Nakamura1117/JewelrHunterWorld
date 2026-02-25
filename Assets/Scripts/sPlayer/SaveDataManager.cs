using UnityEngine.SceneManagement;
using System.Collections.Generic;

using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static void SaveGamedata()
    {
        SaveData saveData = new SaveData();
        saveData.saveDoorNumber = GameManager.currentDoorNumber;
        saveData.saveTotalScore = GameManager.totalScore;
        saveData.saveKeys = GameManager.keys;

        saveData.saveKeyGot = new List<KeyGotEntry>();
        if (GameManager.keyGot != null)
        {
            foreach (var entry in GameManager.keyGot)
            {
                saveData.saveKeyGot.Add(new KeyGotEntry 
                {
                    key = entry.Key,
                    got = entry.Value 
                });
            }
        }

        saveData.saveArrows = GameManager.arrows;
        string jsonData = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString("SaveData",jsonData);
        PlayerPrefs.Save();

        Debug.Log("セーブしました：" + jsonData);
        Debug.Log(GameManager.keyGot);
    }
    public static void LoadGameData()
    {
        // PlayerPrefsからJSON文字列をロード
        string jsonData = PlayerPrefs.GetString("SaveData");

        // JSONデータが存在しない場合、エラーを回避し処理を中断
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.LogWarning("セーブデータが見つかりません");
            Initialize(); // 初期化する
            return;
        }

        // JSON文字列をSaveDataクラスのインスタンスに変換して初期化
        SaveData loadData = JsonUtility.FromJson<SaveData>(jsonData);

        // ロードしたデータを各static変数に適用
        GameManager.currentDoorNumber = loadData.saveDoorNumber;
        GameManager.totalScore = loadData.saveTotalScore;
        GameManager.keys = loadData.saveKeys;

        //List<KeyGotEntry> -> Dictionary への変換
        GameManager.keyGot = new Dictionary<string, bool>(); // Dictionaryを初期化
        if (loadData.saveKeyGot != null) // ロードしたリストが null でないことを確認
        {
            foreach (var entry in loadData.saveKeyGot)
            {
                GameManager.keyGot.Add(entry.key, entry.got);
            }
        }

        //List<KeyOpenedEntry> -> Dictionary への変換
        World_UIController.keyOpened = new Dictionary<int, bool>(); // Dictionaryを初期化
        if (loadData.saveKeyOpened != null) // ロードしたリストが null でないことを確認
        {
            foreach (var entry in loadData.saveKeyOpened)
            {
                World_UIController.keyOpened.Add(entry.doorNumber, entry.opened);
            }
        }

        GameManager.arrows = loadData.saveArrows;

        //WorldMapに行く
        SceneManager.LoadScene("WorldMap");
    }

    //データの初期化
    public static void Initialize()
    {
        PlayerPrefs.DeleteAll(); //全部消す
        GameManager.currentDoorNumber = 0;
        GameManager.totalScore = 0;
        GameManager.keys = 1;
        if (GameManager.keyGot != null)
        {
            GameManager.keyGot.Clear(); //ディクショナリーを削除
        }
        if (World_UIController.keyOpened != null)
        {
            World_UIController.keyOpened.Clear(); //ディクショナリーを削除
        }
        GameManager.arrows = 10;
    }


}

[System.Serializable]
public class SaveData
{
    public int saveDoorNumber;
    public int saveTotalScore;
    public int saveKeys;

    public List<KeyGotEntry> saveKeyGot = new List<KeyGotEntry>();
    public List<KeyOpenedEntry> saveKeyOpened = new List<KeyOpenedEntry>();
    public int saveArrows;
    public int savePlayerLife;
}

[System.Serializable]
public class KeyGotEntry
{
    public string key;
    public bool got;
}

[System.Serializable]
public class KeyOpenedEntry
{
    public int doorNumber;
    public bool opened;
}