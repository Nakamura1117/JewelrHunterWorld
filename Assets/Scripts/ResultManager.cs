using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public string sceneName;

    void OnSubmit(InputValue value)
    {
        Destroy(SoundManager.currentSoundManager);
        Load();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = GameManager.totalScore.ToString("N0");
    }

    // Update is called once per frame
    void Update()
    {
    }

    // シーンを読み込む
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

}
