using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public string sceneName;
    public GameObject startButton;
    public GameObject continueButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string jsonData = PlayerPrefs.GetString("SaveData");
        if(jsonData != null)
        {
            continueButton.GetComponent<Button>().interactable = false;
        }

        SoundManager.currentSoundManager.StopBGM();
        SoundManager.currentSoundManager.PlayBGM(BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnSubmit(InputValue value)
    {
        Load();
    }


    public void Load()
    {
        SaveDataManager.Initialize();
        SceneManager.LoadScene(sceneName);
    }

    public void Continue()
    {
        SaveDataManager.LoadGameData();
        SceneManager.LoadScene(sceneName);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
