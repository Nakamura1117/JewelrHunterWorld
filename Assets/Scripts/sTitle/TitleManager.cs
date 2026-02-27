using System.Collections;
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
        if (jsonData != null)
        {
            continueButton.GetComponent<Button>().interactable = false;
        }
        TitleBGMStartCol();
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

    IEnumerator TitleBGMStartCol()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.currentSoundManager.StopBGM();
        SoundManager.currentSoundManager.PlayBGM(BGMType.Title);

    }
}
