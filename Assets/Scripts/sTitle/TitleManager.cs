using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string sceneName;

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
        GameManager.totalScore = 0;
        SceneManager.LoadScene(sceneName);
    }

}
