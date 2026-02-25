using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public string sceneName;
    public GameObject startButton;
    public GameObject continueButton;

    //public InputAction submitAction;
    //public InputAction cancelAction;


    //private void OnEnable()
    //{
    //    submitAction.Enable();
    //}
    //private void OnDisable()
    //{
    //    submitAction.Disable();
    //}


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
        //Keyboard kb = Keyboard.current;

        //if (kb != null)
        //{
        //    if (kb.enterKey.wasPressedThisFrame)
        //    {
        //        Load();
        //    }
        //}

        //if (submitAction.WasPressedThisFrame())
        //{
        //    Debug.Log("binding;" + submitAction.GetHashCode());
        //    Load();
        //}
    }

    private void OnSubmit(InputValue value)
    {
        Load();
    }


    public void Load()
    {
        //GameManager.totalScore = 0;
        SaveDataManager.Initialize();
        SceneManager.LoadScene(sceneName);
    }

    public void Continue()
    {
        SaveDataManager.LoadGameData();
        SceneManager.LoadScene(sceneName);
    }
}
